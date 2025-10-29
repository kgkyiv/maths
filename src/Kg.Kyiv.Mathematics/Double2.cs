using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Kg.Kyiv.Mathematics;

[StructLayout(LayoutKind.Sequential, Size = 16)]
public struct Double2
{
        internal const int Count = 2;

    public double X;
    public double Y;

    public Double2(double value)
    {
        this = Create(value);
    }

    public Double2(double x, double y)
    {
        this = Create(x, y);
    }

    public double this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get
        {
            if ((uint)index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return this.AsVector256Unsafe().GetElement(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if ((uint)index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            this = this.AsVector256Unsafe().WithElement(index, value).AsDouble2();
        }
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(double[] array)
    {
        if (array.Length < Count)
        {
            throw new ArgumentException("Destination is too short", nameof(array));
        }
        
        Unsafe.WriteUnaligned(ref Unsafe.As<double, byte>(ref array[0]), this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(double[] array, int index)
    {
        if ((uint)index > (uint)array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (array.Length - index < Count)
        {
            throw new ArgumentException("Destination is too short", nameof(array));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<double, byte>(ref array[index]), this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(Span<double> destination)
    {
        if (destination.Length < Count)
        {
            throw new ArgumentException("Destination is too short", nameof(destination));
        }
        
        Unsafe.WriteUnaligned(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(destination)), this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryCopyTo(Span<double> destination)
    {
        if (destination.Length < Count)
        {
            return false;
        }
        
        Unsafe.WriteUnaligned(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(destination)), this);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Equals(Double2 other)
    {
        return this.AsVector256Unsafe().Equals(other.AsVector256Unsafe());
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is Double2 other && Equals(other);
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public readonly double Length() => double.Sqrt(LengthSquared());
    public readonly double LengthSquared() => Dot(this, this);

    public readonly override string ToString() => ToString("G", CultureInfo.CurrentCulture);
    public readonly string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
    {
        string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
        return
            $"<{X.ToString(format, formatProvider)}{separator} {Y.ToString(format, formatProvider)}>";
    }

    public static Double2 E => Create(double.E);
    public static Double2 Epsilon => Create(double.Epsilon);
    public static Double2 NaN => Create(double.NaN);
    public static Double2 NegativeInfinity => Create(double.NegativeInfinity);
    public static Double2 NegativeZero => Create(double.NegativeZero);
    public static Double2 One => Create(1.0);
    public static Double2 Pi => Create(double.Pi);
    public static Double2 PositiveInfinity => Create(double.PositiveInfinity);
    public static Double2 Tau => Create(double.Tau);
    public static Double2 UnitX => Create(1.0, 0.0);
    public static Double2 UnitY => Create(0.0, 1.0);
    public static Double2 Zero => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator +(Double2 left, Double2 right) => (left.AsVector256Unsafe() + right.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator /(Double2 left, Double2 right) => (left.AsVector256Unsafe() / right.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator /(Double2 left, double right) => (left.AsVector256Unsafe() / right).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Double2 left, Double2 right) => left.AsVector256Unsafe() == right.AsVector256Unsafe();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Double2 left, Double2 right) => !(left == right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator *(Double2 left, Double2 right) => (left.AsVector256Unsafe() * right.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator *(Double2 left, double right) => (left.AsVector256Unsafe() * right).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator *(double left, Double2 right) => right * left;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator -(Double2 left, Double2 right) => (left.AsVector256Unsafe() - right.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 operator -(Double2 value) => (-value.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Abs(Double2 value) => Vector256.Abs(value.AsVector256Unsafe()).AsDouble2();

    public static Double2 Add(Double2 left, Double2 right) => left + right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Clamp(Double2 value, Double2 min, Double2 max) =>
        Vector256.Clamp(value.AsVector256Unsafe(), min.AsVector256Unsafe(), max.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 ClampNative(Double2 value, Double2 min, Double2 max) =>
        Vector256.ClampNative(value.AsVector256Unsafe(), min.AsVector256Unsafe(), max.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 CopySign(Double2 value, Double2 sign) => Vector256.CopySign(value.AsVector256Unsafe(), sign.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Cos(Double2 value) => Vector256.Cos(value.AsVector256Unsafe()).AsDouble2();

    public static Double2 Create(double value) => Vector256.Create(value).AsDouble2();
    public static Double2 Create(double x, double y) => Vector256.Create(x, y, 0.0, 0.0).AsDouble2();
    public static Double2 Create(ReadOnlySpan<double> values) => Vector256.Create(values).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 DegreesToRadians(Double2 degrees) => Vector256.DegreesToRadians(degrees.AsVector256Unsafe()).AsDouble2();

    public static double Distance(Double2 x, Double2 y) => double.Sqrt(DistanceSquared(x, y));

    public static double DistanceSquared(Double2 x, Double2 y) => (x - y).LengthSquared();

    public static Double2 Divide(Double2 left, Double2 right) => left / right;
    public static Double2 Divide(Double2 left, double divisor) => left / divisor;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Dot(Double2 x, Double2 y) => Vector256.Dot(x.AsVector256Unsafe(), y.AsVector256Unsafe());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Exp(Double2 value) => Vector256.Exp(value.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 FusedMultiplyAdd(Double2 left, Double2 right, Double2 addend) =>
        Vector256.FusedMultiplyAdd(left.AsVector256Unsafe(), right.AsVector256Unsafe(), addend.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Hypot(Double2 x, Double2 y) => Vector256.Hypot(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Lerp(Double2 x, Double2 y, double amount) => Lerp(x, y, Create(amount));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Lerp(Double2 x, Double2 y, Double2 amount) =>
        Vector256.Lerp(x.AsVector256Unsafe(), y.AsVector256Unsafe(), amount.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Log(Double2 vector) => Vector256.Log(vector.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Log2(Double2 vector) => Vector256.Log2(vector.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Max(Double2 x, Double2 y) => Vector256.Max(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 MaxMagnitude(Double2 x, Double2 y) => Vector256.MaxMagnitude(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 MaxMagnitudeNumber(Double2 x, Double2 y) => Vector256.MaxMagnitudeNumber(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 MaxNative(Double2 x, Double2 y) => Vector256.MaxNative(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 MaxNumber(Double2 x, Double2 y) => Vector256.MaxNumber(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Min(Double2 x, Double2 y) => Vector256.Min(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 MinMagnitude(Double2 x, Double2 y) => Vector256.MinMagnitude(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 MinMagnitudeNumber(Double2 x, Double2 y) => Vector256.MinMagnitudeNumber(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 MinNative(Double2 x, Double2 y) => Vector256.MinNative(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 MinNumber(Double2 x, Double2 y) => Vector256.MinNumber(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble2();

    public static Double2 Multiply(Double2 left, Double2 right) => left * right;
    public static Double2 Multiply(Double2 left, double right) => left * right;
    public static Double2 Multiply(double left, Double2 right) => left * right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 MultiplyAddEstimate(Double2 left, Double2 right, Double2 addend) =>
        Vector256.MultiplyAddEstimate(left.AsVector256Unsafe(), right.AsVector256Unsafe(), addend.AsVector256Unsafe()).AsDouble2();

    public static Double2 Negate(Double2 value) => -value;

    public static Double2 Normalize(Double2 vector) => vector / vector.Length();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 RadiansToDegrees(Double2 radians) => Vector256.RadiansToDegrees(radians.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Reflect(Double2 vector, Double2 normal)
    {
        Double2 tmp = Create(Dot(vector, normal));
        tmp += tmp;
        return MultiplyAddEstimate(-tmp, normal, vector);
    }

    public static Double2 Round(Double2 vector) => Vector256.Round(vector.AsVector256Unsafe()).AsDouble2();

    public static Double2 Round(Double2 vector, MidpointRounding mode) => Vector256.Round(vector.AsVector256Unsafe(), mode).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 Sin(Double2 vector) => Vector256.Sin(vector.AsVector256Unsafe()).AsDouble2();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (Double2 Sin, Double2 Cos) SinCos(Double2 vector)
    {
        (Vector256<double> sin, Vector256<double> cos) = Vector256.SinCos(vector.AsVector256Unsafe());
        return (sin.AsDouble2(), cos.AsDouble2());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double2 SquareRoot(Double2 value) => Vector256.Sqrt(value.AsVector256Unsafe()).AsDouble2();

    public static Double2 Subtract(Double2 left, Double2 right) => left - right;

    public static Double2 Truncate(Double2 vector) => Vector256.Truncate(vector.AsVector256Unsafe()).AsDouble2();
}