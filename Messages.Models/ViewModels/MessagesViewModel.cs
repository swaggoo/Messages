namespace Messages.Models.ViewModels;

public class MessagesViewModel
{
    public IEnumerable<Message> AllMessages { get; set; }
    public IEnumerable<Message> UserMessages { get; set; }
}
