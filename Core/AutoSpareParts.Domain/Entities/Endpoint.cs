using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;

namespace AutoSpareParts.Domain.Entities;

public class Endpoint:BaseEntity
{
    public string EndpointName { get; set; }
    
    public string ControllerName { get; set; }
    
    public string AreaName { get; set; }
    public string EndpointType { get; set; }
    public string HttpType { get; set; }
    public string Definition { get; set; }
    public string Code { get; set; }
    
    public  List<AppRole> AppRoles{ get; set; }
    public  List<IpAddress> IpAddresses{ get; set; }
}