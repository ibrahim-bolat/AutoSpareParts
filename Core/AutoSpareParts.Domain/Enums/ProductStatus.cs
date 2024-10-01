using System.ComponentModel;

namespace AutoSpareParts.Domain.Enums;

public enum ProductStatus
{
    [Description("İkinci El")]
    SecondHand = 1,
    
    [Description("İthal Sıfır")]
    ImportedFirstHand = 2,
    
    [Description("Sıfır")]
    FirstHand = 3
}