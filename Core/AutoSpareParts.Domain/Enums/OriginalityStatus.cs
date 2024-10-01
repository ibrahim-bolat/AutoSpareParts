using System.ComponentModel;

namespace AutoSpareParts.Domain.Enums;

public enum OriginalityStatus
{
    [Description("Orijinal")]
    Original = 1,

    [Description("Yan Sanayi")]
    SubIndustry = 2,

    [Description("Çýkma")]
    Cannibalized = 2
}