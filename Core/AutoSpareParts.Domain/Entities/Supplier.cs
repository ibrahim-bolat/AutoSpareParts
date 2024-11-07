using AutoSpareParts.Domain.Entities.Common;

namespace AutoSpareParts.Domain.Entities;

public class Supplier : BaseEntity
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }
    public List<SupplierAddress> SupplierAddresses { get; set; }
}
