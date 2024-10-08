using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace AutoSpareParts.Application.Features.Accounts.Validations.CustomValidations;

public class CustomDateAttribute:ValidationAttribute
{
    public CustomDateAttribute() : base("{0} uygun tarih girilmelidir.")
    {
        
    }
    public override bool IsValid(object value)
    {
        DateOnly propValue = (DateOnly)value;
        if (propValue <= DateOnly.FromDateTime(DateTime.Now) && propValue >= DateOnly.FromDateTime(DateTime.Now).AddYears(-100) || propValue==DateOnly.MinValue)
        {
            return true;
        }
        return false;
    }

}