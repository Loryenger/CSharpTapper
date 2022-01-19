using System;
using System.IO;
using System.Buffers;
using System.Numerics;
public class Graphics{
    public int BufferWidth {get;}
    public int BufferHeight {get;}
    private char[] frameBuffer;
    private char[] screenBuffer;
    private static Graphics instance;
    private Graphics(int w, int h){
        BufferWidth = w;
        BufferHeight = h;
        frameBuffer = new char[BufferHeight * BufferWidth];
        screenBuffer = new char[BufferHeight * BufferWidth];
        Console.CursorVisible = false;
        Console.SetWindowSize(1, 1);
        Console.SetBufferSize(BufferWidth, BufferHeight);
        Console.SetWindowSize(BufferWidth, BufferHeight);
        ClearFrameBuffer(' ');
        CleanScreenBuffer('\0');      
    }

    public static Graphics GetInstance(int w, int h){
        if(instance == null)
        instance = new Graphics(w, h);
        return instance;
    }

    public void ClearFrameBuffer(char c){
        for(int i=0; i<BufferHeight; i++){
            for(int j=0; j<BufferWidth; j++){
                frameBuffer[i*BufferWidth+j] = c;
            }
        }
    }

    public void CleanScreenBuffer(char c){
        for(int i=0; i<BufferHeight; i++){
            for(int j=0; j<BufferWidth; j++){
                screenBuffer[i*BufferWidth+j] = c;
            }
        }
    }
    public void DrawToFrameBuffer(GameObject gameObject){
        if(!gameObject.Active)return;
        if(!gameObject.PreviousPosition.Equals(gameObject.Position)){
           Erase(gameObject, false);
        }
        for(int i=0; i<gameObject.Height; i++){
            int dstOffset = ((int)gameObject.Position.Y + i)*BufferWidth*sizeof(char) + (int)gameObject.Position.X*sizeof(char);
            int srcOffset = gameObject.Width*i*sizeof(char);
            int count = gameObject.Width * sizeof(char);
            Buffer.BlockCopy(gameObject.TextureBuffer, srcOffset, frameBuffer, dstOffset, count);
        }
    }

    public void Erase(GameObject gameObject, bool which){
        if(!gameObject.Active)return;
        Vector2 p;
        if(which) p = gameObject.Position;
        else p = gameObject.PreviousPosition;
        int h = gameObject.Height;
        int w = gameObject.Width;
        for(int y=0; y<h; y++){
            for(int x=0; x<w; x++){
                if(gameObject.TextureBuffer[y*w+x]==frameBuffer[(y+(int)p.Y)*BufferWidth+(x+(int)p.X)])
                    frameBuffer[(y+(int)p.Y)*BufferWidth+(x+(int)p.X)]=' ';
            }
        }
        
    }
    
    public bool ScreenBoundCheck(float x, float y){
        return (x<BufferWidth && x>-1 && y<BufferHeight && y>-1)? true : false;
    }
    public void WriteToScreen(){
        for(int i=0; i<BufferHeight; i++){
            for(int j=0; j<BufferWidth; j++){
                int index = i * BufferWidth + j;
                if(screenBuffer[index]==frameBuffer[index])
                    continue;
                if(frameBuffer[index]!=' ')
                Console.BackgroundColor = (ConsoleColor) frameBuffer[index];
                else Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(j, i);
                Console.Write(' ');
            }
        }
        Console.Out.Flush();
        Buffer.BlockCopy(frameBuffer, 0, screenBuffer, 0, BufferHeight*BufferWidth*sizeof(char));
    }
    public void WriteFrameToScreen(){
        Console.Clear();
        Console.Write(frameBuffer, 0, BufferHeight*BufferWidth);  
       // Buffer.BlockCopy(frameBuffer, 0, screenBuffer, 0, BufferHeight*BufferWidth*sizeof(char));
    }
/*        bool validPos = ScreenBoundCheck(gameObject.Position.X, gameObject.Position.Y)&
        ScreenBoundCheck(gameObject.Position.X+gameObject.Width, gameObject.Position.Y)&
        ScreenBoundCheck(gameObject.Position.X, gameObject.Position.Y+gameObject.Height)&
        ScreenBoundCheck(gameObject.Position.X+gameObject.Width, gameObject.Position.Y+gameObject.Height);
        if(!validPos)*/
}