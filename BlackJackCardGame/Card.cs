namespace BlackJackCardGame;

readonly struct Card
{
    static ReadOnlySpan<char> CardRanks => "A23456789TJQK";

    public Card(char rank, CardSuit suit)
    {
        if (!CardRanks.Contains(rank))
            throw new ArgumentException($"Invalid rank {rank}");

        if (suit != CardSuit.Clubs && suit != CardSuit.Diamonds && suit != CardSuit.Hearts && suit != CardSuit.Spades)
            throw new ArgumentException($"Invalid suit {suit}");

        Rank = rank;
        Suit = suit;
    }

    public char Rank { get; }
    public CardSuit Suit { get; }

    public int IntegerValue => Math.Min(CardRanks.IndexOf(Rank) + 1, 10);

    public override string ToString() => $"{Rank}{(char)Suit}";

    public void WriteToSpan(Span<char> destination)
    {
        destination[1] = (char)Suit;
        destination[0] = Rank;
    }
}

enum CardSuit : short
{
    Hearts = (short)'H',
    Diamonds = (short)'D',
    Clubs = (short)'C',
    Spades = (short)'S',
}