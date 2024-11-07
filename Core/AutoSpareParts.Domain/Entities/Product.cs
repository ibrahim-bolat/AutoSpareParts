using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSpareParts.Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }  //Ford Tourneo Courier 1.5 2014-2017 Arası Hidrolik Debriyaj Rulmanı Luk Marka gibi
        public DateOnly ProductionDate { get; set; } // 25 Eylül 2022 gibi
        public ProductStatus ProductStatus { get; set; } //2.El ,Sıfır gibi
        public GuaranteeStatus GuaranteeStatus { get; set; }  // var yok gibi
        public int GuaranteePeriod { get; set; } // 2 yıl gibi
        public OriginalityStatus OriginalityStatus { get; set; } // Orijinal Yan sanayii gibi
        public decimal PurchasePrice { get; set; } // 500.500 TL gibi Alış fiyatı
        public decimal SalePrice { get; set; } // 600.100 TL gibi satış fiyatı
        public string ProductDetail { get; set; } // İlan (Yedek Parça) Hakkında Uzun Açıklamalar 
        public Inventory Inventory { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public List<Category> Categories { get; set; }
        public List<Model> Models { get; set; }
        public List<Oem> Oems { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Ad> Ads { get; set; }
        public List<Discount> Discounts{ get; set; }
        public List<OrderDetail> OrderDetails{ get; set; }
    }
}
