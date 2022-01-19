using static Tapper.Config;
using System.Numerics;
using System;
namespace Tapper{
    public class Tapper{
        public long frameCount;
        Graphics graphics;
        GameObject[] bars;
        GameObject[] kegs;
        GameObject[] doors;
        GameObject[] customers;
        GameObject player;
        List<GameObject> drinks;

        public Tapper(){
            Console.Title = title;
            graphics = Graphics.GetInstance(consoleWidth, consoleHeight);
            bars = new GameObject[barNum];
            kegs = new GameObject[kegNum];
            doors = new GameObject[doorNum];
            customers = new GameObject[customerNum];
            drinks = new List<GameObject>();         
            Vector2 playerPos = new Vector2(playerInitPosX, playerInitPoxY);
            player = new GameObject("Player", playerWidth, playerHeight, playerTexture, playerPos, Vector2.Zero);
        }
        public void Setup(){
            SetupBars();
            SetupKegs();
            SetupDoors();
            SetupCustomers();
            Thread.Sleep(6000);
        }
        private void SetupBars(){
            for(int i=0; i<barWidth*barHeight; i++)
            barTexture[i] = barColor; 
            for(int i=0; i<barNum; i++){
                Vector2 pos = new Vector2(10, i*(barHeight+barGap)+barGap+2);
                bars[i] = new GameObject("Bar", barWidth, barHeight, barTexture, pos, Vector2.Zero);
                graphics.DrawToFrameBuffer(bars[i]);
            }
         }
        private void SetupKegs(){
             for(int i=0; i<kegNum; i++){
                 Vector2 pos = new Vector2(consoleWidth-kegWidth, i*(kegHeight+kegGap)+kegGap+3);
                 Vector2 velocity = new Vector2(0,0);
                 kegs[i] = new GameObject("Keg", kegWidth, kegHeight, kegTexture, pos, velocity);
                 graphics.DrawToFrameBuffer(kegs[i]);
            }
        }
        private void SetupDoors(){
             for(int i=0; i<doorNum; i++){
                 Vector2 pos = new Vector2(0, i*(kegHeight+kegGap)+kegGap+1);
                 Vector2 velocity = new Vector2(0,0);
                 doors[i] = new GameObject("Door", doorWidth, doorHeight, doorTexture, pos, velocity);
                 graphics.DrawToFrameBuffer(doors[i]);
            }
        }

        private void SetupCustomers(){
            for(int i=0; i<doorNum; i++){
                 Vector2 pos = new Vector2(customerInitPosX, i*(barHeight+barGap)+barGap+1-playerHeight);
                 Random rand = new Random();
                 float x = 0.5f + rand.NextSingle()*0.3f;
                 Vector2 velocity = new Vector2(x,0);
                 customers[i] = new GameObject("Customer", playerWidth, playerHeight, customerTexture, pos, velocity);
                 //graphics.DrawToFrameBuffer(customers[i]);
            }
        }
        public void HandleKeyEvent(){
            if(Console.KeyAvailable){
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Vector2 left = new Vector2(-1,0);
            Vector2 right = new Vector2(1, 0);
            Vector2 up = new Vector2(0,-9);
            Vector2 down = new Vector2(0,9);
            switch(keyInfo.Key){

            case ConsoleKey.LeftArrow:
            player.MoveBy(left);
            BoundCheck(player);
            break;

            case ConsoleKey.RightArrow:
            player.MoveBy(right);
            BoundCheck(player); 
            break;

            case ConsoleKey.UpArrow:
            player.MoveBy(up);
            BoundCheck(player);
            break;

            case ConsoleKey.DownArrow:
            player.MoveBy(down);
            BoundCheck(player);
            break;

            case ConsoleKey.A:
            SlideDrink();
            break;

            default:
            break;

        }
        }
    } 
        
        public void Update(){
            frameCount++;
            UpdateCumstomers();
            UpdateDrinks();
            UpdateBars();
            HandleKeyEvent();
            if(frameCount>=3)//Magic, I don't know why but it works.
            graphics.DrawToFrameBuffer(player);
            graphics.WriteToScreen();
            Thread.Sleep(30);
        }

        private void BoundCheck(GameObject obj){
            Vector2 p = obj.Position;
            float w = obj.Width;
            float h = obj.Height;
            bool valid = graphics.ScreenBoundCheck(p.X, p.Y)&
                graphics.ScreenBoundCheck(p.X+w, p.Y)&
                graphics.ScreenBoundCheck(p.X, p.Y+h)&
                graphics.ScreenBoundCheck(p.X+w, p.Y+h);
            if(!valid){
                obj.Position = obj.PreviousPosition;
            }
        }

        private void UpdateCumstomers(){
            foreach(GameObject customer in customers){
                customer.Update();
                BoundCheck(customer);
                HandleCollision(customer, player);
                foreach(GameObject keg in kegs)
                if(HandleCollision(customer, keg))break;
                foreach(GameObject door in doors)
                if(HandleCollision(customer, door))break;
                foreach(GameObject drink in drinks)
                if(HandleCollision(customer, drink))break;
                if(frameCount>3)
                graphics.DrawToFrameBuffer(customer);
            }
        }

        private void UpdateBars(){
            foreach(GameObject bar in bars){
                bar.Update();
                graphics.DrawToFrameBuffer(bar);
            }
        }
        private bool HandleCollision(GameObject customer, GameObject other){
            if(!customer.CollisionCheck(other)||other==null)
            return false;
            if(other.Tag=="Drink"){
                graphics.Erase(other,true);
                other.Active=false;
            }
            float dx = customer.collider.dX;
            //customer.MoveBy(new Vector2(-dx, 0));
            customer.Velocity = new Vector2(-customer.Velocity.X, customer.Velocity.Y);
            return true;
        }

        private void SlideDrink(){
            float x = player.Position.X;
            float y = player.Position.Y+1;
            Vector2 pos = new Vector2(x, y);
            Vector2 vel = new Vector2(-1.5f, 0);
            drinks.Add(new GameObject("Drink", 2, 1, drinkTexture, pos, vel));
            graphics.DrawToFrameBuffer(drinks.Last());
        }

        private void UpdateDrinks(){
            foreach(GameObject drink in drinks){
                foreach(GameObject door in doors){
                    if(drink.CollisionCheck(door)){
                        graphics.Erase(drink, true);
                        drink.Active = false;
                        break;
                    }
                }
            }
            drinks.RemoveAll(obj=>!obj.Active);
            foreach(GameObject drink in drinks){
                drink.Update();
                graphics.DrawToFrameBuffer(drink);
            }
        }
    }
}