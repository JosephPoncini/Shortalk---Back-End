

using System.Collections.Concurrent;
using Shortalk___Back_End.Models;

namespace Shortalk___Back_End.Services;
public class SharedDb
{
    private readonly ConcurrentDictionary<string, UserConnection> _connections = new();

    public ConcurrentDictionary<string, UserConnection> connections => _connections;

}
