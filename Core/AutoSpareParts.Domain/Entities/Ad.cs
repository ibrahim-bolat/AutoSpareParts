
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
        public int StarRating { get; set; } //  3 yıldız gibi
        public int AdPageOrder { get; set; } //  5.sırada gibi
        public string AdDetail { get; set; } // İlan (Yedek Parça) Hakkında Uzun Açıklamalar 

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int UserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<Comment> Comments { get; set; }
}
