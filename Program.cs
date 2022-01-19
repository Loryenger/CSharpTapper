using System.Numerics;
using System.Threading;
using static Tapper.Config;
namespace Tapper{
public class Program{
    
    public static void Main(){
        Tapper tapper = new Tapper();
        tapper.Setup();
        while(true){
        tapper.Update();
        }
    }

}
}