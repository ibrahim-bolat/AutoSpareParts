using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Application.DTOs.Base;

namespace AutoSpareParts.Application.Features.Employees.DTOs;



public record RoleAssignDto:BaseDto
    {
        public int Id { get; init; }
        
        [Display(Name = "Rol Adı")]
        public string Name { get; init; }
        public bool HasAssign { get; init; }
    }
