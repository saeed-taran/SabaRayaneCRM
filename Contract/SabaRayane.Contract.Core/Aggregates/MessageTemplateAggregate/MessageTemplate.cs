using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using System.ComponentModel.DataAnnotations;
using Taran.Shared.Core.Entity;
using Taran.Shared.Core.Exceptions;

namespace SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;

[Index(nameof(DaysUntilAgreementExpire), IsUnique = true)]
public class MessageTemplate : AggregateRoot<int>
{
    [StringLength(50)]
    public string Name { get; private set; }
    
    [StringLength(500)]
    public string Template { get; private set; }
    public int DaysUntilAgreementExpire { get; private set; }

    public MessageTemplate(int creatorUserId, string name, string template, int daysUntilAgreementExpire) : base(creatorUserId)
    {
        Name = DomainArgumentNullOrEmptyException.Ensure(name, nameof(Name));
        Template = DomainArgumentNullOrEmptyException.Ensure(template, nameof(Template)); ;
        DaysUntilAgreementExpire = daysUntilAgreementExpire;

        if (!IsValid())
            throw new DomainInvalidArgumentException(nameof(PlaceHolder));
    }

    public string FillPlaceHolders(Customer customer) 
    {
        if (!IsValid())
            throw new DomainInvalidArgumentException(nameof(PlaceHolder));

        var result = Template;
        var parameters = Template.Split(" ").Where(p => p.Trim().StartsWith("@"));

        foreach (var parameter in parameters)
        {
            var placeHolder = PlaceHolder.PlaceHolders[parameter];
            result = result.Replace(parameter, placeHolder.GetValue(customer));
        }

        return result;
    }

    public bool IsValid() 
    {
        var result = Template;
        var parameters = Template.Split(" ").Where(p => p.Trim().StartsWith("@"));

        return !parameters.Any(p => !PlaceHolder.PlaceHolders.ContainsKey(p));
    }

    public void Update(int userId, string name, string template, int daysUntilAgreementExpire)
    {
        Name = DomainArgumentNullOrEmptyException.Ensure(name, nameof(Name));
        Template = DomainArgumentNullOrEmptyException.Ensure(template, nameof(Template)); ;
        DaysUntilAgreementExpire = daysUntilAgreementExpire;

        if (!IsValid())
            throw new DomainInvalidArgumentException(nameof(PlaceHolder));

        Update(userId);
    }
}
