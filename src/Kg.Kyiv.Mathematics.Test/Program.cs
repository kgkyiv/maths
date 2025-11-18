// See https://aka.ms/new-console-template for more information

using System.Numerics;
using Kg.Kyiv.Mathematics;
using Kg.Kyiv.Mathematics.Numerics;

Console.WriteLine(Meth.WrapDegrees(0.0));
Console.WriteLine(Meth.WrapDegrees(180.0));
Console.WriteLine(Meth.WrapDegrees(-180.0));
Console.WriteLine(Meth.WrapDegrees(360.0));
Console.WriteLine(Meth.WrapDegrees(720.0));
Console.WriteLine(Meth.WrapDegrees(-1024.0));
Console.WriteLine(Double2.Create(64.0) / 2.0);
Console.WriteLine(Double3.Dot(Double3.Create(0.0, 0.0, 0.0), Double3.Create(1.0, 1.0, 1.0)));
Console.WriteLine(Meth.IsZero(Vector2.Zero));
Console.WriteLine(Meth.IsZero(Vector2.One));
Console.WriteLine(Meth.SafeDiv(1.0f, 0.0f));
Console.WriteLine(Meth.SafeDiv(Vector3.One, 0.0f));