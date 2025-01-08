﻿namespace BlackJackCardGame;

class BlackJack
{
    private readonly Deck _deck;
    private readonly Card[] _engineCards;
    private readonly Card[] _userCards;
    private int _userCardPosition;
    private int _engineCardPosition;

    public BlackJack()
    {
        _deck = new Deck();
        _engineCards = new Card[26];
        _userCards = new Card[26];
    }

    public ReadOnlyMemory<Card> EngineCards => _engineCards.AsMemory(0,_engineCardPosition);
    public ReadOnlyMemory<Card> UserCards => _userCards.AsMemory(0, _userCardPosition);

    public void StartGame()
    {
        Reset();

        _userCards[0] = _deck.TryPickCard().Card;
        _engineCards[0] = _deck.TryPickCard().Card;

        _userCards[1] = _deck.TryPickCard().Card;
        _engineCards[1] = _deck.TryPickCard().Card;

        _userCardPosition = _engineCardPosition = 2;
    }

    public bool PickCardForUser()
    {
        var (success, card) = _deck.TryPickCard();
        if (!success)
            return false;

        _userCards[_userCardPosition++] = card;
        var userSum = SumCards(_userCards, _userCardPosition);

        return userSum < 21;
    }

    public bool PickCardForEngine()
    {
        var engineSum = 21 - SumCards(_engineCards, _engineCardPosition);
        if (engineSum == 0)
            return false;

        bool pickCard;

        if (engineSum >= 4)
            pickCard = true;
        else
            pickCard = (new Random().Next() & 1) == 0;

        if (!pickCard)
            return true;

        var (success, card) = _deck.TryPickCard();
        if (!success)
            return false;

        _engineCards[_engineCardPosition++] = card;
        engineSum = SumCards(_engineCards, _engineCardPosition);

        return engineSum < 21;
    }

    public GameResult GetResult()
    {
        var userSum = SumCards(_userCards, _userCardPosition);
        var engineSum = SumCards(_engineCards, _engineCardPosition);

        if (userSum > 21 && engineSum > 21)
            return GameResult.Draw;
            
        if (userSum > 21)
            return GameResult.Lose;
        
        if (engineSum > 21)
            return GameResult.Win;

        if (userSum == engineSum)
            return GameResult.Draw;

        return userSum > engineSum ? GameResult.Win : GameResult.Lose;
    }

    private static int SumCards(Card[] cards, int count)
    {
        var result = 0;
        for (var i = 0; i < count; i++)
        {
            var card = cards[i];
            result += card.IntegerValue;
        }

        return result;
    }

    private void Reset()
    {
        _deck.Reset();
        _deck.Shuffle();
        _userCardPosition = _engineCardPosition = 0;

        Array.Clear(_userCards);
        Array.Clear(_engineCards);
    }
}

enum GameResult
{
    Win,
    Lose,
    Draw
}