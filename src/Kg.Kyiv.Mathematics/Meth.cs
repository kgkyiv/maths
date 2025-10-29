using System.Runtime.CompilerServices;

namespace Kg.Kyiv.Math;

public static class Meth
{
    // this is scuffed because we can't define Half constants
    private static readonly Half HalfRadiansPerDegree = Half.Pi / (Half)180.0f;

    // Meth.Min >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Min(sbyte x, sbyte y) => x < y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Min(byte x, byte y) => x < y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Min(short x, short y) => x < y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Min(ushort x, ushort y) => x < y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Min(int x, int y) => x < y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Min(uint x, uint y) => x < y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Min(long x, long y) => x < y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Min(ulong x, ulong y) => x < y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half Min(Half x, Half y) => x < y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Min(float x, float y) => x < y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Min(double x, double y) => x < y ? x : y;
    // <<

    // Meth.Max >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Max(sbyte x, sbyte y) => x > y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Max(byte x, byte y) => x > y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Max(short x, short y) => x > y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Max(ushort x, ushort y) => x > y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Max(int x, int y) => x > y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Max(uint x, uint y) => x > y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Max(long x, long y) => x > y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Max(ulong x, ulong y) => x > y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half Max(Half x, Half y) => x > y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Max(float x, float y) => x > y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Max(double x, double y) => x > y ? x : y;
    // <<

    // Math.Clamp >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Clamp(sbyte x, sbyte min, sbyte max) => x < min ? min : x > max ? max : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Clamp(byte x, byte min, byte max) => x < min ? min : x > max ? max : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Clamp(short x, short min, short max) => x < min ? min : x > max ? max : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Clamp(ushort x, ushort min, ushort max) => x < min ? min : x > max ? max : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Clamp(int x, int min, int max) => x < min ? min : x > max ? max : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Clamp(uint x, uint min, uint max) => x < min ? min : x > max ? max : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Clamp(long x, long min, long max) => x < min ? min : x > max ? max : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Clamp(ulong x, ulong min, ulong max) => x < min ? min : x > max ? max : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half Clamp(Half x, Half min, Half max) => x < min ? min : x > max ? max : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(float x, float min, float max) => x < min ? min : x > max ? max : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Clamp(double x, double min, double max) => x < min ? min : x > max ? max : x;
    // <<

    // honestly these could be replaced by their bcl counterparts (float.DegreesToRadians, etc.)
    // Meth.DegreesToRadians >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half DegreesToRadians(Half x)
    {
        // System.Half is a joke type
        return (Half)((float)x * (float.Pi / 180.0f));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float DegreesToRadians(float x)
    {
        // also `π/180` is getting evaluated by compiler so there's no need for constants
        // idk why i thought that it actually evaluates this at runtime for so long
        return x * (float.Pi / 180.0f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DegreesToRadians(double x)
    {
        return x * (double.Pi / 180.0);
    }
    // <<

    // Meth.RadiansToDegrees >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half RadiansToDegrees(Half x)
    {
        return (Half)((float)x * (180.0f / float.Pi));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float RadiansToDegrees(float x)
    {
        return x * (180.0f / float.Pi);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double RadiansToDegrees(double x)
    {
        return x * (180.0 / double.Pi);
    }
    // <<

    // Meth.Saturate >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half Saturate(Half x) => x < Half.Zero ? Half.Zero : x > Half.One ? Half.One : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Saturate(float x) => x < 0.0f ? 0.0f : x > 1.0f ? 1.0f : x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Saturate(double x) => x < 0.0 ? 0.0 : x > 1.0 ? 1.0 : x;
    // <<

    // blazing fast oc tuner
    // Meth.FloorToInt >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FloorToInt(Half x) => (int)x - (x < Half.Zero ? 1 : 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FloorToInt(float x) => (int)x - (x < 0.0f ? 1 : 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FloorToInt(double x) => (int)x - (x < 0.0 ? 1 : 0);
    // <<

    // Meth.CeilingToInt >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CeilingToInt(Half x)
    {
        int ix = (int)x;
        return ix + ((Half)ix < x ? 1 : 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CeilingToInt(float x)
    {
        int ix = (int)x;
        return ix + (ix < x ? 1 : 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CeilingToInt(double x)
    {
        int ix = (int)x;
        return ix + (ix < x ? 1 : 0);
    }
    // <<

    // Meth.IsZero >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(Half x) => Half.Abs(x) < Half.Epsilon;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(float x) => float.Abs(x) < float.Epsilon;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(double x) => double.Abs(x) < double.Epsilon;
    // <<

    // Meth.SafeDiv >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half SafeDiv(Half x, Half y) => IsZero(y) ? Half.PositiveInfinity : x / y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SafeDiv(float x, float y) => IsZero(y) ? float.PositiveInfinity : x / y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SafeDiv(double x, double y) => IsZero(y) ? double.PositiveInfinity : x / y;
    // <<

    // Meth.Lerp >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half Lerp(Half x, Half y, Half t)
    {
        // joke type
        float fx = (float)x;
        return (Half)(fx + ((float)y - fx) * (float)t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp(float x, float y, float t) => x + (y - x) * t;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Lerp(double x, double y, double t) => x + (y - x) * t;
    // <<

    // Meth.EuclideanMod >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half EuclideanMod(Half x, Half y)
    {
        float fx = (float)x, fy = (float)y, r = fx % fy;
        return (Half)(r < 0.0f ? r + fy : r);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float EuclideanMod(float x, float y)
    {
        float r = x % y;
        return r < 0.0f ? r + y : r;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double EuclideanMod(double x, double y)
    {
        double r = x % y;
        return r < 0.0 ? r + y : r;
    }
    // <<

    // Meth.WrapDegrees >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Half WrapDegrees(Half x) => EuclideanMod(x, Unsafe.BitCast<short, Half>(0x5DA0) /* 360.0f */);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float WrapDegrees(float x) => EuclideanMod(x, 360.0f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double WrapDegrees(double x) => EuclideanMod(x, 360.0);
    // <<
}