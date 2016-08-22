using UnityEngine;
using System.Collections;

public class Walk : MonoBehaviour {

    public Vector2 velocity = Vector2.zero;
    public bool hitwall;
    private float colThreshold = 1f;
    public float absVelX = 0f;

    private Rigidbody2D body2D;
	// Use this for initialization
	void Awake () {

        body2D = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
    

        absVelX = System.Math.Abs(body2D.velocity.x);

        hitwall = absVelX <= colThreshold;

        if (hitwall)
        {
            velocity.x *= -1;
        }

        body2D.velocity = velocity;

        
    }
}
