using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSpareParts.Domain.Entities
{
    public class Comment:BaseEntity
    {
        public string CommentDetail{ get; set; }  // Yorum açıklaması
        public int CommentOrder { get; set; }  // 10.sıra gibi
        public int CommentStarRating { get; set; } //  3 yıldız gibi
        public int AdId { get; set; }
        public Ad Ad { get; set; }
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
