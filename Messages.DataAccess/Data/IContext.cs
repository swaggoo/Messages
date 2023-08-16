using Messages.Models;
using System.Collections.Concurrent;

namespace Messages.Data;

public interface IContext
{
    ConcurrentDictionary<string, ConcurrentQueue<Message>> UserMessages { get; }
    ConcurrentQueue<Message> AllUsersMessages { get; }
}
