using AutoSpareParts.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSpareParts.Domain.Entities
{
    public class Oem:BaseEntity
    {
        public string OemNo { get; set; }  //3140005010 gibi
        public string OemBrandName { get; set; }  //FORD,VOLVO gibi

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
