using System.Runtime.Intrinsics;

namespace Kg.Kyiv.Mathematics.Extensions;

public static class VectorExtensions
{
    extension<T>(Vector64<T> vector)
    {
        public T X => vector.GetElement(0);
        public T Y => vector.GetElement(1);
        public T Z => vector.GetElement(2);
        public T W => vector.GetElement(3);
    }
    
    extension<T>(Vector128<T> vector)
    {
        public T X => vector.GetElement(0);
        public T Y => vector.GetElement(1);
        public T Z => vector.GetElement(2);
        public T W => vector.GetElement(3);
    }
    
    extension<T>(Vector256<T> vector)
    {
        public T X => vector.GetElement(0);
        public T Y => vector.GetElement(1);
        public T Z => vector.GetElement(2);
        public T W => vector.GetElement(3);
    }
}