namespace Text.API.Data.Entities;

public class Text
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}