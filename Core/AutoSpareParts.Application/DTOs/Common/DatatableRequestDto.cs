namespace AutoSpareParts.Application.DTOs.Common;

public record DatatableRequestDto
{
    public int Draw { get; init; }
    public int Start { get; init; }
    public int Length { get; init; }
    public Search Search { get; init; }
    public List<Column> Columns { get; init; }
    public List<Order> Order { get; init; }
}

public record Column
{
    public string Data { get; init; }
    public string Name { get; init; }
    public bool Searchable { get; init; }
    public bool Orderable { get; init; }
    public Search Search { get; init; }
}

public record Order
{
    public int Column { get; init; }
    public OrderDirType Dir { get; init; }
}

public enum OrderDirType
{
    Asc,
    Desc
}

public record Search
{
    public string Value { get; init; }
    public bool Regex { get; init; }
}