namespace BlackJackCardGame;

struct Xorshift32
{
    private uint _state;

    public static Xorshift32 Create()
    {
        return new Xorshift32((uint)DateTime.UtcNow.Ticks);
    }

    public Xorshift32(uint seed)
    {
        _state = seed;
    }

    public int Next()
    {
        var x = _state;
        x ^= x << 13;
        x ^= x >> 17;
        x ^= x << 5;
        _state = x;
        return Math.Abs((int)(x * 0x9E3779B1));
    }

    public int Next(int maxValue)
    {
        return Next() % maxValue;
    }
}