using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Kg.Kyiv.Mathematics;

// TODO: ISpanFormattable
[StructLayout(LayoutKind.Sequential, Size = 8)]
public struct Int2 : IEquatable<Int2>, IFormattable
{
    internal const int Count = 2;

    public int X;
    public int Y;

    public Int2(int value)
    {
        this = Create(value);
    }

    public Int2(int x, int y)
    {
        this = Create(x, y);
    }

    public Int2(ReadOnlySpan<int> values)
    {
        this = Create(values);
    }

    public int this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get
        {
            if ((uint)index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return this.AsVector128Unsafe().GetElement(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if ((uint)index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            this = this.AsVector128Unsafe().WithElement(index, value).AsInt2();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(int[] array)
    {
        if ((uint)array.Length < Count)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<int, byte>(ref array[0]), this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(int[] array, int index)
    {
        if ((uint)index > (uint)array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (array.Length - index < Count)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<int, byte>(ref array[index]), this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(Span<int> destination)
    {
        if (destination.Length < Count)
        {
            throw new ArgumentOutOfRangeException(nameof(destination));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(destination)), this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryCopyTo(Span<int> destination)
    {
        if (destination.Length < Count)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(destination)), this);
        return true;
    }

    public static Int2 MinValue => Create(int.MinValue);
    public static Int2 MaxValue => Create(int.MaxValue);
    public static Int2 One => Create(1);
    public static Int2 UnitX => Create(1, 0);
    public static Int2 UnitY => Create(0, 1);
    public static Int2 Zero => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 operator +(Int2 left, Int2 right) => (left.AsVector128Unsafe() + right.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 operator /(Int2 left, Int2 right) => (left.AsVector128Unsafe() / right.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 operator /(Int2 left, int right) => (left.AsVector128Unsafe() / right).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int2 left, Int2 right) => left.AsVector128Unsafe() == right.AsVector128Unsafe();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int2 left, Int2 right) => !(left == right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 operator *(Int2 left, Int2 right) => (left.AsVector128Unsafe() * right.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 operator *(Int2 left, int right) => (left.AsVector128Unsafe() * right).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 operator *(int left, Int2 right) => right * left;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 operator -(Int2 left, Int2 right) => (left.AsVector128Unsafe() - right.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 operator -(Int2 value) => (-value.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 Abs(Int2 value) => Vector128.Abs(value.AsVector128Unsafe()).AsInt2();

    public static Int2 Add(Int2 left, Int2 right) => left + right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 Clamp(Int2 value, Int2 min, Int2 max) => Vector128.Clamp(value.AsVector128Unsafe(), min.AsVector128Unsafe(), max.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 ClampNative(Int2 value, Int2 min, Int2 max) => Vector128.ClampNative(value.AsVector128Unsafe(), min.AsVector128Unsafe(), max.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 CopySign(Int2 value, Int2 sign) => Vector128.CopySign(value.AsVector128Unsafe(), sign.AsVector128Unsafe()).AsInt2();

    public static Int2 Create(int value) => Vector128.Create(value).AsInt2();

    public static Int2 Create(int x, int y) => Vector128.Create(x, y, 0, 0).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 Create(ReadOnlySpan<int> values)
    {
        if (values.Length < Count)
        {
            throw new ArgumentOutOfRangeException(nameof(values));
        }

        return Unsafe.ReadUnaligned<Int2>(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(values)));
    }

    public static Int2 Divide(Int2 left, Int2 right) => left / right;
    public static Int2 Divide(Int2 left, int divisor) => left / divisor;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 Max(Int2 x, Int2 y) => Vector128.Max(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 MaxMagnitude(Int2 x, Int2 y) => Vector128.MaxMagnitude(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 MaxNative(Int2 x, Int2 y) => Vector128.MaxNative(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 Min(Int2 x, Int2 y) => Vector128.Min(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 MinMagnitude(Int2 x, Int2 y) => Vector128.MinMagnitude(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 MinNative(Int2 x, Int2 y) => Vector128.MinNative(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt2();

    public static Int2 Multiply(Int2 left, Int2 right) => left * right;
    public static Int2 Multiply(Int2 left, int right) => left * right;
    public static Int2 Multiply(int left, Int2 right) => left * right;
    public static Int2 Negate(Int2 value) => -value;
    public static Int2 Subtract(Int2 left, Int2 right) => left - right;

    public readonly bool Equals(Int2 other)
    {
        return this.AsVector128Unsafe().Equals(other.AsVector128Unsafe());
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is Int2 other && Equals(other);
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public readonly override string ToString() => ToString("G", CultureInfo.CurrentCulture);
    public readonly string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
    {
        string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
        return $"<{X.ToString(format, formatProvider)}{separator} {Y.ToString(format, formatProvider)}>";
    }
}