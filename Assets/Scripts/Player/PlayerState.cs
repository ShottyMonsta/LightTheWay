using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {

    public float absVelX = 0f;
    public float absVelY = 0f;
    public bool standing;
    
    public float standingThreshold = 1;

    private Rigidbody2D body2D;
    //private HandelColour colour;

    void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
        //colour = GetComponent<HandelColour>();
    }
     
	// Use this for initialization
	void Start () {
	
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        absVelX = System.Math.Abs(body2D.velocity.x);
        absVelY = System.Math.Abs(body2D.velocity.y);

        standing = absVelY <= standingThreshold;
        

    }

    
}
