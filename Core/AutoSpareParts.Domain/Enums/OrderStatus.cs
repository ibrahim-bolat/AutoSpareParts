using System.ComponentModel;

namespace AutoSpareParts.Domain.Enums;

public enum OrderStatus
{
    [Description("Gönderildi")]
    Sent = 1,
    
    [Description("İthal Sıfır")]
    NotSent = 2,
    
    [Description("Sıfır")]
    Canceled = 3
}