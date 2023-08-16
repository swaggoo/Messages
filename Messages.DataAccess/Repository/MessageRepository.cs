using System.Collections.Concurrent;
using Messages.Models;
using Messages.Data;
using Messages.IRepository;
using Messages.Utility;

namespace Messages.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IContext _context;

        public MessageRepository(IContext context)
        {
            _context = context;
        }

        public void AddMessage(Message message)
        {
            if (_context.UserMessages.TryGetValue(message.UserId, out var userMessages))
            {
                userMessages.Enqueue(message);
                while (userMessages.Count > SD.MaxUserMessages)
                {
                    userMessages.TryDequeue(out _);
                }
            }
            else
            {
                userMessages = new ConcurrentQueue<Message>(new[] { message });
                _context.UserMessages.TryAdd(message.UserId, userMessages);
            }

            _context.AllUsersMessages.Enqueue(message);
            while (_context.AllUsersMessages.Count > SD.MaxAllUsersMessages)
            {
                _context.AllUsersMessages.TryDequeue(out _);
            }

        }

        public IEnumerable<Message> GetAllMessages()
        {
            return _context.AllUsersMessages.Reverse().ToList();
        }

        public IEnumerable<Message> GetUserMessages(string userId)
        {
            if (_context.UserMessages.TryGetValue(userId, out var userMessages))
            {
                return userMessages.Reverse().ToList();
            }

            return Enumerable.Empty<Message>();
        }
    }
}
