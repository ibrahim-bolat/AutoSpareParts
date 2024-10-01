using System.ComponentModel;

namespace AutoSpareParts.Domain.Enums;

public enum EndpointType
{
    [Description("Okuma")]
    Reading = 1,

    [Description("Yazma")]
    Writing=2,

    [Description("Güncelleme")]
    Updating=3,

    [Description("Silme")]
    Deleting=4
}