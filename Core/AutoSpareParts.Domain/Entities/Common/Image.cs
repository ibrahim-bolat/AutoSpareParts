using AutoSpareParts.Domain.Enums;

namespace AutoSpareParts.Domain.Entities.Common;

public abstract class Image:BaseEntity
{
    public string Title { get; set; }
    public string Path { get; set; }
    public string AltText { get; set; }
}