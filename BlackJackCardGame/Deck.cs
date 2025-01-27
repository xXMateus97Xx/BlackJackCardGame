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
        if (_cardPosition < _cards.Length)
            return (true, _cards[_cardPosition++]);

        return (false, default);
    }

    public void Reset()
    {
        _cardPosition = 0;
    }

    public void Shuffle()
    {
        var random = Xorshift32.Create();
        for (var i = 0; i < _cards.Length; i++)
        {
            var pos = random.Next(_cards.Length);
            (_cards[i], _cards[pos]) = (_cards[pos], _cards[i]);
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (var i = 0; i < _cards.Length; i++)
            sb.AppendLine(_cards[i].ToString());

        return sb.ToString();
    }
}