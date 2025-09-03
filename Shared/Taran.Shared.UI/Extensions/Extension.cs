using Microsoft.Extensions.DependencyInjection;

namespace Taran.Shared.UI.Extensions;

public static class Extension
{
    public static void AddServices(this IServiceCollection services, Type typeToGetAssembly)
    {
        foreach (var type in typeToGetAssembly.Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service")))
        {
            var i = type.GetInterfaces();
            var _interface = i.First(v => v.Name == "I" + type.Name);
            services.AddScoped(_interface, type);
        }
    }
}
