using UnityEngine;


public class PatroleScript : MonoBehaviour {

    // points to lerp between 
    public Vector2 pointOne;
    public Vector2 pointTwo;

    // speed
    public float speed = 5f;

    //time spent at each point;
    public float hangTime = 0f;

    //if true object will move between points
    public bool active = true;

    private float elapsedTime = 0f;

    private float hangClock = 0f;

   
    private float direction = 1;
	// Use this for initialization
	void Start () {
        pointOne += new Vector2(transform.position.x,transform.position.y);
        pointTwo += new Vector2(transform.position.x, transform.position.y);


    }

 

    // Update is called once per frame
    void Update() {

        Debug.DrawLine(pointOne, pointTwo, Color.green);

        if (active)
        {

         
            Vector2 newPos = Vector2.Lerp(pointOne, pointTwo, elapsedTime / speed);

            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

            hangClock += Time.deltaTime;

            if (hangTime < hangClock)
            {
                elapsedTime += (Time.deltaTime * direction);
            }

            if (elapsedTime >= speed && direction > 0)
            {
                direction *= -1;
                hangClock = 0;
            }
            else if (elapsedTime <= 0 && direction < 0)
            {

                direction *= -1;
                hangClock = 0;
            }
        }
        else
        {
            hangClock = hangTime;
        }

       
                
	}

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pointOne + new Vector2(transform.position.x, transform.position.y), pointTwo + new Vector2(transform.position.x, transform.position.y));
    }
}
