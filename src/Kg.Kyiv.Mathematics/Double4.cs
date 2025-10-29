using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Kg.Kyiv.Mathematics;

[StructLayout(LayoutKind.Sequential, Size = 32)]
public struct Double4 : IEquatable<Double4>, IFormattable
{
    internal const int Count = 4;

    public double X;
    public double Y;
    public double Z;
    public double W;

    public Double4(double value)
    {
        this = Create(value);
    }

    public Double4(Double2 vector, double z, double w)
    {
        this = Create(vector, z, w);
    }

    public Double4(Double3 vector, double w)
    {
        this = Create(vector, w);
    }

    public Double4(double x, double y, double z, double w)
    {
        this = Create(x, y, z, w);
    }

    public double this[int index]
    {
        readonly get => this.AsVector256().GetElement(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => this = this.AsVector256().WithElement(index, value).AsDouble4();
    }
    
    public readonly void CopyTo(double[] array) => this.AsVector256().CopyTo(array);
    public readonly void CopyTo(double[] array, int index) => this.AsVector256().CopyTo(array, index);
    public readonly void CopyTo(Span<double> destination) => this.AsVector256().CopyTo(destination);
    public readonly bool TryCopyTo(Span<double> destination) => this.AsVector256().TryCopyTo(destination);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Equals(Double4 other)
    {
        return this.AsVector256().Equals(other.AsVector256());
    }

    public readonly override bool Equals(object? obj)
    {
        return obj is Double4 other && Equals(other);
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }

    public readonly double Length() => double.Sqrt(LengthSquared());
    public readonly double LengthSquared() => Dot(this, this);
    
    public readonly override string ToString() => ToString("G", CultureInfo.CurrentCulture);
    public readonly string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
    {
        string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
        return $"<{X.ToString(format, formatProvider)}{separator} {Y.ToString(format, formatProvider)}{separator} {Z.ToString(format, formatProvider)}{separator} {W.ToString(format, formatProvider)}>";
    }

    public static Double4 E => Create(double.E);
    public static Double4 Epsilon => Create(double.Epsilon);
    public static Double4 NaN => Create(double.NaN);
    public static Double4 NegativeInfinity => Create(double.NegativeInfinity);
    public static Double4 NegativeZero => Create(double.NegativeZero);
    public static Double4 One => Create(1.0);
    public static Double4 Pi => Create(double.Pi);
    public static Double4 PositiveInfinity => Create(double.PositiveInfinity);
    public static Double4 Tau => Create(double.Tau);
    public static Double4 UnitX => Create(1.0, 0.0, 0.0, 0.0);
    public static Double4 UnitY => Create(0.0, 1.0, 0.0, 0.0);
    public static Double4 UnitZ => Create(0.0, 0.0, 1.0, 0.0);
    public static Double4 UnitW => Create(0.0, 0.0, 0.0, 1.0);
    public static Double4 Zero => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 operator +(Double4 left, Double4 right) => (left.AsVector256() + right.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 operator /(Double4 left, Double4 right) => (left.AsVector256() / right.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 operator /(Double4 left, double right) => (left.AsVector256() / right).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Double4 left, Double4 right) => left.AsVector256() == right.AsVector256();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Double4 left, Double4 right) => !(left == right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 operator *(Double4 left, Double4 right) => (left.AsVector256() * right.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 operator *(Double4 left, double right) => (left.AsVector256() * right).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 operator *(double left, Double4 right) => right * left;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 operator -(Double4 left, Double4 right) => (left.AsVector256() - right.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 operator -(Double4 value) => (-value.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Abs(Double4 value) => Vector256.Abs(value.AsVector256()).AsDouble4();
    
    public static Double4 Add(Double4 left, Double4 right) => left + right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Clamp(Double4 value, Double4 min, Double4 max) => Vector256.Clamp(value.AsVector256(), min.AsVector256(), max.AsVector256()).AsDouble4();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 ClampNative(Double4 value, Double4 min, Double4 max) => Vector256.ClampNative(value.AsVector256(), min.AsVector256(), max.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 CopySign(Double4 value, Double4 sign) => Vector256.CopySign(value.AsVector256(), sign.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Cos(Double4 value) => Vector256.Cos(value.AsVector256()).AsDouble4();

    public static Double4 Create(double value) => Vector256.Create(value).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Create(Double2 vector, double z, double w) => vector
        .AsVector256Unsafe()
        .WithElement(2, z)
        .WithElement(3, w)
        .AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Create(Double3 vector, double w) => vector
        .AsVector256Unsafe()
        .WithElement(3, w)
        .AsDouble4();

    public static Double4 Create(double x, double y, double z, double w) => Vector256.Create(x, y, z, w).AsDouble4();
    public static Double4 Create(ReadOnlySpan<double> values) => Vector256.Create(values).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 DegreesToRadians(Double4 degrees) => Vector256.DegreesToRadians(degrees.AsVector256()).AsDouble4();

    public static double Distance(Double4 x, Double4 y) => double.Sqrt(DistanceSquared(x, y));

    public static double DistanceSquared(Double4 x, Double4 y) => (x - y).LengthSquared();

    public static Double4 Divide(Double4 left, Double4 right) => left / right;
    public static Double4 Divide(Double4 left, double divisor) => left / divisor;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Dot(Double4 x, Double4 y) => Vector256.Dot(x.AsVector256(), y.AsVector256());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Exp(Double4 value) => Vector256.Exp(value.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 FusedMultiplyAdd(Double4 left, Double4 right, Double4 addend) => Vector256.FusedMultiplyAdd(left.AsVector256(), right.AsVector256(), addend.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Hypot(Double4 x, Double4 y) => Vector256.Hypot(x.AsVector256(), y.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Lerp(Double4 x, Double4 y, double amount) => Lerp(x, y, Create(amount));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Lerp(Double4 x, Double4 y, Double4 amount) => Vector256.Lerp(x.AsVector256(), y.AsVector256(), amount.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Log(Double4 vector) => Vector256.Log(vector.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Log2(Double4 vector) => Vector256.Log2(vector.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Max(Double4 x, Double4 y) => Vector256.Max(x.AsVector256(), y.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 MaxMagnitude(Double4 x, Double4 y) => Vector256.MaxMagnitude(x.AsVector256(), y.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 MaxMagnitudeNumber(Double4 x, Double4 y) => Vector256.MaxMagnitudeNumber(x.AsVector256(), y.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 MaxNative(Double4 x, Double4 y) => Vector256.MaxNative(x.AsVector256(), y.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 MaxNumber(Double4 x, Double4 y) => Vector256.MaxNumber(x.AsVector256(), y.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Min(Double4 x, Double4 y) => Vector256.Min(x.AsVector256(), y.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 MinMagnitude(Double4 x, Double4 y) => Vector256.MinMagnitude(x.AsVector256(), y.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 MinMagnitudeNumber(Double4 x, Double4 y) => Vector256.MinMagnitudeNumber(x.AsVector256(), y.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 MinNative(Double4 x, Double4 y) => Vector256.MinNative(x.AsVector256(), y.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 MinNumber(Double4 x, Double4 y) => Vector256.MinNumber(x.AsVector256(), y.AsVector256()).AsDouble4();
    
    public static Double4 Multiply(Double4 left, Double4 right) => left * right;
    public static Double4 Multiply(Double4 left, double right) => left * right;
    public static Double4 Multiply(double left, Double4 right) => left * right;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 MultiplyAddEstimate(Double4 left, Double4 right, Double4 addend) => Vector256.MultiplyAddEstimate(left.AsVector256(), right.AsVector256(), addend.AsVector256()).AsDouble4();

    public static Double4 Negate(Double4 value) => -value;

    public static Double4 Normalize(Double4 vector) => vector / vector.Length();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 RadiansToDegrees(Double4 radians) => Vector256.RadiansToDegrees(radians.AsVector256()).AsDouble4();

    public static Double4 Round(Double4 vector) => Vector256.Round(vector.AsVector256()).AsDouble4();

    public static Double4 Round(Double4 vector, MidpointRounding mode) => Vector256.Round(vector.AsVector256(), mode).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 Sin(Double4 vector) => Vector256.Sin(vector.AsVector256()).AsDouble4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (Double4 Sin, Double4 Cos) SinCos(Double4 vector)
    {
        (Vector256<double> sin, Vector256<double> cos) = Vector256.SinCos(vector.AsVector256());
        return (sin.AsDouble4(), cos.AsDouble4());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 SquareRoot(Double4 value) => Vector256.Sqrt(value.AsVector256()).AsDouble4();
    
    public static Double4 Subtract(Double4 left, Double4 right) => left - right;

    public static Double4 Truncate(Double4 vector) => Vector256.Truncate(vector.AsVector256()).AsDouble4();
}