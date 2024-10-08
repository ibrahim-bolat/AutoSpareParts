using Newtonsoft.Json;

namespace AutoSpareParts.Application.DTOs.Common;

public record DatatableResponseDto<T>
{
    [JsonProperty("draw")] 
    public int Draw { get; init; }
    
    [JsonProperty("recordsTotal")] 
    public int RecordsTotal { get; init; }
    
    [JsonProperty("recordsFiltered")] 
    public int RecordsFiltered { get; init; }

    [JsonProperty("data")] 
    public IEnumerable<T> Data { get; init; }

    [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
    public string Error { get; init; }
    
}