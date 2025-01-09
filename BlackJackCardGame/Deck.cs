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
            new Card('A', CardSuit.Clubs),
            new Card('2', CardSuit.Clubs),
            new Card('3', CardSuit.Clubs),
            new Card('4', CardSuit.Clubs),
            new Card('5', CardSuit.Clubs),
            new Card('6', CardSuit.Clubs),
            new Card('7', CardSuit.Clubs),
            new Card('8', CardSuit.Clubs),
            new Card('9', CardSuit.Clubs),
            new Card('T', CardSuit.Clubs),
            new Card('J', CardSuit.Clubs),
            new Card('Q', CardSuit.Clubs),
            new Card('K', CardSuit.Clubs),
            new Card('A', CardSuit.Diamonds),
            new Card('2', CardSuit.Diamonds),
            new Card('3', CardSuit.Diamonds),
            new Card('4', CardSuit.Diamonds),
            new Card('5', CardSuit.Diamonds),
            new Card('6', CardSuit.Diamonds),
            new Card('7', CardSuit.Diamonds),
            new Card('8', CardSuit.Diamonds),
            new Card('9', CardSuit.Diamonds),
            new Card('T', CardSuit.Diamonds),
            new Card('J', CardSuit.Diamonds),
            new Card('Q', CardSuit.Diamonds),
            new Card('K', CardSuit.Diamonds),
            new Card('A', CardSuit.Spades),
            new Card('2', CardSuit.Spades),
            new Card('3', CardSuit.Spades),
            new Card('4', CardSuit.Spades),
            new Card('5', CardSuit.Spades),
            new Card('6', CardSuit.Spades),
            new Card('7', CardSuit.Spades),
            new Card('8', CardSuit.Spades),
            new Card('9', CardSuit.Spades),
            new Card('T', CardSuit.Spades),
            new Card('J', CardSuit.Spades),
            new Card('Q', CardSuit.Spades),
            new Card('K', CardSuit.Spades),
            new Card('A', CardSuit.Hearts),
            new Card('2', CardSuit.Hearts),
            new Card('3', CardSuit.Hearts),
            new Card('4', CardSuit.Hearts),
            new Card('5', CardSuit.Hearts),
            new Card('6', CardSuit.Hearts),
            new Card('7', CardSuit.Hearts),
            new Card('8', CardSuit.Hearts),
            new Card('9', CardSuit.Hearts),
            new Card('T', CardSuit.Hearts),
            new Card('J', CardSuit.Hearts),
            new Card('Q', CardSuit.Hearts),
            new Card('K', CardSuit.Hearts),
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