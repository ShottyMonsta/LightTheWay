using UnityEngine;
using System.Collections;


public class Ladder : MonoBehaviour {


    

   

    private Vector2 top;

    private Vector2 bottom;

    private float enteryPoints = 1;

    
	// Use this for initialization
	void Start () {

        top = new Vector2(transform.position.x, transform.position.y + (transform.localScale.y / 2));
        bottom = new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2));
        
    }

    public Vector2 GetTop
    {
        get { return top; }
    }

    public Vector2 GetBottom
    {
        get { return bottom; }
    }

    // Update is called once per frame
    void Update()
    {
        top = new Vector2(transform.position.x, transform.position.y + (transform.localScale.y / 2));
        bottom = new Vector2(transform.position.x, transform.position.y - (transform.localScale.y / 2));



    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(top, bottom);

    }
}
