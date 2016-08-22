using UnityEngine;
using System.Collections;

public class ParentColour : MonoBehaviour {

    private SpriteRenderer sprite;
	// Use this for initialization
	void Start () {

        sprite = GetComponent<SpriteRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () {

        sprite.color = gameObject.GetComponentInParent<SpriteRenderer>().color;
	}
}
