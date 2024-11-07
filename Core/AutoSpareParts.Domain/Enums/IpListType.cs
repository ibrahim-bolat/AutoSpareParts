using System.ComponentModel;

namespace AutoSpareParts.Domain.Enums;
public enum IPListType
    {
        [Description("Beyaz Liste")]
        WhiteList = 1,
        
        [Description("Kara Liste")]
        BlackList = 2,
    }
