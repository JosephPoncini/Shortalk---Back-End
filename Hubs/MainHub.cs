using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Shortalk___Back_End.Models;
using Shortalk___Back_End.Services;

namespace Shortalk___Back_End.Hubs;
public class MainHub : Hub
{
    private readonly SharedDb _shared;

    private readonly HomeService _data;

    public MainHub(SharedDb shared, HomeService data)
    {
        _shared = shared;
        _data = data;
    }

}