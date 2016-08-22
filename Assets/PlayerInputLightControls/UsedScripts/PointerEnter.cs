using UnityEngine;

public class PointerEnter : MonoBehaviour
{
    public int colourNum;
    public void SetColour()
    {
        PlayerPrefs.SetInt("Colour", colourNum);
    }
}
