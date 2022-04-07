namespace CardGame;

class Card
{
    const string CardRanks = "A23456789TJQK";
    const string CardSuits = "HSDC";

    public Card(string card)
    {
        var rank = card[0];
        if (!CardRanks.Contains(rank))
            throw new ArgumentException($"Invalid rank {rank}");

        var suit = card[1];
        if (char.IsLower(suit))
            suit = char.ToUpper(suit);

        if (!CardSuits.Contains(suit))
            throw new ArgumentException($"Invalid suit {suit}");

        Rank = rank;
        Suit = suit;
    }

    public char Rank { get; }
    public char Suit { get; }

    public int IntegerValue => CardRanks.IndexOf(Rank) + 1;

    public override string ToString() => $"{Rank}{Suit}";
}

