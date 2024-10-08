using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.RoleOperations.DTOs;

public record RoleDto:BaseDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
        
        public bool Status { get; set; }
    }
