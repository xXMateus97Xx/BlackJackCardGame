using System.Runtime.CompilerServices;

namespace BlackJackCardGame;

struct Xorshift32(uint seed)
{
    private uint _state = seed;

    public static Xorshift32 Create()
    {
        return new Xorshift32((uint)DateTime.UtcNow.Ticks);
    }

    public int Next() => NextCore();

    public int Next(int maxValue)
    {
        return NextCore() % maxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int NextCore()
    {
        var x = _state;
        x ^= x << 13;
        x ^= x >> 17;
        x ^= x << 5;
        _state = x;
        var v = (int)(x * 0x9E3779B1);
        if (v == int.MinValue)
            return int.MaxValue;
        return v < 0 ? -v : v;
    }
}