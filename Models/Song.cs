namespace UkuleleSongbook.Models;

public sealed class Song
{
    public string Id { get; init; } = Guid.NewGuid().ToString();

    public required string Title           { get; set; }
    public required string Artist          { get; set; }
    public string          Genre           { get; set; } = string.Empty;
    public string          Key             { get; set; } = string.Empty;
    public int             Capo            { get; set; } = 0;
    public string          Tempo           { get; set; } = string.Empty;
    public string          ChordProContent { get; set; } = string.Empty;
    public string          FilePath        { get; set; } = string.Empty;
    public DateTime        DateAdded       { get; init; } = DateTime.Now;
    public DateTime        DateModified    { get; set; }  = DateTime.Now;

    public override string ToString() => $"{Artist} – {Title}";
}
