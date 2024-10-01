using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Domain.Entities;
public class UserImage:BaseEntity
    {
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public string ImageAltText { get; set; }
        public bool Profil { get; set; }
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
