using Messages.Models;

namespace Messages.IRepository;

public interface IMessageRepository
{
    IEnumerable<Message> GetAllMessages();
    IEnumerable<Message> GetUserMessages(string userId);
    void AddMessage(Message message);
}
