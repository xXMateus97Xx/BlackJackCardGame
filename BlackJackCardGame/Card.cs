using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BlackJackCardGame;

readonly struct Card(CardRank rank, CardSuit suit)
{
    static ReadOnlySpan<char> CardRanks => "A23456789TJQK";

    public CardRank Rank { get; } = rank;
    public CardSuit Suit { get; } = suit;

    public int IntegerValue => ((byte)Rank & 0xF0) >> 4;

    public bool IsDefault => Rank == default;

#if DEBUG
    public override string ToString() => $"{CardRanks[(byte)Rank & 0xF]}{(char)Suit}";
#endif

    public void WriteToSpan(Span<char> destination)
    {
        if (destination.Length > 1)
        {
            ref var ranks = ref MemoryMarshal.GetReference(CardRanks);
            destination[0] = Unsafe.Add(ref ranks, (byte)Rank & 0xF);
            destination[1] = (char)Suit;
        }
    }
}

/* Os 4 bits mais significativos guardam o valor inteiro da carta
   Os 4 bits menos significativos guardam a posição da letra no CardRanks */
enum CardRank : byte
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

enum CardSuit : byte
{
    Hearts = (byte)'H',
    Diamonds = (byte)'D',
    Clubs = (byte)'C',
    Spades = (byte)'S',
}