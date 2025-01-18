using YamlDotNet.Serialization;

public class BlogFrontMatter
{
    [YamlMember(Alias = "tags")]
    public string? Tags { get; set; }
    
    [YamlMember(Alias = "title")]
    public string Title { get; set; }
    [YamlMember(Alias = "image")]
    public string? Image { get; set; }
    [YamlMember(Alias = "date")]
    public DateTimeOffset Date { get; init; }
    [YamlIgnore]
    public IList<string> GetTags => Tags?
        .Split(",", StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Trim())
        .ToArray();
}
