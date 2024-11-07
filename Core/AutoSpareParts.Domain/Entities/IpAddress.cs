using System.ComponentModel.DataAnnotations;
using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities;

public class IPAddress:BaseEntity
    {
        public string RangeStart { get; set; }
        public string RangeEnd { get; set; }
        public  IPListType IPListType { get; set; }
        public  List<Endpoint> Endpoints{ get; set; }
    }
