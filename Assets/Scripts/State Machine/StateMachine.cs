using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {

    protected State<GameObject> currentState;
    protected State<GameObject> previousState;

    public bool Standing;

    public bool Dead = false;

    public bool Spawned = false;

    public bool DeathAniFinished = false;

    public float stateTime = 0f;

    public float GravityScale;

    public Vector2 velocity;
    // Use this for initialization
    protected virtual void Start () {
        GravityScale = GetComponent<Rigidbody2D>().gravityScale;
	}

    public float StateTime
    {
        get { return stateTime; }
        set { stateTime = value; }
    }
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {

        stateTime += Time.fixedDeltaTime;
        
        Standing = System.Math.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) <= 0.5;

        if (Dead)
            ChangeState(DeadState.GetInstance);

        //if (DeathAniFinished && !Spawned)
            //Destroy(gameObject);

        if (currentState)
        {
            currentState.Excute(gameObject);
        }
        
    }



    public virtual void ChangeState(State<GameObject> newState)
    {
        if(newState)
        {
            stateTime = 0;
            if (currentState)
            {
                currentState.Exit(gameObject);
                previousState = currentState;
            }
            currentState = newState;
            currentState.Enter(gameObject);
        }
    }
}
