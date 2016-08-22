using UnityEngine;
using System.Collections;

public class RepeatTileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponent<SpriteRenderer>().material.SetFloat("RepeatX", transform.localScale.x);
        GetComponent<SpriteRenderer>().material.SetFloat("RepeatY", transform.localScale.y);

    }

    void Update()
    {
        GetComponent<SpriteRenderer>().material.SetFloat("RepeatX", transform.localScale.x);
        GetComponent<SpriteRenderer>().material.SetFloat("RepeatY", transform.localScale.y);
    }
	
   
}
