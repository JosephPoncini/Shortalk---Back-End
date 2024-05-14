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

    public CardModel getCard()
    {
        // string json = File.ReadAllText("words.json");
        // List<CardChoiceModel> cardChoices = JsonSerializer.Deserialize<List<CardChoiceModel>>(json)!;

        // Console.WriteLine(cardChoices);

        // Random random = new Random();
        // int index = random.Next(0, cardChoices.Count);

        // CardChoiceModel randomCardChoice = cardChoices[index];

        // int indexj = random.Next(0, randomCardChoice.BottomWords.Count);

        // CardModel card = new CardModel();

        // int coinFlip = random.Next(0, 2);

        // switch (coinFlip)
        // {
        //     case 0:
        //         card.TopWord = randomCardChoice.TopWord;
        //         card.BottomWord = randomCardChoice.TopWord + randomCardChoice.BottomWords[indexj];
        //         break;
        //     case 1:
        //         card.TopWord = randomCardChoice.BottomWords[indexj];
        //         card.BottomWord = randomCardChoice.TopWord + randomCardChoice.BottomWords[indexj];
        //         break;
        // }

        CardModel card = new CardModel();

        card.TopWord = "Code";
        card.BottomWord = "Code Stack";
        return card;

    }

    public bool AddGame(LobbyRoomModel lobby)
    {

        GameModel newGame = new GameModel();
        CardModel card = getCard();

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
}
