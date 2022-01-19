using System.Numerics;
public class GameObject{
    public string Tag{get;}
    public int Height {get;}
    public int Width {get;}
    public char[] TextureBuffer;
    public Vector2 Position {get; set;}
    public Vector2 PreviousPosition {get; set;}
    public Vector2 Velocity{get; set;}
    public Collider collider{get;}
    public bool Active {get;set;}
    public GameObject(string tag, int w, int h, char[] texture, Vector2 position, Vector2 velocity, bool active=true){
        Tag = tag;
        Width = w;
        Height = h;
        TextureBuffer = texture;
        Position = position;
        PreviousPosition = Position;
        Velocity = velocity;
        collider = new Collider(Position, Width, Height);
        Active = active;
    }
    public void Update(){
        if(!Active)return;
        PreviousPosition = Position;
        Position += Velocity;
        UpdateCollider();
    }
    public void MoveBy(Vector2 d){
        if(!Active)return;
        PreviousPosition = Position;
        Position += d;
        UpdateCollider();
    }

    public bool CollisionCheck(GameObject other){
        if(other.collider==null)
            return false;
        return this.collider.CollisionCheck(other.collider);
    }
    private void UpdateCollider(){
        collider.boundingBox.Position = Position;
    }
}