using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.UserOperations.DTOs;



public class RoleAssignDto:BaseDto
    {
        public int Id { get; set; }
        
        
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
        public bool HasAssign { get; set; }
    }
