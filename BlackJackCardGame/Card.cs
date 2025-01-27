using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BlackJackCardGame;

readonly struct Card
{
    static ReadOnlySpan<char> CardRanks => "A23456789TJQK";

    public Card(CardRank rank, CardSuit suit)
    {
        if (rank != CardRank.A && rank != CardRank.Two && rank != CardRank.Three && rank != CardRank.Four &&
            rank != CardRank.Five && rank != CardRank.Six && rank != CardRank.Seven && rank != CardRank.Eight &&
            rank != CardRank.Nine && rank != CardRank.Ten && rank != CardRank.J && rank != CardRank.Q &&
            rank != CardRank.K)
            throw new ArgumentException($"Invalid rank {rank}");

        if (suit != CardSuit.Clubs && suit != CardSuit.Diamonds && suit != CardSuit.Hearts && suit != CardSuit.Spades)
            throw new ArgumentException($"Invalid suit {suit}");

        Rank = rank;
        Suit = suit;
    }

    public CardRank Rank { get; }
    public CardSuit Suit { get; }

    public int IntegerValue => ((short)Rank & 0xF0) >> 4;

    public override string ToString() => $"{CardRanks[(short)Rank & 0xF]}{(char)Suit}";

    public void WriteToSpan(Span<char> destination)
    {
        if (destination.Length > 1)
        {
            ref var ranks = ref MemoryMarshal.GetReference(CardRanks);
            destination[0] = Unsafe.Add(ref ranks, (short)Rank & 0xF);
            destination[1] = (char)Suit;
        }
    }
}

/* Os 4 bits mais significativos guardam o valor inteiro da carta
   Os 4 bits menos significativos guardam a posição da letra no CardRanks */
enum CardRank : short
{
    A = 1 << 4,
    Two = 1 | 2 << 4,
    Three = 2 | 3 << 4,
    Four = 3 | 4 << 4,
    Five = 4 | 5 << 4,
    Six = 5 | 6 << 4,
    Seven = 6 | 7 << 4,
    Eight = 7 | 8 << 4,
    Nine = 8 | 9 << 4,
    Ten = 9 | 10 << 4,
    J = 10 | 10 << 4,
    Q = 11 | 10 << 4,
    K = 12 | 10 << 4,
}

enum CardSuit : short
{
    Hearts = (short)'H',
    Diamonds = (short)'D',
    Clubs = (short)'C',
    Spades = (short)'S',
}