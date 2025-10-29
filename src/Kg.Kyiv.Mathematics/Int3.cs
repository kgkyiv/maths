using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Kg.Kyiv.Math;

// TODO: ISpanFormattable
[StructLayout(LayoutKind.Sequential, Size = 12)]
public struct Int3 : IEquatable<Int3>, IFormattable
{
    internal const int Count = 3;

    public int X;
    public int Y;
    public int Z;

    public Int3(int value)
    {
        this = Create(value);
    }

    public Int3(Int2 vector, int z)
    {
        this = Create(vector, z);
    }

    public Int3(int x, int y, int z)
    {
        this = Create(x, y, z);
    }

    public Int3(ReadOnlySpan<int> values)
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

            this = this.AsVector128Unsafe().WithElement(index, value).AsInt3();
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

    public static Int3 MinValue => Create(int.MinValue);
    public static Int3 MaxValue => Create(int.MaxValue);
    public static Int3 One => Create(1);
    public static Int3 UnitX => Create(1, 0, 0);
    public static Int3 UnitY => Create(0, 1, 0);
    public static Int3 UnitZ => Create(0, 0, 1);
    public static Int3 Zero => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 operator +(Int3 left, Int3 right) => (left.AsVector128Unsafe() + right.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 operator /(Int3 left, Int3 right) => (left.AsVector128Unsafe() / right.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 operator /(Int3 left, int right) => (left.AsVector128Unsafe() / right).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int3 left, Int3 right) => left.AsVector128Unsafe() == right.AsVector128Unsafe();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int3 left, Int3 right) => !(left == right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 operator *(Int3 left, Int3 right) => (left.AsVector128Unsafe() * right.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 operator *(Int3 left, int right) => (left.AsVector128Unsafe() * right).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 operator *(int left, Int3 right) => right * left;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 operator -(Int3 left, Int3 right) => (left.AsVector128Unsafe() - right.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 operator -(Int3 value) => (-value.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 Abs(Int3 value) => Vector128.Abs(value.AsVector128Unsafe()).AsInt3();

    public static Int3 Add(Int3 left, Int3 right) => left + right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 Clamp(Int3 value, Int3 min, Int3 max) =>
        Vector128.Clamp(value.AsVector128Unsafe(), min.AsVector128Unsafe(), max.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 ClampNative(Int3 value, Int3 min, Int3 max) =>
        Vector128.ClampNative(value.AsVector128Unsafe(), min.AsVector128Unsafe(), max.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 CopySign(Int3 value, Int3 sign) => Vector128.CopySign(value.AsVector128Unsafe(), sign.AsVector128Unsafe()).AsInt3();

    public static Int3 Create(int value) => Vector128.Create(value).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 Create(Int2 vector, int z) => vector
        .AsVector128Unsafe()
        .WithElement(2, z)
        .AsInt3();

    public static Int3 Create(int x, int y, int z) => Vector128.Create(x, y, z, 0).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 Create(ReadOnlySpan<int> values)
    {
        if (values.Length < Count)
        {
            throw new ArgumentOutOfRangeException(nameof(values));
        }

        return Unsafe.ReadUnaligned<Int3>(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(values)));
    }

    public static Int3 Divide(Int3 left, Int3 right) => left / right;
    public static Int3 Divide(Int3 left, int divisor) => left / divisor;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 Max(Int3 x, Int3 y) => Vector128.Max(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 MaxMagnitude(Int3 x, Int3 y) => Vector128.MaxMagnitude(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 MaxNative(Int3 x, Int3 y) => Vector128.MaxNative(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 Min(Int3 x, Int3 y) => Vector128.Min(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 MinMagnitude(Int3 x, Int3 y) => Vector128.MinMagnitude(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 MinNative(Int3 x, Int3 y) => Vector128.MinNative(x.AsVector128Unsafe(), y.AsVector128Unsafe()).AsInt3();

    public static Int3 Multiply(Int3 left, Int3 right) => left * right;
    public static Int3 Multiply(Int3 left, int right) => left * right;
    public static Int3 Multiply(int left, Int3 right) => left * right;
    public static Int3 Negate(Int3 value) => -value;
    public static Int3 Subtract(Int3 left, Int3 right) => left - right;

    public readonly bool Equals(Int3 other)
    {
        return this.AsVector128Unsafe().Equals(other.AsVector128Unsafe());
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is Int3 other && Equals(other);
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public readonly override string ToString() => ToString("G", CultureInfo.CurrentCulture);
    public readonly string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
    {
        string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
        return $"<{X.ToString(format, formatProvider)}{separator} {Y.ToString(format, formatProvider)}{separator} {Z.ToString(format, formatProvider)}>";
    }
}