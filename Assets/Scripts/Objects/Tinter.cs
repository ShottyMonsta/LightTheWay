using UnityEngine;
using System.Collections;

public class Tinter : MonoBehaviour {

    private HandelColour colour;
    
    void Awake()
    {
        colour = GetComponent<HandelColour>();
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    // changes the base colour of dynamic objects
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag != "Obstical")
        if(coll.gameObject.GetComponent<HandelColour>())
        {
            coll.gameObject.GetComponent<HandelColour>().baseColour = colour.GetColourType;
            coll.gameObject.GetComponent<HandelColour>().ResetColour();
        }
    }
}
