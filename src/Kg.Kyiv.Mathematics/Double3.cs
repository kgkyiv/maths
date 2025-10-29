using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Kg.Kyiv.Mathematics;

[StructLayout(LayoutKind.Sequential, Size = 24)]
public struct Double3
{
    internal const int Count = 3;

    public double X;
    public double Y;
    public double Z;

    public Double3(double value)
    {
        this = Create(value);
    }

    public Double3(Double2 vector, double z)
    {
        this = Create(vector, z);
    }

    public Double3(double x, double y, double z)
    {
        this = Create(x, y, z);
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

            this = this.AsVector256Unsafe().WithElement(index, value).AsDouble3();
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
    public readonly bool Equals(Double3 other)
    {
        return this.AsVector256Unsafe().Equals(other.AsVector256Unsafe());
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is Double3 other && Equals(other);
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public readonly double Length() => double.Sqrt(LengthSquared());
    public readonly double LengthSquared() => Dot(this, this);

    public readonly override string ToString() => ToString("G", CultureInfo.CurrentCulture);
    public readonly string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
    {
        string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
        return
            $"<{X.ToString(format, formatProvider)}{separator} {Y.ToString(format, formatProvider)}{separator} {Z.ToString(format, formatProvider)}>";
    }

    public static Double3 E => Create(double.E);
    public static Double3 Epsilon => Create(double.Epsilon);
    public static Double3 NaN => Create(double.NaN);
    public static Double3 NegativeInfinity => Create(double.NegativeInfinity);
    public static Double3 NegativeZero => Create(double.NegativeZero);
    public static Double3 One => Create(1.0);
    public static Double3 Pi => Create(double.Pi);
    public static Double3 PositiveInfinity => Create(double.PositiveInfinity);
    public static Double3 Tau => Create(double.Tau);
    public static Double3 UnitX => Create(1.0, 0.0, 0.0);
    public static Double3 UnitY => Create(0.0, 1.0, 0.0);
    public static Double3 UnitZ => Create(0.0, 0.0, 1.0);
    public static Double3 Zero => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 operator +(Double3 left, Double3 right) => (left.AsVector256Unsafe() + right.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 operator /(Double3 left, Double3 right) => (left.AsVector256Unsafe() / right.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 operator /(Double3 left, double right) => (left.AsVector256Unsafe() / right).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Double3 left, Double3 right) => left.AsVector256Unsafe() == right.AsVector256Unsafe();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Double3 left, Double3 right) => !(left == right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 operator *(Double3 left, Double3 right) => (left.AsVector256Unsafe() * right.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 operator *(Double3 left, double right) => (left.AsVector256Unsafe() * right).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 operator *(double left, Double3 right) => right * left;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 operator -(Double3 left, Double3 right) => (left.AsVector256Unsafe() - right.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 operator -(Double3 value) => (-value.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Abs(Double3 value) => Vector256.Abs(value.AsVector256Unsafe()).AsDouble3();

    public static Double3 Add(Double3 left, Double3 right) => left + right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Clamp(Double3 value, Double3 min, Double3 max) =>
        Vector256.Clamp(value.AsVector256Unsafe(), min.AsVector256Unsafe(), max.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 ClampNative(Double3 value, Double3 min, Double3 max) =>
        Vector256.ClampNative(value.AsVector256Unsafe(), min.AsVector256Unsafe(), max.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 CopySign(Double3 value, Double3 sign) => Vector256.CopySign(value.AsVector256Unsafe(), sign.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Cos(Double3 value) => Vector256.Cos(value.AsVector256Unsafe()).AsDouble3();

    public static Double3 Create(double value) => Vector256.Create(value).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Create(Double2 vector, double z) => vector
        .AsVector256Unsafe()
        .WithElement(2, z)
        .AsDouble3();

    public static Double3 Create(double x, double y, double z) => Vector256.Create(x, y, z, 0.0).AsDouble3();
    public static Double3 Create(ReadOnlySpan<double> values) => Vector256.Create(values).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 DegreesToRadians(Double3 degrees) => Vector256.DegreesToRadians(degrees.AsVector256Unsafe()).AsDouble3();

    public static double Distance(Double3 x, Double3 y) => double.Sqrt(DistanceSquared(x, y));

    public static double DistanceSquared(Double3 x, Double3 y) => (x - y).LengthSquared();

    public static Double3 Divide(Double3 left, Double3 right) => left / right;
    public static Double3 Divide(Double3 left, double divisor) => left / divisor;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Dot(Double3 x, Double3 y) => Vector256.Dot(x.AsVector256Unsafe(), y.AsVector256Unsafe());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Exp(Double3 value) => Vector256.Exp(value.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 FusedMultiplyAdd(Double3 left, Double3 right, Double3 addend) =>
        Vector256.FusedMultiplyAdd(left.AsVector256Unsafe(), right.AsVector256Unsafe(), addend.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Hypot(Double3 x, Double3 y) => Vector256.Hypot(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Lerp(Double3 x, Double3 y, double amount) => Lerp(x, y, Create(amount));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Lerp(Double3 x, Double3 y, Double3 amount) =>
        Vector256.Lerp(x.AsVector256Unsafe(), y.AsVector256Unsafe(), amount.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Log(Double3 vector) => Vector256.Log(vector.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Log2(Double3 vector) => Vector256.Log2(vector.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Max(Double3 x, Double3 y) => Vector256.Max(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 MaxMagnitude(Double3 x, Double3 y) => Vector256.MaxMagnitude(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 MaxMagnitudeNumber(Double3 x, Double3 y) => Vector256.MaxMagnitudeNumber(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 MaxNative(Double3 x, Double3 y) => Vector256.MaxNative(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 MaxNumber(Double3 x, Double3 y) => Vector256.MaxNumber(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Min(Double3 x, Double3 y) => Vector256.Min(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 MinMagnitude(Double3 x, Double3 y) => Vector256.MinMagnitude(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 MinMagnitudeNumber(Double3 x, Double3 y) => Vector256.MinMagnitudeNumber(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 MinNative(Double3 x, Double3 y) => Vector256.MinNative(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 MinNumber(Double3 x, Double3 y) => Vector256.MinNumber(x.AsVector256Unsafe(), y.AsVector256Unsafe()).AsDouble3();

    public static Double3 Multiply(Double3 left, Double3 right) => left * right;
    public static Double3 Multiply(Double3 left, double right) => left * right;
    public static Double3 Multiply(double left, Double3 right) => left * right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 MultiplyAddEstimate(Double3 left, Double3 right, Double3 addend) =>
        Vector256.MultiplyAddEstimate(left.AsVector256Unsafe(), right.AsVector256Unsafe(), addend.AsVector256Unsafe()).AsDouble3();

    public static Double3 Negate(Double3 value) => -value;

    public static Double3 Normalize(Double3 vector) => vector / vector.Length();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 RadiansToDegrees(Double3 radians) => Vector256.RadiansToDegrees(radians.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Reflect(Double3 vector, Double3 normal)
    {
        Double3 tmp = Create(Dot(vector, normal));
        tmp += tmp;
        return MultiplyAddEstimate(-tmp, normal, vector);
    }

    public static Double3 Round(Double3 vector) => Vector256.Round(vector.AsVector256Unsafe()).AsDouble3();

    public static Double3 Round(Double3 vector, MidpointRounding mode) => Vector256.Round(vector.AsVector256Unsafe(), mode).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 Sin(Double3 vector) => Vector256.Sin(vector.AsVector256Unsafe()).AsDouble3();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (Double3 Sin, Double3 Cos) SinCos(Double3 vector)
    {
        (Vector256<double> sin, Vector256<double> cos) = Vector256.SinCos(vector.AsVector256Unsafe());
        return (sin.AsDouble3(), cos.AsDouble3());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double3 SquareRoot(Double3 value) => Vector256.Sqrt(value.AsVector256Unsafe()).AsDouble3();

    public static Double3 Subtract(Double3 left, Double3 right) => left - right;

    public static Double3 Truncate(Double3 vector) => Vector256.Truncate(vector.AsVector256Unsafe()).AsDouble3();
}