using Newtonsoft.Json;

namespace AutoSpareParts.Application.DTOs.Common;

public class TreeViewDto
{
    public string id { get; set; }
    public string text { get; set; }
    public bool @checked { get; set; }
    public virtual List<TreeViewDto> children { get; set; }
}