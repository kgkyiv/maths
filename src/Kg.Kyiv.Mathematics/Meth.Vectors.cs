using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Kg.Kyiv.Mathematics.Extensions;
using Kg.Kyiv.Mathematics.Numerics;

namespace Kg.Kyiv.Mathematics;

public static partial class Meth
{
    // Meth.FloorToInt >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<int> FloorToInt(Vector64<float> vector)
    {
        Unsafe.SkipInit(out Vector64<int> result);
        return result
            .WithElement(0, FloorToInt(vector.GetElement(0)))
            .WithElement(1, FloorToInt(vector.GetElement(1)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> FloorToInt(Vector128<float> vector)
        => Vector128.Create(FloorToInt(vector.GetLower()), FloorToInt(vector.GetUpper()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<int> FloorToInt(Vector256<float> vector)
        => Vector256.Create(FloorToInt(vector.GetLower()), FloorToInt(vector.GetUpper()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector512<int> FloorToInt(Vector512<float> vector)
        => Vector512.Create(FloorToInt(vector.GetLower()), FloorToInt(vector.GetUpper()));

    public static Int2 FloorToInt(Vector2 vector) => FloorToInt(vector.AsVector128Unsafe()).AsInt2();
    public static Int3 FloorToInt(Vector3 vector) => FloorToInt(vector.AsVector128Unsafe()).AsInt3();
    public static Int4 FloorToInt(Vector4 vector) => FloorToInt(vector.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<int> FloorToInt(Vector128<double> vector)
    {
        Unsafe.SkipInit(out Vector64<int> result);
        return result
            .WithElement(0, FloorToInt(vector.GetElement(0)))
            .WithElement(1, FloorToInt(vector.GetElement(1)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> FloorToInt(Vector256<double> vector)
        => Vector128.Create(FloorToInt(vector.GetLower()), FloorToInt(vector.GetUpper()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<int> FloorToInt(Vector512<double> vector)
        => Vector256.Create(FloorToInt(vector.GetLower()), FloorToInt(vector.GetUpper()));

    public static Int2 FloorToInt(Double2 vector) => FloorToInt(vector.AsVector256Unsafe()).AsInt2();
    public static Int3 FloorToInt(Double3 vector) => FloorToInt(vector.AsVector256Unsafe()).AsInt3();
    public static Int4 FloorToInt(Double4 vector) => FloorToInt(vector.AsVector256()).AsInt4();
    // <<

    // Meth.CeilingToInt >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<int> CeilingToInt(Vector64<float> vector)
    {
        Unsafe.SkipInit(out Vector64<int> result);
        return result
            .WithElement(0, CeilingToInt(vector.GetElement(0)))
            .WithElement(1, CeilingToInt(vector.GetElement(1)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> CeilingToInt(Vector128<float> vector)
        => Vector128.Create(CeilingToInt(vector.GetLower()), CeilingToInt(vector.GetUpper()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<int> CeilingToInt(Vector256<float> vector)
        => Vector256.Create(CeilingToInt(vector.GetLower()), CeilingToInt(vector.GetUpper()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector512<int> CeilingToInt(Vector512<float> vector)
        => Vector512.Create(CeilingToInt(vector.GetLower()), CeilingToInt(vector.GetUpper()));

    public static Int2 CeilingToInt(Vector2 vector) => CeilingToInt(vector.AsVector128Unsafe()).AsInt2();
    public static Int3 CeilingToInt(Vector3 vector) => CeilingToInt(vector.AsVector128Unsafe()).AsInt3();
    public static Int4 CeilingToInt(Vector4 vector) => CeilingToInt(vector.AsVector128()).AsInt4();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<int> CeilingToInt(Vector128<double> vector)
    {
        Unsafe.SkipInit(out Vector64<int> result);
        return result
            .WithElement(0, CeilingToInt(vector.GetElement(0)))
            .WithElement(1, CeilingToInt(vector.GetElement(1)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> CeilingToInt(Vector256<double> vector)
        => Vector128.Create(CeilingToInt(vector.GetLower()), CeilingToInt(vector.GetUpper()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<int> CeilingToInt(Vector512<double> vector)
        => Vector256.Create(CeilingToInt(vector.GetLower()), CeilingToInt(vector.GetUpper()));

    public static Int2 CeilingToInt(Double2 vector) => CeilingToInt(vector.AsVector256Unsafe()).AsInt2();
    public static Int3 CeilingToInt(Double3 vector) => CeilingToInt(vector.AsVector256Unsafe()).AsInt3();
    public static Int4 CeilingToInt(Double4 vector) => CeilingToInt(vector.AsVector256()).AsInt4();
    //
    
    // Meth.IsZero >>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(Vector64<float> vector) => Vector64.LessThanAll(Vector64.Abs(vector), Vector64.Create(float.Epsilon));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(Vector128<float> vector) => Vector128.LessThanAll(Vector128.Abs(vector), Vector128.Create(float.Epsilon));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(Vector256<float> vector) => Vector256.LessThanAll(Vector256.Abs(vector), Vector256.Create(float.Epsilon));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(Vector512<float> vector) => Vector512.LessThanAll(Vector512.Abs(vector), Vector512.Create(float.Epsilon));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(Vector128<double> vector) => Vector128.LessThanAll(Vector128.Abs(vector), Vector128.Create(double.Epsilon));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(Vector256<double> vector) => Vector256.LessThanAll(Vector256.Abs(vector), Vector256.Create(double.Epsilon));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(Vector512<double> vector) => Vector512.LessThanAll(Vector512.Abs(vector), Vector512.Create(double.Epsilon));

    public static bool IsZero(Vector2 vector) => IsZero(vector.AsVector128Unsafe());
    public static bool IsZero(Vector3 vector) => IsZero(vector.AsVector128Unsafe());
    public static bool IsZero(Vector4 vector) => IsZero(vector.AsVector128());
    public static bool IsZero(Double2 vector) => IsZero(vector.AsVector256Unsafe());
    public static bool IsZero(Double3 vector) => IsZero(vector.AsVector256Unsafe());
    public static bool IsZero(Double4 vector) => IsZero(vector.AsVector256());
    // <<
    
    // Meth.SafeDiv >> 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> SafeDiv(Vector64<float> left, Vector64<float> right) => IsZero(right) ? Vector64.Create(float.PositiveInfinity) : left / right;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> SafeDiv(Vector64<float> left, float right) => IsZero(right) ? Vector64.Create(float.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> SafeDiv(Vector128<float> left, Vector128<float> right) => IsZero(right) ? Vector128.Create(float.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> SafeDiv(Vector128<float> left, float right) => IsZero(right) ? Vector128.Create(float.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<float> SafeDiv(Vector256<float> left, Vector256<float> right) => IsZero(right) ? Vector256.Create(float.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<float> SafeDiv(Vector256<float> left, float right) => IsZero(right) ? Vector256.Create(float.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector512<float> SafeDiv(Vector512<float> left, Vector512<float> right) => IsZero(right) ? Vector512.Create(float.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector512<float> SafeDiv(Vector512<float> left, float right) => IsZero(right) ? Vector512.Create(float.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<double> SafeDiv(Vector128<double> left, Vector128<double> right) => IsZero(right) ? Vector128.Create(double.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<double> SafeDiv(Vector128<double> left, double right) => IsZero(right) ? Vector128.Create(double.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> SafeDiv(Vector256<double> left, Vector256<double> right) => IsZero(right) ? Vector256.Create(double.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> SafeDiv(Vector256<double> left, double right) => IsZero(right) ? Vector256.Create(double.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector512<double> SafeDiv(Vector512<double> left, Vector512<double> right) => IsZero(right) ? Vector512.Create(double.PositiveInfinity) : left / right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector512<double> SafeDiv(Vector512<double> left, double right) => IsZero(right) ? Vector512.Create(double.PositiveInfinity) : left / right;

    public static Vector2 SafeDiv(Vector2 left, Vector2 right) => SafeDiv(left.AsVector128Unsafe(), right.AsVector128Unsafe()).AsVector2();
    public static Vector2 SafeDiv(Vector2 left, float right) => SafeDiv(left.AsVector128Unsafe(), right).AsVector2();
    public static Vector3 SafeDiv(Vector3 left, Vector3 right) => SafeDiv(left.AsVector128Unsafe(), right.AsVector128Unsafe()).AsVector3();
    public static Vector3 SafeDiv(Vector3 left, float right) => SafeDiv(left.AsVector128Unsafe(), right).AsVector3();
    public static Vector4 SafeDiv(Vector4 left, Vector4 right) => SafeDiv(left.AsVector128(), right.AsVector128()).AsVector4();
    public static Vector4 SafeDiv(Vector4 left, float right) => SafeDiv(left.AsVector128(), right).AsVector4();
    public static Double2 SafeDiv(Double2 left, Double2 right) => SafeDiv(left.AsVector256Unsafe(), right.AsVector256Unsafe()).AsDouble2();
    public static Double2 SafeDiv(Double2 left, double right) => SafeDiv(left.AsVector256Unsafe(), right).AsDouble2();
    public static Double3 SafeDiv(Double3 left, Double3 right) => SafeDiv(left.AsVector256Unsafe(), right.AsVector256Unsafe()).AsDouble3();
    public static Double3 SafeDiv(Double3 left, double right) => SafeDiv(left.AsVector256Unsafe(), right).AsDouble3();
    public static Double4 SafeDiv(Double4 left, Double4 right) => SafeDiv(left.AsVector256(), right.AsVector256()).AsDouble4();
    public static Double4 SafeDiv(Double4 left, double right) => SafeDiv(left.AsVector256(), right).AsDouble4();
    // <<
}