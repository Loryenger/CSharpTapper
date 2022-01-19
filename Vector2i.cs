using System.Diagnostics.CodeAnalysis;
using static System.MathF;
public struct Vector2i:IEquatable<Vector2i> {
    public int X;
    public int Y;
    public Vector2i(int x, int y){
        X = x;
        Y = y;
    }
    // override object.Equals
    public bool Equals(Vector2i other){
        return other.X==X & other.Y==Y;
    }
    public override bool Equals(Object obj)
    {
       if (! (obj is Vector2i)) return false;

       Vector2i p = (Vector2i) obj;
       return Equals(p);
    }

    public override int GetHashCode()
    {
        return X ^ Y;
    }
    public static Vector2i operator+(Vector2i a, Vector2i b){
        return new Vector2i(a.X+b.X, a.Y+b.Y);
    }
    public static Vector2i operator-(Vector2i a, Vector2i b){
        return new Vector2i(a.X-b.X, a.Y-b.Y);
    }

    public static float Distance(Vector2i a, Vector2i b){
       return Sqrt((a.X-b.X)*(a.X-b.X)+(a.Y-b.Y)*(a.Y-b.Y));
    }
    public static Vector2i One = new Vector2i(1, 1);
    public static Vector2i Zero = new Vector2i(0, 0);
}