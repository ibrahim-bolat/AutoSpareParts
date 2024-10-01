using System.ComponentModel;

namespace AutoSpareParts.Domain.Enums;

public enum GenderType
{
    [Description("Belirtilmemiş")]
    Unspecified,
    
    [Description("Erkek")]
    Male,
    
    [Description("Kadın")]
    Female,
}