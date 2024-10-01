

using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoSpareParts.Domain.Entities.Identity;

public class AppUser:IdentityUser<int>,IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public GenderType GenderType { get; set; }
        public string UserIdendityNo { get; set; }
        
        public  DateOnly? DateOfBirth{get; set;}
        public  DateTime CreatedTime { get; set; } = DateTime.Now;
        public  DateTime ModifiedTime { get; set; } = DateTime.Now;
        public  bool IsActive { get; set; } = true;
        public  bool IsDeleted { get; set; } = false;
        public  string CreatedByName { get; set; } = "Owner";
        public  string ModifiedByName { get; set; } = "Owner";
        public  string Note { get; set; }
        public List<Ad> Ads { get; set; }
        public  List<Address> Addresses{ get; set; }
        public  List<UserImage> UserImages{ get; set; }
        
        public  List<RequestInfoLog> RequestInfoLogs{ get; set; }
        public List<Comment> Comments { get; set; }
}
    