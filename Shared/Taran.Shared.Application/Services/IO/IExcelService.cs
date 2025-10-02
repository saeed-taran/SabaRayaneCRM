namespace Taran.Shared.Application.Services.IO
{
    public interface IExcelService
    {
        byte[] GenerateExcelWithValidation(Type inputDtoType, Dictionary<string, object> fixedValues);
    }
}