using System.ComponentModel.DataAnnotations;

namespace Messages.Models;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [MinLength(1)]
    public string Text { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
