using System.Numerics;
using static System.MathF;
public struct BoundingBox{
    public Vector2 Position;
    public float Width {get;}
    public float Height {get;}

    public BoundingBox(Vector2 pos, float w, float h){
        Position = pos;
        Width = w;
        Height = h;
    }
    public Vector2 Center(){
        float x = Position.X + Width/2;
        float y = Position.Y + Height/2;
        return new Vector2(x,y);
    }
}
public class Collider{
    public BoundingBox boundingBox;
    public float dX;
    public float dY;
    public Collider(Vector2 position, float width, float height)
    {
        boundingBox = new BoundingBox(position, width, height);
        dX = 0;
        dY = 0;
    }

    public bool CollisionCheck(Collider other){
        Vector2 center1 = boundingBox.Center();
        Vector2 center2 = other.boundingBox.Center();
        dY = Abs(center1.Y - center2.Y);
        dX = Abs(center1.X - center2.X);
        float minY = (other.boundingBox.Width + this.boundingBox.Width)/2;
        float minX = (other.boundingBox.Height + this.boundingBox.Height)/2;
        if(dY<minY&&dX<minX)
        return true;
        else return false; 
    }
}