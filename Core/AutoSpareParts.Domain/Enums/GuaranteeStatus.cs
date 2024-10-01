using System.ComponentModel;

namespace AutoSpareParts.Domain.Enums;

public enum GuaranteeStatus
{
    [Description("Var")]
    Yes = 1,

    [Description("Yok")]
    No = 2
}