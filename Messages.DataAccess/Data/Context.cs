using Messages.Models;
using System.Collections.Concurrent;

namespace Messages.Data;

public class Context : IContext
{
    private readonly ConcurrentDictionary<string, ConcurrentQueue<Message>> _userMessages;
    private readonly ConcurrentQueue<Message> _allUsersMessages;

    public Context()
    {
        _userMessages = new ConcurrentDictionary<string, ConcurrentQueue<Message>>();
        _allUsersMessages = new ConcurrentQueue<Message>();
    }

    public ConcurrentDictionary<string, ConcurrentQueue<Message>> UserMessages => _userMessages;
    public ConcurrentQueue<Message> AllUsersMessages => _allUsersMessages;
}
