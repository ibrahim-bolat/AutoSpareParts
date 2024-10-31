using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

public class Inventory : BaseEntity
{

    public string StockCode { get; set; }  //Ford Tourneo Courier 1.5 2014-2017 Arası Hidrolik Debriyaj Rulmanı Luk Marka gibi
    public bool StockStatus { get; set; }  // Stok Durumu (var ,yok )
    public int StockQuantity { get; set; }  // Stok Miktarı (1000, 5000)
    public Product Product { get; set; }

}
