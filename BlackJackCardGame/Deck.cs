using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BlackJackCardGame;

class Deck
{
    private readonly Card[] _cards;
    private int _cardPosition;

    public Deck()
    {
        _cards =
        [
            new(CardRank.A, CardSuit.Clubs),
            new(CardRank.Two, CardSuit.Clubs),
            new(CardRank.Three, CardSuit.Clubs),
            new(CardRank.Four, CardSuit.Clubs),
            new(CardRank.Five, CardSuit.Clubs),
            new(CardRank.Six, CardSuit.Clubs),
            new(CardRank.Seven, CardSuit.Clubs),
            new(CardRank.Eight, CardSuit.Clubs),
            new(CardRank.Nine, CardSuit.Clubs),
            new(CardRank.Ten, CardSuit.Clubs),
            new(CardRank.J, CardSuit.Clubs),
            new(CardRank.Q, CardSuit.Clubs),
            new(CardRank.K, CardSuit.Clubs),
            new(CardRank.A, CardSuit.Diamonds),
            new(CardRank.Two, CardSuit.Diamonds),
            new(CardRank.Three, CardSuit.Diamonds),
            new(CardRank.Four, CardSuit.Diamonds),
            new(CardRank.Five, CardSuit.Diamonds),
            new(CardRank.Six, CardSuit.Diamonds),
            new(CardRank.Seven, CardSuit.Diamonds),
            new(CardRank.Eight, CardSuit.Diamonds),
            new(CardRank.Nine, CardSuit.Diamonds),
            new(CardRank.Ten, CardSuit.Diamonds),
            new(CardRank.J, CardSuit.Diamonds),
            new(CardRank.Q, CardSuit.Diamonds),
            new(CardRank.K, CardSuit.Diamonds),
            new(CardRank.A, CardSuit.Spades),
            new(CardRank.Two, CardSuit.Spades),
            new(CardRank.Three, CardSuit.Spades),
            new(CardRank.Four, CardSuit.Spades),
            new(CardRank.Five, CardSuit.Spades),
            new(CardRank.Six, CardSuit.Spades),
            new(CardRank.Seven, CardSuit.Spades),
            new(CardRank.Eight, CardSuit.Spades),
            new(CardRank.Nine, CardSuit.Spades),
            new(CardRank.Ten, CardSuit.Spades),
            new(CardRank.J, CardSuit.Spades),
            new(CardRank.Q, CardSuit.Spades),
            new(CardRank.K, CardSuit.Spades),
            new(CardRank.A, CardSuit.Hearts),
            new(CardRank.Two, CardSuit.Hearts),
            new(CardRank.Three, CardSuit.Hearts),
            new(CardRank.Four, CardSuit.Hearts),
            new(CardRank.Five, CardSuit.Hearts),
            new(CardRank.Six, CardSuit.Hearts),
            new(CardRank.Seven, CardSuit.Hearts),
            new(CardRank.Eight, CardSuit.Hearts),
            new(CardRank.Nine, CardSuit.Hearts),
            new(CardRank.Ten, CardSuit.Hearts),
            new(CardRank.J, CardSuit.Hearts),
            new(CardRank.Q, CardSuit.Hearts),
            new(CardRank.K, CardSuit.Hearts),
        ];
    }

    public Card PickCard()
    {
        var pos = _cardPosition;
        var cards = _cards;
        if (pos < cards.Length)
        {
            ref var cardsRef = ref MemoryMarshal.GetArrayDataReference(cards);
            _cardPosition++;
            return Unsafe.Add(ref cardsRef, pos);
        }

        return default;
    }

    public void Reset()
    {
        _cardPosition = 0;
    }

    public void Shuffle()
    {
        var random = Xorshift32.Create();
        var cards = _cards;
        ref var cardsRef = ref MemoryMarshal.GetArrayDataReference(cards);
        for (var i = 0; i < cards.Length; i++)
        {
            var pos = random.Next(cards.Length);
            var c1 = Unsafe.Add(ref cardsRef, i);
            Unsafe.Add(ref cardsRef, i) = Unsafe.Add(ref cardsRef, pos);
            Unsafe.Add(ref cardsRef, pos) = c1;
        }
    }

#if DEBUG
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();

        for (var i = 0; i < _cards.Length; i++)
            sb.AppendLine(_cards[i].ToString());

        return sb.ToString();
    }
#endif
}