using UnityEngine;
using System.Collections;

public class SlunkSM : StateMachine {

    public bool nearLight = false;

    public bool nearEdge = false;


    private bool InMiasma = false;
    public bool Fleeing = false;

    public bool NearLight
    {
        get { return nearLight; }
        set { nearLight = value; }
    }

    public void Spawn(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;
        Dead = false;
        DeathAniFinished = false;
        StateTime = 10f;
        ChangeState(SlunkIdle.GetInstance);
    }

    protected override void Start()
    {
        //velocity.x *= -1;
        currentState = SlunkIdle.GetInstance;
    }

    protected override void FixedUpdate()
    {
        
        
        
        base.FixedUpdate();
        Debug.Log(stateTime);

        if (velocity.x >= 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Miasma>())
            InMiasma = true;
        if(other.gameObject.GetComponent<HandleLight>())
        {
            if(!InMiasma && !Dead)
                Dead = true;
        }
        if (other.tag == "LightArea" && ! InMiasma)
            nearLight = true;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "LightArea")
            nearLight = false;

        if (other.GetComponent<Miasma>())
            InMiasma = false;
    }
}
