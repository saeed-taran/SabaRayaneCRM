namespace Taran.Shared.UI.Components.CustomizedQuickGrid.Columns;

public class EditTemplate
{
    public string PropertyName { get; private set; }
    public AutoCompleteItemsProvider AutoCompleteItemsProvider { get; private set; }
    public IReadOnlyCollection<(int, string)> DropDownItems { get; private set; }
    public IReadOnlyCollection<(string, string)> StringDropDownItems { get; private set; }
    public delegate Task DropDownChanged(object? value);
    public DropDownChanged OnDropDownChanged { get; private set; }
    public bool IsSecret { get; private set; } = false;

    public EditTemplateUsage EditTemplateUsage { get; set; } = EditTemplateUsage.Both;

    public Func<object, bool>? IsEnableFunction { get; private set; }
    public bool ClearValueWhenDisabled { get; private set; } = true;

    public ThirdPartyComponent ThirdPartyComponent { get; private set; }

    public EditTemplate(string propertyName)
    {
        PropertyName = propertyName;
    }

    public EditTemplate(string propertyName, List<(int, string)> dropDownItems)
    {
        PropertyName = propertyName;
        DropDownItems = dropDownItems;
    }

    public EditTemplate(string propertyName, List<(string, string)> stringDropDownItems)
    {
        PropertyName = propertyName;
        StringDropDownItems = stringDropDownItems;
    }

    public EditTemplate(string propertyName, AutoCompleteItemsProvider autoCompleteItemsProvider)
    {
        PropertyName = propertyName;
        AutoCompleteItemsProvider = autoCompleteItemsProvider;
    }

    public EditTemplate OnlyEdit()
    {
        EditTemplateUsage = EditTemplateUsage.Edit;
        return this;
    }

    public EditTemplate OnlyCreate()
    {
        EditTemplateUsage = EditTemplateUsage.Create;
        return this;
    }

    public EditTemplate Secure()
    {
        IsSecret = true;
        return this;
    }

    public EditTemplate SetThirdPartyComponent(ThirdPartyComponent thirdPartyComponent)
    {
        ThirdPartyComponent = thirdPartyComponent;
        return this;
    }

    public EditTemplate SetAbilityDependency(Func<object, bool> isEnableFunction, bool clearValueWhenDisabled = true)
    {
        IsEnableFunction = isEnableFunction;
        ClearValueWhenDisabled = clearValueWhenDisabled;

        return this;
    }

    public EditTemplate SetOnDropDownChanged(DropDownChanged onDropDownChanged)
    {
        OnDropDownChanged = onDropDownChanged;
        return this;
    }
}

public delegate (int, string) AutoCompleteItemsProvider(string term);
public enum EditTemplateUsage
{
    Edit,
    Create,
    Both
}