using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Application.CustomAttributes;

public class EndpointAttribute:Attribute
{
    public string Menu { get; set; }
    public string Decription { get; set; }
    public EndpointType EndpointType { get; set; }
}