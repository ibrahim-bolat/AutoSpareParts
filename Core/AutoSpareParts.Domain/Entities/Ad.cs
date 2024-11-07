
using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoSpareParts.Domain.Entities;

public class Ad : BaseEntity
{
    public string AdNo { get; set; }  //123458752 gibi (İlan No)
    public string Title { get; set; }  //SAHİBİNDEN 2016 SERVİS BAKIMLI İYİ NİYET GARANTİLİ GOLF gibi
    public DateTime AdDate { get; set; } // 25 Eylül 2022 12:00.00 gibi
    public decimal FormerPrice { get; set; } // 500.500 TL gibi
    public decimal CurrentPrice { get; set; } // 500.500 TL gibi
    public string DiscountAmount { get; set; } //  %25 gibi
    public int StarCount { get; set; } //  3 yıldız gibi
    public int GiveStarUserCount { get; set; } //  350 kişi
    public int AdOrder { get; set; } //  5.sırada gibi
    public bool Showcase { get; set; } //  Vitrin (yayımlansınmı?)
    public string AdDetail { get; set; } // İlan (Yedek Parça) Hakkında Uzun Açıklamalar 

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public List<Comment> Comments { get; set; }
}
