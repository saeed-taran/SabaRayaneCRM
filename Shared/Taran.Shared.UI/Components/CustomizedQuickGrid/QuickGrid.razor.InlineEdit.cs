using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Taran.Shared.Dtos.Languages;
using Taran.Shared.UI.Components.CustomizedQuickGrid.Columns;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Taran.Shared.UI.Components.CustomizedQuickGrid;

public partial class QuickGrid<TGridItem>
{
    private int _editingRowIndex = -1;
    private string _crudErrorMessage = "";
    private string _createErrorMessage = "";

    [Parameter] public bool FixedToolsColumn { get; set; } = true;

    [Parameter] public EventCallback<IReadOnlyCollection<TGridItem>> OnSelectedChanged { get; set; }

    [Parameter] public bool MultiSelect { get; set; } = false;
    private List<int> _selectedRowIndex = new();
    private List<TGridItem> _selectedItem = new();
    public IReadOnlyCollection<TGridItem> SelectedItem => _selectedItem;

    private bool _showNewItemRow = false;

    public delegate Task<GridItemEditResult> GridSaveChanges(object item);
    public delegate Task<GridItemEditResult> GridItemDelete(TGridItem item);
    public delegate Task GridInlineEditStarting(TGridItem item);
    public delegate Task GridCreatiStarting();
    public delegate Task GridEditCreateItemChanged(object item);

    [Parameter, EditorRequired] public string Id { get; set; }
    [Parameter] public GridSaveChanges OnSaveEdit { get; set; }
    [Parameter] public GridSaveChanges OnSaveNew { get; set; }

    [Parameter] public GridEditCreateItemChanged OnGridEditCreateItemChanged { get; set; }

    [Parameter] public GridInlineEditStarting OnInlineEditStarting { get; set; }
    [Parameter] public GridCreatiStarting OnCreateStarting { get; set; }
    [Parameter] public EventCallback OnEditOrCreateEnd { get; set; }

    [Parameter] public EventCallback<TGridItem> OnRowDoubleCkicked { get; set; }

    [Parameter] public GridItemDelete OnDelete { get; set; }

    private List<PropertyInfo> _gridItemProperties;

    [Parameter] public Type EditingObjectType { get; set; }
    private List<PropertyInfo> _editingObjectProperties;
    private object _editingObject;
    [Parameter] public EventCallback<TGridItem> CustomeEditAction { get; set; }

    [Parameter] public List<CustomeAction<TGridItem>> CustomeActions { get; set; } = new();
    private int ActionColumnWidth => 100 + CustomeActions.Count * 30;

    [Parameter] public Type CreatingObjectType { get; set; }
    [Parameter] public bool CreatingEnabled { get; set; } = true;
    private List<PropertyInfo> _creatingObjectProperties;
    private object _creatingObject;

    [Parameter] public string CreatingPageUrl { get; set; }

    private Dictionary<string, object?> DefaultValues = new();

    private Dictionary<string, FieldsVisualStatus> fieldStatusDict = new();

    private bool HaveAnyTools => OnDelete is not null || EditingObjectType is not null || CreatingObjectType is not null || CustomeActions.Any();

    protected override Task OnInitializedAsync()
    {
        _editingObjectProperties = EditingObjectType?.GetProperties().ToList() ?? new();
        foreach (var property in _editingObjectProperties)
        {
            if (!fieldStatusDict.ContainsKey(property.Name))
                fieldStatusDict[property.Name] = new FieldsVisualStatus();
        }

        _creatingObjectProperties = CreatingObjectType?.GetProperties().ToList() ?? new();
        foreach (var property in _creatingObjectProperties)
        {
            if (!fieldStatusDict.ContainsKey(property.Name))
                fieldStatusDict[property.Name] = new FieldsVisualStatus();
        }

        _gridItemProperties = typeof(TGridItem).GetProperties().ToList();

        return base.OnInitializedAsync();
    }

    public void AddDefaultValue(string propertyName, object? value)
    {
        DefaultValues.Add(propertyName, value);
    }

    private async Task StartEditing(int rowIndex, TGridItem item)
    {
        if (rowIndex == _editingRowIndex)
            return;

        CloseEdit();

        if (OnInlineEditStarting is not null)
            await OnInlineEditStarting(item);

        _editingObject = Activator.CreateInstance(EditingObjectType) ?? throw new Exception("Unable to create instance for new item.");

        foreach (var gridItemProperty in _gridItemProperties)
        {
            var editingObjectProperty = _editingObjectProperties.FirstOrDefault(p => p.Name == gridItemProperty.Name);
            if (editingObjectProperty is null)
                continue;

            var propertyValue = gridItemProperty.GetValue(item);
            editingObjectProperty.SetValue(_editingObject, propertyValue);

            if(DefaultValues.ContainsKey(gridItemProperty.Name))
                editingObjectProperty.SetValue(_editingObject, DefaultValues[gridItemProperty.Name]);

            var colWithEditTemplate = _columns.FirstOrDefault(c => c.EditTemplate?.PropertyName == gridItemProperty.Name);
            if (colWithEditTemplate?.EditTemplate?.OnDropDownChanged is not null)
            {
                await colWithEditTemplate.EditTemplate.OnDropDownChanged(propertyValue);
            }
        }

        _editingRowIndex = rowIndex;

        await MakeRowDropDownsSearchable(rowIndex);
    }

    private async Task StartCreating(int rowIndex)
    {
        if (_showNewItemRow)
            return;

        CloseEdit();

        if (OnCreateStarting is not null)
            await OnCreateStarting();

        _creatingObject = Activator.CreateInstance(CreatingObjectType) ?? throw new Exception("Unable to create instance for edit item.");

        if (DefaultValues.Any()) 
        {
            var props = CreatingObjectType.GetProperties(
                BindingFlags.Public | BindingFlags.Instance
            );

            foreach (var kv in DefaultValues)
            {
                var prop = props.FirstOrDefault(p => p.Name.Equals(kv.Key, StringComparison.OrdinalIgnoreCase));

                if (prop != null && prop.CanWrite)
                {
                    object? value = kv.Value;
                    prop.SetValue(_creatingObject, value);
                }
            }
        }

        _showNewItemRow = true;

        await MakeRowDropDownsSearchable(rowIndex);
    }

    private async Task MakeRowDropDownsSearchable(int rowIndex)
    {
        await jsRuntime.InvokeVoidAsync("setTimeout", $"initSelects('#{Id} tr[aria-rowindex=\"{rowIndex}\"] select', 0)", 1);
    }
    private async Task MakeDropDownsSearchableById(string dropDownId, string initialValue)
    {
        await jsRuntime.InvokeVoidAsync("setTimeout", $"initSelects('#{dropDownId}', 0, {initialValue})", 1);
    }
    private async Task ClearSearchableDropDowns(string? selector = null)
    {
        await jsRuntime.InvokeVoidAsync("clearSelects", selector ?? $"#{Id} select");
    }

    private async Task SetDateinputMask(string inputId)
    {
        await jsRuntime.InvokeVoidAsync("setTimeout", $"setDateInputMask('#{inputId}')", 1);
    }

    private async Task SaveEdit()
    {
        if (!ValidateObject(_editingObject))
            return;

        if (OnSaveEdit != null)
        {
            var result = await OnSaveEdit(_editingObject);
            if (result.Success)
            {
                await RefreshDataAsync();
                CloseEdit();
            }
            else
                //_crudErrorMessage = result.ErrorMessage ?? "Unknown error!";
                await toastService.Show(result.ErrorMessage ?? "Unknown error!", UI.Services.ToastKind.danger);
        }
    }

    private async Task SaveCreate()
    {
        if (!ValidateObject(_creatingObject))
            return;

        if (OnSaveNew != null)
        {
            var result = await OnSaveNew(_creatingObject);
            if (result.Success)
            {
                await ClearSearchableDropDowns();
                await RefreshDataAsync();
                CloseEdit();
            }
            else
                //_crudErrorMessage = result.ErrorMessage ?? "Unknown error!";
                await toastService.Show(result.ErrorMessage ?? "Unknown error!", UI.Services.ToastKind.danger);
        }
    }

    private bool ValidateObject(object Obj)
    {
        var ctx = new ValidationContext(Obj);
        List<ValidationResult> validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(Obj, ctx, validationResults, true);

        foreach (var fieldStatus in fieldStatusDict)
        {
            var currenctFieldError = validationResults.FirstOrDefault(r => r.MemberNames.First() == fieldStatus.Key);
            if (currenctFieldError is null)
                fieldStatus.Value.SetValid();
            else
                fieldStatus.Value.SetError(currenctFieldError.ErrorMessage ?? "Error");
        }

        return isValid;
    }

    private async void CloseEdit()
    {
        _editingRowIndex = -1;
        _crudErrorMessage = "";

        _showNewItemRow = false;

        foreach (var key in fieldStatusDict.Keys)
        {
            fieldStatusDict[key].Clear();
        }

        await ClearSearchableDropDowns();

        if (OnEditOrCreateEnd.HasDelegate)
            await OnEditOrCreateEnd.InvokeAsync();

        foreach (var col in _columns)
        {
            col.EditTemplate?.ThirdPartyComponent?.ClearDisplayText();
        }
    }

    protected async Task InputValueChanged(object obj, PropertyInfo propertyInfo, ChangeEventArgs e, EditTemplate editTemplate)
    {
        var propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
        object safeValue;

        if (propertyInfo.PropertyType.IsEnum)
        {
            safeValue = Enum.Parse(propertyInfo.PropertyType, e.Value?.ToString()!);
            propertyInfo.SetValue(obj, safeValue);
        }
        else
        {
            if ((e.Value == null || string.IsNullOrEmpty(e.Value + "")))
            {
                if (propertyInfo.ToString().StartsWith("System.Nullable"))
                    safeValue = null;
                else if (propertyType == typeof(string))
                    safeValue = "";
                else
                    safeValue = Activator.CreateInstance(propertyType);
            }
            else
                safeValue = Convert.ChangeType(e.Value, propertyType);

            propertyInfo.SetValue(obj, safeValue);
        }

        if (editTemplate.OnDropDownChanged != null)
        {
            await editTemplate.OnDropDownChanged(safeValue);
        }

        if (OnGridEditCreateItemChanged != null)
            await OnGridEditCreateItemChanged(_editingObject ?? _creatingObject);

        ValidateObject(obj);
    }

    private async Task DeleteItem(TGridItem item)
    {
        bool confirmed = await deleteConfirmDialog.Confirm(translator.Translate(nameof(KeyWords.DoYouWantToDeleteThisItem)));
        if (confirmed)
        {
            var result = await OnDelete(item!);
            if (result.Success)
                await RefreshDataAsync();
            else
                await toastService.Show(result.ErrorMessage ?? "Unknown error!", UI.Services.ToastKind.danger);
        }
    }

    private class FieldsVisualStatus
    {
        public string InputElementClass { get; private set; } = "";
        public string ErrorMessage { get; private set; } = "";

        public void SetError(string errorMessage)
        {
            ErrorMessage = errorMessage;
            InputElementClass = "is-invalid";
        }

        public void SetValid()
        {
            ErrorMessage = "";
            InputElementClass = "is-valid";
        }

        public void Clear()
        {
            ErrorMessage = "";
            InputElementClass = "";
        }
    }
}

public record GridItemEditResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}
