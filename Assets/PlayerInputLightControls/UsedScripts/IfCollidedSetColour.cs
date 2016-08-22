using UnityEngine;
using System.Collections;

public class IfCollidedSetColour : MonoBehaviour {


    public int colourNum;

    // Use this for initialization
    void Update()
    {
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
                if (hitInfo)
                {
                    PlayerPrefs.SetInt("Colour", colourNum);
                    // Here you can check hitInfo to see which collider has been hit, and act appropriately.
                }
            }
        }
    }
}
