using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColourLayer : System.Object {

    public enum Type { RED, GREEN, BLUE, CYAN, MAGENTA, YELLOW, PINK, PURPLE, ORANGE, SKYBLUE, LIME, TURQUOISE, WHITE, BLACK }
    

    public static int RedLayer { get { return 11; } }
    public static int GreenLayer { get { return 12; } }
    public static int BlueLayer { get { return 13; } }
    public static int CyanLayer { get { return 14; } }
    public static int MagentaLayer { get { return 15; } }
    public static int YellowLayer { get { return 16; } }
    public static int PinkLayer { get { return 17; } }
    public static int PurpleLayer { get { return 18; } }
    public static int OrangeLayer { get { return 19; } }
    public static int SkyblueLayer { get { return 20; } }
    public static int LimeLayer { get { return 21; } }
    public static int TurquoiseLayer { get { return 22; } }
    public static int WhiteLayer { get { return 23; } }
    public static int BlackLayer { get { return 24; } }

    public static Type BlendColour(Type addedColour, Type baseColour)
    {
        Type newColour = baseColour;
        switch (baseColour)
        {
            case Type.BLUE:
                switch (addedColour)
                {
                    case Type.BLUE:
                        newColour = Type.BLUE;
                        break;
                    case Type.GREEN:
                        newColour = Type.CYAN;
                        break;
                    case Type.RED:
                        newColour = Type.MAGENTA;
                        break;
                    case Type.CYAN:
                        newColour = Type.SKYBLUE;
                        break;
                    case Type.YELLOW:
                        newColour = Type.WHITE;
                        break;
                    case Type.MAGENTA:
                        newColour = Type.PURPLE;
                        break;
                }
                break;
            case Type.CYAN:
                switch (addedColour)
                {
                    case Type.BLUE:
                        newColour = Type.SKYBLUE;
                        break;
                    case Type.GREEN:
                        newColour = Type.TURQUOISE;
                        break;
                    case Type.RED:
                        newColour = Type.WHITE;
                        break;
                }
                break;
            case Type.GREEN:
                switch (addedColour)
                {
                    case Type.BLUE:
                        newColour = Type.CYAN;
                        break;
                    case Type.GREEN:
                        newColour = Type.GREEN;
                        break;
                    case Type.RED:
                        newColour = Type.YELLOW;
                        break;
                    case Type.CYAN:
                        newColour = Type.TURQUOISE;
                        break;
                    case Type.YELLOW:
                        newColour = Type.LIME;
                        break;
                    case Type.MAGENTA:
                        newColour = Type.WHITE;
                        break;
                }
                break;
            case Type.MAGENTA:
                switch (addedColour)
                {
                    case Type.BLUE:
                        newColour = Type.PURPLE;
                        break;
                    case Type.GREEN:
                        newColour = Type.WHITE;
                        break;
                    case Type.RED:
                        newColour = Type.PINK;
                        break;
                }
                break;
            case Type.RED:
                switch (addedColour)
                {
                    case Type.BLUE:
                        newColour = ColourLayer.Type.MAGENTA;
                        break;
                    case Type.GREEN:
                        newColour = Type.YELLOW;
                        break;
                    case Type.RED:
                        newColour = Type.RED;
                        break;
                    case Type.CYAN:
                        newColour = Type.WHITE;
                        break;
                    case Type.YELLOW:
                        newColour = Type.ORANGE;
                        break;
                    case Type.MAGENTA:
                        newColour = Type.PINK;
                        break;
                }
                break;
            case Type.YELLOW:
                switch (addedColour)
                {
                    case Type.BLUE:
                        newColour = Type.WHITE;
                        break;
                    case Type.GREEN:
                        newColour = Type.LIME;
                        break;
                    case Type.RED:
                        newColour = Type.ORANGE;
                        break;
                }
                break;
            case Type.WHITE:
                newColour = addedColour;
                break;
        }

        return newColour;
    }

    public static Vector2 RotateVec(Vector2 vec, float degrees)
    {
        float radians = -degrees * Mathf.Deg2Rad;

        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        Vector2 newVec = new Vector2(cos * vec.x - sin * vec.y, sin * vec.x + cos * vec.y);
        newVec.Normalize();
        return newVec;
    }

}
