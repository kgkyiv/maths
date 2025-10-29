using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Kg.Kyiv.Mathematics;

public static unsafe class VectorExtensions
{
    public static Vector128<int> AsVector128(this Int2 value) => Int4.Create(value, 0, 0).AsVector128();
    public static Vector128<int> AsVector128(this Int3 value) => Int4.Create(value, 0).AsVector128();
    public static Vector128<int> AsVector128(this Int4 value) => Unsafe.BitCast<Int4, Vector128<int>>(value);

    [SkipLocalsInit]
    public static Vector128<int> AsVector128Unsafe(this Int2 value)
    {
        Unsafe.SkipInit(out Vector128<int> result);
        Unsafe.WriteUnaligned(ref Unsafe.As<Vector128<int>, byte>(ref result), value);
        return result;
    }

    [SkipLocalsInit]
    public static Vector128<int> AsVector128Unsafe(this Int3 value)
    {
        Unsafe.SkipInit(out Vector128<int> result);
        Unsafe.WriteUnaligned(ref Unsafe.As<Vector128<int>, byte>(ref result), value);
        return result;
    }

    [SkipLocalsInit]
    public static Vector256<double> AsVector256Unsafe(this Double2 value)
    {
        Unsafe.SkipInit(out Vector256<double> result);
        Unsafe.WriteUnaligned(ref Unsafe.As<Vector256<double>, byte>(ref result), value);
        return result;
    }

    [SkipLocalsInit]
    public static Vector256<double> AsVector256Unsafe(this Double3 value)
    {
        Unsafe.SkipInit(out Vector256<double> result);
        Unsafe.WriteUnaligned(ref Unsafe.As<Vector256<double>, byte>(ref result), value);
        return result;
    }

    public static Vector256<double> AsVector256(this Double4 value) => Unsafe.BitCast<Double4, Vector256<double>>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int2 AsInt2(this Vector128<int> value)
    {
        ref byte address = ref Unsafe.As<Vector128<int>, byte>(ref value);
        return Unsafe.ReadUnaligned<Int2>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int3 AsInt3(this Vector128<int> value)
    {
        ref byte address = ref Unsafe.As<Vector128<int>, byte>(ref value);
        return Unsafe.ReadUnaligned<Int3>(ref address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int4 AsInt4(this Vector128<int> value)
    {
        return Unsafe.BitCast<Vector128<int>, Int4>(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Double4 AsDouble4(this Vector256<double> value)
    {
        return Unsafe.BitCast<Vector256<double>, Double4>(value);
    }
}