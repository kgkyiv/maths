using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Kg.Kyiv.Mathematics;

public static class VectorExtensions
{
    extension(Int2 value)
    {
        public Vector128<int> AsVector128() => Int4.Create(value, 0, 0).AsVector128();

        [SkipLocalsInit]
        public Vector128<int> AsVector128Unsafe()
        {
            Unsafe.SkipInit(out Vector128<int> result);
            Unsafe.WriteUnaligned(ref Unsafe.As<Vector128<int>, byte>(ref result), value);
            return result;
        }
    }

    extension(Int3 value)
    {
        public Vector128<int> AsVector128() => Int4.Create(value, 0).AsVector128();

        [SkipLocalsInit]
        public Vector128<int> AsVector128Unsafe()
        {
            Unsafe.SkipInit(out Vector128<int> result);
            Unsafe.WriteUnaligned(ref Unsafe.As<Vector128<int>, byte>(ref result), value);
            return result;
        }
    }

    extension(Int4 value)
    {
        public Vector128<int> AsVector128() => Unsafe.BitCast<Int4, Vector128<int>>(value);
    }

    extension(Double2 value)
    {
        public Vector256<double> AsVector256() => Double4.Create(value, 0.0, 0.0).AsVector256();

        [SkipLocalsInit]
        public Vector256<double> AsVector256Unsafe()
        {
            Unsafe.SkipInit(out Vector256<double> result);
            Unsafe.WriteUnaligned(ref Unsafe.As<Vector256<double>, byte>(ref result), value);
            return result;
        }
    }

    extension(Double3 value)
    {
        public Vector256<double> AsVector256() => Double4.Create(value, 0.0).AsVector256();

        [SkipLocalsInit]
        public Vector256<double> AsVector256Unsafe()
        {
            Unsafe.SkipInit(out Vector256<double> result);
            Unsafe.WriteUnaligned(ref Unsafe.As<Vector256<double>, byte>(ref result), value);
            return result;
        }
    }

    extension(Double4 value)
    {
        public Vector256<double> AsVector256() => Unsafe.BitCast<Double4, Vector256<double>>(value);
    }

    extension(Vector128<int> value)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int2 AsInt2()
        {
            ref byte address = ref Unsafe.As<Vector128<int>, byte>(ref value);
            return Unsafe.ReadUnaligned<Int2>(ref address);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int3 AsInt3()
        {
            ref byte address = ref Unsafe.As<Vector128<int>, byte>(ref value);
            return Unsafe.ReadUnaligned<Int3>(ref address);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int4 AsInt4()
        {
            return Unsafe.BitCast<Vector128<int>, Int4>(value);
        }
    }

    extension(Vector256<double> value)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Double2 AsDouble2()
        {
            ref byte address = ref Unsafe.As<Vector256<double>, byte>(ref value);
            return Unsafe.ReadUnaligned<Double2>(ref address);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Double3 AsDouble3()
        {
            ref byte address = ref Unsafe.As<Vector256<double>, byte>(ref value);
            return Unsafe.ReadUnaligned<Double3>(ref address);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Double4 AsDouble4()
        {
            return Unsafe.BitCast<Vector256<double>, Double4>(value);
        }
    }
}