using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using Taran.Shared.Language;
using Taran.Shared.Languages;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Taran.Shared.Dtos.Attributes;

namespace Taran.Shared.Application.Services.IO;

public class ExcelService : IExcelService
{
    private readonly CultureConfiguration cultureConfiguration;
    private readonly ITranslator translator;

    public ExcelService(IOptions<CultureConfiguration> cultureConfiguration, ITranslator translator)
    {
        this.cultureConfiguration = cultureConfiguration.Value;
        this.translator = translator;
    }

    public byte[] GenerateExcelWithValidation(Type inputDtoType, Dictionary<string, object> fixedValues)
    {
        ExcelPackage.License.SetNonCommercialPersonal("SaeedTaran");

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Data");

        var properties = inputDtoType.GetProperties();

        for (int col = 0; col < properties.Length; col++)
        {
            worksheet.Cells[1, col + 1].Value = translator.Translate(properties[col].Name);
            var comment = worksheet.Cells[1, col + 1].AddComment(properties[col].Name);
            comment.Visible = false;
        }

        for (int col = 0; col < properties.Length; col++)
        {
            var property = properties[col];
            int excelColumn = col + 1;
            object? fixedValue = null;
            fixedValues.TryGetValue(property.Name, out fixedValue);
            ApplyValidation(worksheet, excelColumn, property, fixedValue);
        }

        return package.GetAsByteArray();
    }

    private void ApplyValidation(ExcelWorksheet worksheet, int col, PropertyInfo property, object fixedValue)
    {
        var range = worksheet.Cells[2, col];

        if (fixedValue is not null)
        { 
            range.Value = fixedValue;
            return;
        }

        foreach (var attr in property.GetCustomAttributes())
        {
            if (attr is RequiredAttribute)
            {
                worksheet.Cells[1, col].Style.Font.Bold = true;
                worksheet.Cells[1, col].Style.Font.Color.SetColor(System.Drawing.Color.Red);
            }

            if (attr is RangeAttribute rangeAttr)
            {
                var validation = worksheet.DataValidations.AddIntegerValidation(range.Address);
                validation.Operator = ExcelDataValidationOperator.between;
                validation.Formula.Value = Convert.ToInt32(rangeAttr.Minimum);
                validation.Formula2.Value = Convert.ToInt32(rangeAttr.Maximum);
                validation.ShowErrorMessage = true;
                validation.ErrorTitle = "Invalid Input";
                validation.Error = $"Enter a value between {rangeAttr.Minimum} and {rangeAttr.Maximum}.";
            }

            if (attr is StringLengthAttribute strLengthAttr)
            {
                var validation = worksheet.DataValidations.AddTextLengthValidation(range.Address);
                validation.Operator = ExcelDataValidationOperator.between;
                validation.Formula.Value = strLengthAttr.MinimumLength;
                validation.Formula2.Value = strLengthAttr.MaximumLength;
                validation.ShowErrorMessage = true;
                validation.ErrorTitle = "Invalid Length";
                validation.Error = $"Text must be between {strLengthAttr.MinimumLength} and {strLengthAttr.MaximumLength} characters.";
            }

            if (attr is EmailAddressAttribute)
            {
                var validation = worksheet.DataValidations.AddCustomValidation(range.Address);
                validation.Formula.ExcelFormula = "ISNUMBER(SEARCH(\"@\",INDIRECT(ADDRESS(ROW(),COLUMN()))))";
                validation.ShowErrorMessage = true;
                validation.ErrorTitle = "Invalid Email";
                validation.Error = "Enter a valid email address.";
            }

            if (attr is DateAttribute)
            {
                if (cultureConfiguration.DateTime.ToLower() == "shamsi")
                {
                    var validation = worksheet.DataValidations.AddCustomValidation(range.Address);
                    validation.Formula.ExcelFormula = "AND(ISNUMBER(--LEFT(A2,4)), MID(A2,5,1)=\"/\", ISNUMBER(--MID(A2,6,2)), MID(A2,8,1)=\"/\", ISNUMBER(--RIGHT(A2,2)))";
                    validation.ShowErrorMessage = true;
                    validation.ErrorTitle = "Invalid Persian Date Format";
                    validation.Error = "Enter a valid Persian date in YYYY/MM/DD format.";
                    worksheet.Column(col).Style.Numberformat.Format = "@";
                    worksheet.Cells[2, col].Value = "1403/05/04";
                }
            }

            if (string.IsNullOrWhiteSpace(worksheet.Cells[2, col].Value?.ToString())) 
            {
                var sampleValue = (property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null);
                if (sampleValue?.ToString() == "0")
                    sampleValue = 1;
                else
                    worksheet.Cells[2, col].Style.Numberformat.Format = "@";//text
                worksheet.Cells[2, col].Value = sampleValue;
            }
        }
    }
}
