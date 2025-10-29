using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Kg.Kyiv.Mathematics;

// TODO: ISpanFormattable
[StructLayout(LayoutKind.Sequential, Size = 16)]
public struct Int4 : IEquatable<Int4>, IFormattable
{
    internal const int Count = 4;

    public int X;
    public int Y;
    public int Z;
    public int W;

    public Int4(int value)
    {
        this = Create(value);
    }

    public Int4(Int2 vector, int z, int w)
    {
        this = Create(vector, z, w);
    }

    public Int4(Int3 vector, int w)
    {
        this = Create(vector, w);
    }

    public Int4(int x, int y, int z, int w)
    {
        this = Create(x, y, z, w);
    }

    public Int4(ReadOnlySpan<int> values)
    {
        this = Create(values);
    }

    public int this[int index]
    {
        readonly get => this.AsVector128().GetElement(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => this = this.AsVector128().WithElement(index, value).AsInt4();
    }

    public readonly void CopyTo(int[] array) => this.AsVector128().CopyTo(array);
    public readonly void CopyTo(int[] array, int index) => this.AsVector128().CopyTo(array, index);
    public readonly void CopyTo(Span<int> destination) => this.AsVector128().CopyTo(destination);
    public readonly bool TryCopyTo(Span<int> destination) => this.AsVector128().TryCopyTo(destination);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Equals(Int4 other)
    {
        return this.AsVector128().Equals(other.AsVector128());
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is Int4 other && Equals(other);
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }

    public readonly override string ToString() => ToString("G", CultureInfo.CurrentCulture);
    public readonly string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
    {
        string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
        return $"<{X.ToString(format, formatProvider)}{separator} {Y.ToString(format, formatProvider)}{separator} {Z.ToString(format, formatProvider)}{separator} {W.ToString(format, formatProvider)}>";
    }

    public static Int4 MinValue => Create(int.MinValue);
    public static Int4 MaxValue => Create(int.MaxValue);
    public static Int4 One => Create(1);
    public static Int4 UnitX => Create(1, 0, 0, 0);
    public static Int4 UnitY => Create(0, 1, 0, 0);
    public static Int4 UnitZ => Create(0, 0, 1, 0);
    public static Int4 Zero => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 operator +(Int4 left, Int4 right) => (left.AsVector128() + right.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 operator /(Int4 left, Int4 right) => (left.AsVector128() / right.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 operator /(Int4 left, int right) => (left.AsVector128() / right).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int4 left, Int4 right) => left.AsVector128() == right.AsVector128();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int4 left, Int4 right) => !(left == right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 operator *(Int4 left, Int4 right) => (left.AsVector128() * right.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 operator *(Int4 left, int right) => (left.AsVector128() * right).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 operator *(int left, Int4 right) => right * left;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 operator -(Int4 left, Int4 right) => (left.AsVector128() - right.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 operator -(Int4 value) => (-value.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 Abs(Int4 value) => Vector128.Abs(value.AsVector128()).AsInt4();

    public static Int4 Add(Int4 left, Int4 right) => left + right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 Clamp(Int4 value, Int4 min, Int4 max) => Vector128.Clamp(value.AsVector128(), min.AsVector128(), max.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 ClampNative(Int4 value, Int4 min, Int4 max) => Vector128.ClampNative(value.AsVector128(), min.AsVector128(), max.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 CopySign(Int4 value, Int4 sign) => Vector128.CopySign(value.AsVector128(), sign.AsVector128()).AsInt4();

    public static Int4 Create(int value) => Vector128.Create(value).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 Create(Int2 vector, int z, int w) => vector
        .AsVector128()
        .WithElement(2, z)
        .WithElement(3, w)
        .AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 Create(Int3 vector, int w) => vector
        .AsVector128()
        .WithElement(3, w)
        .AsInt4();

    public static Int4 Create(int x, int y, int z, int w) => Vector128.Create(x, y, z, w).AsInt4();

    public static Int4 Create(ReadOnlySpan<int> values) => Vector128.Create(values).AsInt4();

    public static Int4 Divide(Int4 left, Int4 right) => left / right;
    public static Int4 Divide(Int4 left, int divisor) => left / divisor;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 Max(Int4 x, Int4 y) => Vector128.Max(x.AsVector128(), y.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 MaxMagnitude(Int4 x, Int4 y) => Vector128.MaxMagnitude(x.AsVector128(), y.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 MaxNative(Int4 x, Int4 y) => Vector128.MaxNative(x.AsVector128(), y.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 Min(Int4 x, Int4 y) => Vector128.Min(x.AsVector128(), y.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 MinMagnitude(Int4 x, Int4 y) => Vector128.MinMagnitude(x.AsVector128(), y.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 MinNative(Int4 x, Int4 y) => Vector128.MinNative(x.AsVector128(), y.AsVector128()).AsInt4();

    public static Int4 Multiply(Int4 left, Int4 right) => left * right;
    public static Int4 Multiply(Int4 left, int right) => left * right;
    public static Int4 Multiply(int left, Int4 right) => left * right;
    public static Int4 Negate(Int4 value) => -value;
    public static Int4 Subtract(Int4 left, Int4 right) => left - right;
}