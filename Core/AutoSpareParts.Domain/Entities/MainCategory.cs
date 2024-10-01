using AutoSpareParts.Domain.Entities.Common;

namespace AutoSpareParts.Domain.Entities;

    public class MainCategory : BaseEntity
    {
        public string Name { get; set; }  //Fren Gibi
        public List<Category> Categories { get; set; }
    }
