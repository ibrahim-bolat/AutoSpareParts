using Newtonsoft.Json;

namespace AutoSpareParts.Application.DTOs.Common;

public record TreeViewDto
{
    public string id { get; init; }
    public string text { get; init; }
    public bool @checked { get; init; }
    public virtual List<TreeViewDto> children { get; init; }
}