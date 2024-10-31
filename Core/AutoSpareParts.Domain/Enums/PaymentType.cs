using System.ComponentModel;

namespace AutoSpareParts.Domain.Enums;
public enum PaymentType
    {
        [Description("Nakit")]
        Cash = 1,
        
        [Description("Kredi Kartı")]
        CreditCard = 2,

        [Description("Havale")]
        Transfer = 3,
        
        [Description("Diğer")]
        Other = 4,
    }
