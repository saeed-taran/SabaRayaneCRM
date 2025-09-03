namespace Taran.Shared.UI.Components.CustomizedQuickGrid.Columns;

public class ThirdPartyComponent
{
    public Action Show { get; set; }
    public Func<object?, Task>? ValueChanged { get; set; }
    public string? DisplayText { get; private set; }

    public ThirdPartyComponent(Action show)
    {
        Show = show;
    }

    public async Task SetValue(object? value, string? text)
    {
        if (ValueChanged is not null)
        {
            DisplayText = text;
            await ValueChanged(value);
        }
    }

    public void ClearDisplayText() 
    {
        DisplayText = null;
    }

    public void EmptyDisplayText()
    {
        DisplayText = "";
    }
}
