using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoubleTap : MonoBehaviour
{
    public GameObject prefab;
    Vector3 newPos;
    public int currentColour;

    private float timeDecay = 0.001f;
    private float slowTime = 0.2f;
    private float timeSpeed;
    //public GameObject clone;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            
            if (Input.touches[0].tapCount == 2)
            {

                Time.timeScale = timeSpeed;
                if (newPos == new Vector3(0, 0, 0))
                {
                    newPos = Input.GetTouch(0).position;
                }
                prefab.transform.position = newPos;
                if(timeSpeed < 1)
                    timeSpeed += timeDecay;
            }
        }      
        else if (Input.GetMouseButton(1))
        {
            Time.timeScale = timeSpeed;
            if (newPos == new Vector3(0, 0, 0))
            {
                newPos = Input.mousePosition;
            }

            prefab.transform.position = newPos;
            if (timeSpeed < 1)
                timeSpeed += timeDecay;

        }
        else
        {
            Time.timeScale = 1f;
            timeSpeed = slowTime;
            newPos = new Vector3(0, 0, 0);
            prefab.transform.position = new Vector3(-1000, -1000, 0);
        }

    }
}
