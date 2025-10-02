using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using Taran.Identity.Access;
using Taran.Identity.Core.Aggregates.RoleAggregate;
using Taran.Identity.Core.Aggregates.UserAggregate;
using Taran.Infrastructure;
using Taran.Shared.Application.Services.Security;

namespace SabaRayane.Infrastructure;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new MainDbContext(serviceProvider.GetRequiredService<DbContextOptions<MainDbContext>>(), null);
        
        User user;

        if (!context.User.Any())
        {
            user = new User(1, "admin", PasswordHasher.Hash("admin123"), null, null, null, null, true, false);

            context.User.Add(user);
            context.SaveChanges();

            var role = new Role(user.Id, "admin", "admin");
            context.Role.Add(role);
            context.SaveChanges();

            user.AddRole(user.Id, role.Id);

            List<int> allAccesses = Enum.GetValues(typeof(AccessNames))
                             .Cast<int>()
                             .ToList();
            role.SetRoleAccesses(user.Id, allAccesses);

            context.SaveChanges();
        }

        foreach (var placeHolder in PlaceHolder.PlaceHolders) 
        {
            if (!context.PlaceHolder.Any(p => p.Id == placeHolder.Id))
            {
                context.PlaceHolder.Add(placeHolder);
            }
        }

        context.SaveChanges();
    }
}