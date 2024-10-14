using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Domain.Entities;
public class UserImage:BaseImage
    {
        public bool Profil { get; set; }
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
