using UnityEngine;
using UnityEngine.SceneManagement;

public class InputState : MonoBehaviour {


    private Vector3 mousePosition;
    public float mouseSpeed = 1f;
    public float depth = 0f;

    private HandleLight colour;

    private int currentColour;

    public Vector3 offSet = new Vector3(0, 1,0);

	// Use this for initialization
	void Start () {

        colour = GetComponent<HandleLight>();
        PlayerPrefs.SetInt("Colour", 1);
        currentColour = PlayerPrefs.GetInt("Colour");
	}


	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButton(0))
        {
            mousePosition = offSet + Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        else
        {
            mousePosition = new Vector2(-100, -100);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            SceneManager.LoadScene("World1");
        }


        transform.position = new Vector3(mousePosition.x, mousePosition.y, depth);
        //transform.position = Vector3.Lerp(transform.position, new Vector3(mousePosition.x, mousePosition.y, depth), mouseSpeed);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            colour.currentColour = ColourLayer.Type.RED;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            colour.currentColour = ColourLayer.Type.GREEN;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            colour.currentColour = ColourLayer.Type.BLUE;
        }

        if(currentColour != PlayerPrefs.GetInt("Colour"))
        {
            
            if (PlayerPrefs.GetInt("Colour") == 1)
            {
                colour.currentColour = ColourLayer.Type.RED;
            }
            if (PlayerPrefs.GetInt("Colour") == 2)
            {
                colour.currentColour = ColourLayer.Type.BLUE;
            }
            if (PlayerPrefs.GetInt("Colour") == 3)
            {
                colour.currentColour = ColourLayer.Type.GREEN;
            }

            currentColour = PlayerPrefs.GetInt("Colour");

        }

    }
}
