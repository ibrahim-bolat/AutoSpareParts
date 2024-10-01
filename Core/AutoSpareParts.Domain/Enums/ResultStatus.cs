using System.ComponentModel;

namespace AutoSpareParts.Domain.Enums;

public enum ResultStatus
{
    [Description("Başarılı")]
    Success = 1,

    [Description("Hatalı")]
    Error = 2,

    [Description("Uyarı")]
    Warning = 3,

    [Description("Bilgi")]
    Info = 4
}

