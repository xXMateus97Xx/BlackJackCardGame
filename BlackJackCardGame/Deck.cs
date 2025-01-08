using System.Text;

namespace BlackJackCardGame;

class Deck
{
    private readonly Card[] _cards;
    private int _cardPosition;

    public Deck()
    {
        _cards = [
            new Card("AC"),
            new Card("2C"),
            new Card("3C"),
            new Card("4C"),
            new Card("5C"),
            new Card("6C"),
            new Card("7C"),
            new Card("8C"),
            new Card("9C"),
            new Card("TC"),
            new Card("JC"),
            new Card("QC"),
            new Card("KC"),
            new Card("AD"),
            new Card("2D"),
            new Card("3D"),
            new Card("4D"),
            new Card("5D"),
            new Card("6D"),
            new Card("7D"),
            new Card("8D"),
            new Card("9D"),
            new Card("TD"),
            new Card("JD"),
            new Card("QD"),
            new Card("KD"),
            new Card("AS"),
            new Card("2S"),
            new Card("3S"),
            new Card("4S"),
            new Card("5S"),
            new Card("6S"),
            new Card("7S"),
            new Card("8S"),
            new Card("9S"),
            new Card("TS"),
            new Card("JS"),
            new Card("QS"),
            new Card("KS"),
            new Card("AH"),
            new Card("2H"),
            new Card("3H"),
            new Card("4H"),
            new Card("5H"),
            new Card("6H"),
            new Card("7H"),
            new Card("8H"),
            new Card("9H"),
            new Card("TH"),
            new Card("JH"),
            new Card("QH"),
            new Card("KH")
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
        var random = new Random();
        for (var i = 0; i < _cards.Length; i++)
        {
            var pos = random.Next(0, _cards.Length);
            (_cards[i], _cards[pos]) = (_cards[pos], _cards[i]);
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (var i = 0; i < _cards.Length; i++)
        {
            ref var card = ref _cards[i];
            sb.AppendLine(card.ToString());
        }

        return sb.ToString();
    }
}

