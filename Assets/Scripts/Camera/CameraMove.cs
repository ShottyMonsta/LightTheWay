using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public Transform playerPosition;

    private float screenWidth;
    private float screenHeight;

    private Vector2 playerPixelPos;


    public float bufferAmount;

    private Vector2 bufferZone;

    private Vector3 newCameraPosition;

    private float elapsedTime = 0;

    public float lerpSpeed = 1f;

    private bool screenMove;
   
	// Use this for initialization
	void Start () {
        screenHeight = Camera.main.pixelHeight;
        screenWidth = Camera.main.pixelWidth;
        bufferZone = new Vector2(screenWidth * bufferAmount, screenHeight * bufferAmount);
        
    }
	
	// Update is called once per frame
	void Update () {

        if (!screenMove)
        {
            playerPixelPos = Camera.main.WorldToScreenPoint(playerPosition.position);
            elapsedTime = 0;
            // move camera right
            if (playerPixelPos.x > screenWidth)
            {
                screenMove = true;
                newCameraPosition = Camera.main.ScreenToWorldPoint(new Vector2(screenWidth * 1.5f, screenHeight/2));
            }
            // move camera right
            if (playerPixelPos.x < 0)
            {
                screenMove = true;
                newCameraPosition = Camera.main.ScreenToWorldPoint(new Vector2(-screenWidth/2, screenHeight / 2));
            }
            // move camera up
            if (playerPixelPos.y > screenHeight)
            {
                screenMove = true;
                newCameraPosition = Camera.main.ScreenToWorldPoint(new Vector2(screenWidth/2, screenHeight * 1.5f));

            }
            // move camera down
            if (playerPixelPos.y < 0)
            {
                screenMove = true;
                newCameraPosition = Camera.main.ScreenToWorldPoint(new Vector2(screenWidth/2, - screenHeight/2));
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, newCameraPosition, elapsedTime / lerpSpeed);
            if(elapsedTime > lerpSpeed)
                screenMove = false;
        }



    }
}
