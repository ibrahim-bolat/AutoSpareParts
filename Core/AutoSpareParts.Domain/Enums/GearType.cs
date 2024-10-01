using System.ComponentModel;

namespace AutoSpareParts.Domain.Enums;

public enum GearType
{
    [Description("Düz")]
    Manuel = 1,
    
    [Description("Yarı Otomatik")]
    SemiAutomatic = 2,
    
    [Description("Otomatik")]
    Automatic = 3
}