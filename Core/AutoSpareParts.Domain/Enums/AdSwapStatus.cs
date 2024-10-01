using System.ComponentModel;

namespace AutoSpareParts.Domain.Enums;

public enum AdSwapStatus
{
    [Description("Evet")]
    Yes = 1,
    
    [Description("Hayır")]
    No = 2
}