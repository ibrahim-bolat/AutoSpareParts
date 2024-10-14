using AutoSpareParts.Domain.Entities.Common;

namespace AutoSpareParts.Domain.Entities;
public class ProductImage:BaseImage
    {
        public bool Vitrin { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
