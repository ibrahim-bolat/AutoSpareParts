using AutoSpareParts.Domain.Entities.Common;

namespace AutoSpareParts.Domain.Entities;
public class ProductImage:Image
    {
        public bool Vitrin { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
