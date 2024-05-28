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

    public bool DoesGameExist(string lobbyName)
    {
        return _context.GameInfo.SingleOrDefault(lobby => lobby.LobbyName == lobbyName) != null;
    }

    public bool DeleteGame(string gameToDelete)
    {
        GameModel foundGame = GetGameByLobbyName(gameToDelete);

        bool result = false;

        if (foundGame != null)
        {

            _context.Remove<GameModel>(foundGame);
            result = _context.SaveChanges() != 0;
        }

        return result;
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
        bool res = DoesGameExist(lobby.LobbyName);
        if (res)
        {
            return false;
        }

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
        newGame.OnePointWordHasBeenSaid = false;
        newGame.ThreePointWordHasBeenSaid = false;
        newGame.BuzzWords = string.Empty;
        newGame.SkippedWords = string.Empty;
        newGame.OnePointWords = string.Empty;
        newGame.ThreePointWords = string.Empty;

        _context.Add(newGame);

        bool result = _context.SaveChanges() != 0;
        Console.WriteLine(card.TopWord);
        Console.WriteLine(card.BottomWord);

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
        game.OnePointWordHasBeenSaid = false;
        game.ThreePointWordHasBeenSaid = false;

        _context.Update<GameModel>(game);
        bool result = _context.SaveChanges() != 0;

        return result;

    }

    public bool IsGuessOnePoint(string lobbyName, string onePointWord, string guess)
    {
        bool result = RemoveSpacesAndLowercase(onePointWord) == RemoveSpacesAndLowercase(guess);

        if (result)
        {
            GameModel game = GetGameByLobbyName(lobbyName);
            game.OnePointWordHasBeenSaid = true;
            _context.Update<GameModel>(game);
            result = _context.SaveChanges() != 0;
        }

        return result;
    }

    public bool IsGuessThreePoint(string lobbyName, string threePointWord, string guess)
    {
        bool result = RemoveSpacesAndLowercase(threePointWord) == RemoveSpacesAndLowercase(guess);

        if (result)
        {
            GameModel game = GetGameByLobbyName(lobbyName);
            game.ThreePointWordHasBeenSaid = true;
            _context.Update<GameModel>(game);
            result = _context.SaveChanges() != 0;
        }

        return result;
    }

    public bool IsGuessClose(string onePointWord, string threePointWord, string guess)
    {
        bool result = AreStringsOffByOneChar(RemoveSpacesAndLowercase(onePointWord), RemoveSpacesAndLowercase(guess)) || AreStringsOffByOneChar(RemoveSpacesAndLowercase(threePointWord), RemoveSpacesAndLowercase(guess));

        return result;
    }

    public static string RemoveSpacesAndLowercase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        // Remove all spaces and convert to lowercase
        return new string(input.Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();
    }

    public bool AreStringsOffByOneChar(string str1, string str2)
    {
        if (str1 == null || str2 == null)
        {
            throw new ArgumentNullException("Input strings cannot be null");
        }

        // If lengths differ by more than 1, they cannot be off by just one char
        if (Math.Abs(str1.Length - str2.Length) > 1)
        {
            return false;
        }

        // If the lengths are the same, check for one differing character
        if (str1.Length == str2.Length)
        {
            int mismatchCount = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                {
                    mismatchCount++;
                    if (mismatchCount > 1)
                    {
                        return false;
                    }
                }
            }

            return mismatchCount == 1;
        }

        // If lengths differ by exactly 1, check for insert/remove case
        if (str1.Length > str2.Length)
        {
            return IsOneEditDistance(str2, str1);
        }
        else
        {
            return IsOneEditDistance(str1, str2);
        }
    }

    private static bool IsOneEditDistance(string shorter, string longer)
    {
        int i = 0, j = 0;

        while (i < shorter.Length && j < longer.Length)
        {
            if (shorter[i] != longer[j])
            {
                // If there is a mismatch, move the pointer of the longer string
                if (i != j)
                {
                    return false;
                }
                j++;
            }
            else
            {
                i++;
                j++;
            }
        }

        return true;
    }

    public bool ChangeScore(string lobbyName, string Team, int point)
    {


        GameModel game = GetGameByLobbyName(lobbyName);

        switch (Team)
        {
            case "Team1":
                game.Team1Score += point;
                break;
            case "Team2":
                game.Team2Score += point;
                break;
            default:
                return false;
        }

        _context.Update<GameModel>(game);
        bool result = _context.SaveChanges() != 0;

        return result;

    }

    public bool AppendBuzzWords(string lobbyName, string buzzWordTop, string buzzWordBottom)
    {
        GameModel game = GetGameByLobbyName(lobbyName);

        if (game.BuzzWords == "")
        {
            game.BuzzWords = $"{buzzWordTop}-{buzzWordBottom}";
        }
        else
        {
            game.BuzzWords += "," + $"{buzzWordTop}-{buzzWordBottom}";
        }

        _context.Update<GameModel>(game);
        bool result = _context.SaveChanges() != 0;

        return result;
    }

    public bool AppendOnePointWords(string lobbyName, string onePointWordTop, string onePointWordBottom)
    {
        GameModel game = GetGameByLobbyName(lobbyName);

        if (game.OnePointWords == "")
        {
            game.OnePointWords = $"{onePointWordTop}-{onePointWordBottom}";
        }
        else
        {
            game.OnePointWords += "," + $"{onePointWordTop}-{onePointWordBottom}";
        }

        _context.Update<GameModel>(game);
        bool result = _context.SaveChanges() != 0;

        return result;
    }

    public bool AppendThreePointWords(string lobbyName, string threePointWordTop, string threePointWordBottom)
    {
        GameModel game = GetGameByLobbyName(lobbyName);

        if (game.ThreePointWords == "")
        {
            game.ThreePointWords = $"{threePointWordTop}-{threePointWordBottom}";
        }
        else
        {
            game.ThreePointWords += "," + $"{threePointWordTop}-{threePointWordBottom}";
        }

        _context.Update<GameModel>(game);
        bool result = _context.SaveChanges() != 0;

        return result;
    }

    public bool AppendSkipPointWords(string lobbyName, string skipWordTop, string skipWordBottom)
    {
        GameModel game = GetGameByLobbyName(lobbyName);

        if (game.SkippedWords == "")
        {
            game.SkippedWords = $"{skipWordTop}-{skipWordBottom}";
        }
        else
        {
            game.SkippedWords += "," + $"{skipWordTop}-{skipWordBottom}";
        }

        _context.Update<GameModel>(game);
        bool result = _context.SaveChanges() != 0;

        return result;
    }

    public bool GoToNextTurn(string lobbyName)
    {
        GameModel game = GetGameByLobbyName(lobbyName);
        game.Turn += 1;

        _context.Update<GameModel>(game);
        bool result = _context.SaveChanges() != 0;

        return result;
    }

    public bool UpdateSpeaker(string lobbyName)
    {
        GameModel game = GetGameByLobbyName(lobbyName);

        string[] team = new string[5];

        switch (game.Turn % 2)
        {
            case 0:
                team[0] = game.TeamMemberB1;
                team[1] = game.TeamMemberB2;
                team[2] = game.TeamMemberB3;
                team[3] = game.TeamMemberB4;
                team[4] = game.TeamMemberB5;
                break;
            case 1:
                team[0] = game.TeamMemberA1;
                team[1] = game.TeamMemberA2;
                team[2] = game.TeamMemberA3;
                team[3] = game.TeamMemberA4;
                team[4] = game.TeamMemberA5;
                break;
        }

        int numberOfTeamates = 0;
        for (int i = 0; i < team.Length; i++)
        {
            if (team[i] != "")
            {
                numberOfTeamates++;
            }
        }

        int index = (((game.Turn - 1) / 2) % numberOfTeamates);

        game.Speaker = team[index];

        _context.Update<GameModel>(game);
        bool result = _context.SaveChanges() != 0;

        return result;
    }

    public bool ClearWordLists(string lobbyName)
    {
        GameModel game = GetGameByLobbyName(lobbyName);
        game.BuzzWords = "";
        game.SkippedWords = "";
        game.ThreePointWords = "";
        game.OnePointWords = "";

        _context.Update<GameModel>(game);
        bool result = _context.SaveChanges() != 0;

        return result;
    }


}
