using Microsoft.AspNetCore.Components;

namespace Taran.Shared.UI.Components.CustomizedQuickGrid;

public class CustomeAction<TGridItem>
{
    public Func<TGridItem, Task> Function { get; set; }
    public string Class { get; set; }

    public CustomeAction(Func<TGridItem, Task> function, string cssClass)
    {
        Function = function;
        Class = cssClass;
    }
}
