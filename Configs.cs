namespace Tapper{
    public static class Config{
    public static string title = "Tapper";
    public static int consoleWidth = 100;
    public static int consoleHeight = 40;
    public static int barNum = 4;
    public static int barWidth = 70;
    public static int barHeight = 3;
    public static int barGap = 6;
    public static int doorNum = 4;
    public static int doorWidth = 3;
    public static int doorHeight = 6;
    public static int playerWidth = 6;
    public static int playerHeight = 5;

    public static int kegWidth = 5;
    public static int kegHeight = 5;
    public static int kegNum = 4;
    public static int kegGap = 4;
    public static int playerInitPosX = consoleWidth - kegWidth - playerWidth - 3;
    public static int playerInitPoxY = barGap-1;
    public static int customerInitPosX = doorWidth+2;
    public static int customerNum = 4;
    public static char barColor = (char)ConsoleColor.DarkRed;
    public static char playerColor = (char)ConsoleColor.Green;
    public static char customerColor = (char)ConsoleColor.Blue;
    public static char kegColor = (char)ConsoleColor.DarkYellow;
    public static char doorColor = (char)ConsoleColor.Gray;
    public static char drinkColor = (char)ConsoleColor.Yellow;
    public static char[] barTexture = new char[barWidth*barHeight];
    public static char[] playerTexture = {' ',' ',playerColor,playerColor,' ',' ',
    playerColor,playerColor,playerColor,playerColor,playerColor,playerColor,
    ' ',' ',playerColor,playerColor,' ',' ',
    playerColor,playerColor,playerColor,playerColor,playerColor,playerColor,
    playerColor,playerColor,' ',' ',playerColor,playerColor
    };
    public static char[] kegTexture = {' ',' ',kegColor,kegColor,kegColor,
    kegColor,kegColor,kegColor,kegColor,kegColor,
    kegColor,' ',kegColor,kegColor,kegColor,
    ' ',' ',kegColor,kegColor,kegColor,
    ' ',' ',kegColor,kegColor,kegColor
    };
    public static char[] doorTexture = {' ',' ',doorColor,
    ' ',' ',doorColor,
    doorColor,doorColor,' ',
    doorColor,doorColor,' ',
    ' ',' ',doorColor,
    ' ',' ',doorColor
    };
    public static char[] customerTexture = {' ',' ',customerColor,customerColor,' ',' ',
    customerColor,customerColor,customerColor,customerColor,customerColor,customerColor,
    ' ',' ',customerColor,customerColor,' ',' ',
    customerColor,customerColor,customerColor,customerColor,customerColor,customerColor,
    customerColor,customerColor,' ',' ',customerColor,customerColor
    };
    public static char[] drinkTexture = {drinkColor, drinkColor};
    }
}