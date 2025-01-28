using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace BlackJackCardGame;

class Deck
{
    private readonly Card[] _cards;
    private int _cardPosition;

    public Deck()
    {
        _cards =
        [
            new Card(CardRank.A, CardSuit.Clubs),
            new Card(CardRank.Two, CardSuit.Clubs),
            new Card(CardRank.Three, CardSuit.Clubs),
            new Card(CardRank.Four, CardSuit.Clubs),
            new Card(CardRank.Five, CardSuit.Clubs),
            new Card(CardRank.Six, CardSuit.Clubs),
            new Card(CardRank.Seven, CardSuit.Clubs),
            new Card(CardRank.Eight, CardSuit.Clubs),
            new Card(CardRank.Nine, CardSuit.Clubs),
            new Card(CardRank.Ten, CardSuit.Clubs),
            new Card(CardRank.J, CardSuit.Clubs),
            new Card(CardRank.Q, CardSuit.Clubs),
            new Card(CardRank.K, CardSuit.Clubs),
            new Card(CardRank.A, CardSuit.Diamonds),
            new Card(CardRank.Two, CardSuit.Diamonds),
            new Card(CardRank.Three, CardSuit.Diamonds),
            new Card(CardRank.Four, CardSuit.Diamonds),
            new Card(CardRank.Five, CardSuit.Diamonds),
            new Card(CardRank.Six, CardSuit.Diamonds),
            new Card(CardRank.Seven, CardSuit.Diamonds),
            new Card(CardRank.Eight, CardSuit.Diamonds),
            new Card(CardRank.Nine, CardSuit.Diamonds),
            new Card(CardRank.Ten, CardSuit.Diamonds),
            new Card(CardRank.J, CardSuit.Diamonds),
            new Card(CardRank.Q, CardSuit.Diamonds),
            new Card(CardRank.K, CardSuit.Diamonds),
            new Card(CardRank.A, CardSuit.Spades),
            new Card(CardRank.Two, CardSuit.Spades),
            new Card(CardRank.Three, CardSuit.Spades),
            new Card(CardRank.Four, CardSuit.Spades),
            new Card(CardRank.Five, CardSuit.Spades),
            new Card(CardRank.Six, CardSuit.Spades),
            new Card(CardRank.Seven, CardSuit.Spades),
            new Card(CardRank.Eight, CardSuit.Spades),
            new Card(CardRank.Nine, CardSuit.Spades),
            new Card(CardRank.Ten, CardSuit.Spades),
            new Card(CardRank.J, CardSuit.Spades),
            new Card(CardRank.Q, CardSuit.Spades),
            new Card(CardRank.K, CardSuit.Spades),
            new Card(CardRank.A, CardSuit.Hearts),
            new Card(CardRank.Two, CardSuit.Hearts),
            new Card(CardRank.Three, CardSuit.Hearts),
            new Card(CardRank.Four, CardSuit.Hearts),
            new Card(CardRank.Five, CardSuit.Hearts),
            new Card(CardRank.Six, CardSuit.Hearts),
            new Card(CardRank.Seven, CardSuit.Hearts),
            new Card(CardRank.Eight, CardSuit.Hearts),
            new Card(CardRank.Nine, CardSuit.Hearts),
            new Card(CardRank.Ten, CardSuit.Hearts),
            new Card(CardRank.J, CardSuit.Hearts),
            new Card(CardRank.Q, CardSuit.Hearts),
            new Card(CardRank.K, CardSuit.Hearts),
        ];
    }

    public (bool Success, Card Card) TryPickCard()
    {
        var pos = _cardPosition;
        var cards = _cards;
        if (pos < cards.Length)
        {
            ref var cardsRef = ref MemoryMarshal.GetArrayDataReference(cards);
            _cardPosition++;
            return (true, Unsafe.Add(ref cardsRef, pos));
        }

        return (false, default);
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
        var sb = new StringBuilder();

        for (var i = 0; i < _cards.Length; i++)
            sb.AppendLine(_cards[i].ToString());

        return sb.ToString();
    }
#endif
}