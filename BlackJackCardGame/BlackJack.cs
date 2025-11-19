using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BlackJackCardGame;

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
        _engineCards = new Card[12];
        _userCards = new Card[12];
    }

    public ReadOnlySpan<Card> EngineCards =>
        MemoryMarshal.CreateReadOnlySpan(ref MemoryMarshal.GetArrayDataReference(_engineCards), _engineCardPosition);

    public ReadOnlySpan<Card> UserCards =>
        MemoryMarshal.CreateReadOnlySpan(ref MemoryMarshal.GetArrayDataReference(_userCards), _userCardPosition);

    public void StartGame()
    {
        Reset();

        ref var userCards = ref MemoryMarshal.GetArrayDataReference(_userCards);
        ref var engineCards = ref MemoryMarshal.GetArrayDataReference(_engineCards);

        Unsafe.Add(ref userCards, 0) = _deck.PickCard();
        Unsafe.Add(ref engineCards, 0) = _deck.PickCard();

        Unsafe.Add(ref userCards, 1) = _deck.PickCard();
        Unsafe.Add(ref engineCards, 1) = _deck.PickCard();

        _userCardPosition = _engineCardPosition = 2;
    }

    public RoundState PickCardForUser()
    {
        var card = _deck.PickCard();
        if (card.IsDefault)
            return RoundState.Stop;

        ref var userCards = ref MemoryMarshal.GetArrayDataReference(_userCards);
        Unsafe.Add(ref userCards, _userCardPosition++) = card;
        var userSum = SumCards(ref userCards, _userCardPosition);

        if (userSum > 21)
            return RoundState.Lose;

        return userSum < 21 ? RoundState.Continue : RoundState.Stop;
    }

    public RoundState PickCardForEngine()
    {
        ref var engineCards = ref MemoryMarshal.GetArrayDataReference(_engineCards);
        var engineSum = SumCards(ref engineCards, _engineCardPosition);
        if (engineSum == 21)
            return RoundState.Stop;

        bool pickCard;

        if (engineSum < 17)
        {
            pickCard = true;
        }
        else
        {
            var n = Xorshift32.Create().Next();
            var f = 21 - engineSum + 4;
            pickCard = n % f == 0;
        }

        if (!pickCard)
            return RoundState.Stop;

        var card = _deck.PickCard();
        if (card.IsDefault)
            return RoundState.Stop;

        Unsafe.Add(ref engineCards, _engineCardPosition++) = card;
        engineSum = SumCards(ref engineCards, _engineCardPosition);

        if (engineSum > 21)
            return RoundState.Lose;

        return engineSum < 21 ? RoundState.Continue : RoundState.Stop;
    }

    public GameResult GetResult()
    {
        var userSum = SumCards(ref MemoryMarshal.GetArrayDataReference(_userCards), _userCardPosition);
        var engineSum = SumCards(ref MemoryMarshal.GetArrayDataReference(_engineCards), _engineCardPosition);

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

    private static int SumCards(ref Card cards, int count)
    {
        var result = 0;
        for (var i = 0; i < count; i++)
            result += Unsafe.Add(ref cards, i).IntegerValue;

        return result;
    }

    private void Reset()
    {
        _deck.Reset();
        _deck.Shuffle();

        MemoryMarshal.CreateSpan(ref MemoryMarshal.GetArrayDataReference(_userCards), _userCardPosition).Clear();
        MemoryMarshal.CreateSpan(ref MemoryMarshal.GetArrayDataReference(_engineCards), _engineCardPosition).Clear();

        _userCardPosition = _engineCardPosition = 0;
    }
}

enum GameResult
{
    Win,
    Lose,
    Draw
}

enum RoundState
{
    Continue,
    Stop,
    Lose
}