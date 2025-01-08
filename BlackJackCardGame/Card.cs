namespace BlackJackCardGame;

class Card
{
    const string CardRanks = "A23456789TJQK";

    public Card(string card)
    {
        var rank = card[0];
        if (!CardRanks.Contains(rank))
            throw new ArgumentException($"Invalid rank {rank}");

        var suit = (CardSuit)(short)card[1];
        if (!Enum.IsDefined(suit))
            throw new ArgumentException($"Invalid suit {suit}");

        Rank = rank;
        Suit = suit;
    }

    public char Rank { get; }
    public CardSuit Suit { get; }

    public int IntegerValue
    {
        get
        {
            var val = CardRanks.IndexOf(Rank) + 1;
            return val > 10 ? 10 : val;
        }
    }

    public override string ToString() => $"{Rank}{(char)Suit}";
}

enum CardSuit : short
{
    Hearts = (short)'H',
    Diamonds =  (short)'D',
    Clubs = (short)'C',
    Spades = (short)'S',
}