using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortalk___Back_End.Models;
using Shortalk___Back_End.Services.Context;
using System.Text.Json;

namespace Shortalk___Back_End.Services;
public class GameService
{
    private readonly DataContext _context;

    public GameService(DataContext context)
    {
        _context = context;
    }

    public GameModel GetGameByLobbyName(string lobbyName)
    {
        return _context.GameInfo.SingleOrDefault(game => game.LobbyName == lobbyName);
    }

    public CardModel GetCard()
    {
        string json = File.ReadAllText("words.json");
        List<CardChoiceModel> data = JsonSerializer.Deserialize<List<CardChoiceModel>>(json);

        Random random = new Random();
        int index = random.Next(0, data.Count);


        CardChoiceModel randomCardChoice = data[index];

        Random random2 = new Random();
        int indexj = random2.Next(0, randomCardChoice.Bottom.Count);

        CardModel card = new CardModel();

        int coinFlip = random.Next(0, 2);

        switch (coinFlip)
        {
            case 0:
                card.TopWord = randomCardChoice.Top;
                card.BottomWord = randomCardChoice.Top + " " + randomCardChoice.Bottom[indexj];
                break;
            case 1:
                card.TopWord = randomCardChoice.Bottom[indexj];
                card.BottomWord = randomCardChoice.Top + " " + randomCardChoice.Bottom[indexj];
                break;
        }

        return card;

    }

    public bool AddGame(LobbyRoomModel lobby)
    {

        GameModel newGame = new GameModel();
        CardModel card = GetCard();

        // newGame.ID = lobby.ID;
        newGame.LobbyName = lobby.LobbyName;
        newGame.Host = lobby.Host;
        newGame.NumberOfRounds = lobby.NumberOfRounds;
        newGame.TimeLimit = lobby.TimeLimit;
        newGame.TeamMemberA1 = lobby.TeamMemberA1;
        newGame.TeamMemberA2 = lobby.TeamMemberA2;
        newGame.TeamMemberA3 = lobby.TeamMemberA3;
        newGame.TeamMemberA4 = lobby.TeamMemberA4;
        newGame.TeamMemberA5 = lobby.TeamMemberA5;
        newGame.TeamMemberB1 = lobby.TeamMemberB1;
        newGame.TeamMemberB2 = lobby.TeamMemberB2;
        newGame.TeamMemberB3 = lobby.TeamMemberB3;
        newGame.TeamMemberB4 = lobby.TeamMemberB4;
        newGame.TeamMemberB5 = lobby.TeamMemberB5;
        newGame.Turn = 1;
        newGame.Speaker = lobby.TeamMemberA1;
        newGame.OnePointWord = card.TopWord;
        newGame.ThreePointWord = card.BottomWord;
        newGame.Team1Score = 0;
        newGame.Team2Score = 0;

        _context.Add(newGame);

        bool result = _context.SaveChanges() != 0;

        return result;
    }

    public GameModel GetGameInfo(string lobbyName)
    {
        return _context.GameInfo.SingleOrDefault(game => game.LobbyName == lobbyName);
    }

    public bool GetNextCard(string lobbyName)
    {

        GameModel game = GetGameByLobbyName(lobbyName);

        CardModel card = GetCard();

        game.OnePointWord = card.TopWord;
        game.ThreePointWord = card.BottomWord; 

        _context.Update<GameModel>(game);
        bool result = _context.SaveChanges() != 0;

        return result;

    }
}
