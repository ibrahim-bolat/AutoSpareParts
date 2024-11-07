using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

public class SupplierAddress : Address
{
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
}
