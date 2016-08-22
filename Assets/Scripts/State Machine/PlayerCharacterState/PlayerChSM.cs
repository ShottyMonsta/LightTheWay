using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerChSM : StateMachine {

    public bool climb = false;
    private Vector2 start;
    private Vector2 end;

    public bool justDied;

    public float climbSpeedRatio = 0.5f;
    private float climbSpeed;

    public Vector3 bodyVol;

    private Animator anim;

    public Vector2 StartLadder
    {
        get { return start; }
    }

    public Vector2 EndLadder
    {
        get { return end; }
    }

    protected override void Start()
    {
        //velocity.x *= -1;
        currentState = Idle.GetInstance;
        anim = gameObject.GetComponent<Animator>();
        base.Start();
    }

   

    public float GetClimbSpeed
    {
        get { return climbSpeed; }
    }

    protected override void FixedUpdate()
    {
        bodyVol = GetComponent<Rigidbody2D>().velocity;
        base.FixedUpdate();

        if (velocity.x <= 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
        if(!Dead)
        {
            justDied = true;
        }

        if(Dead && justDied)
        {
            justDied = false;
            GameObject.Find("GameOver").transform.position = GameObject.Find("Canvas").transform.position;
            GameObject.Find("Continue").SetActive(false);
            GameObject.Find("Level Complete").SetActive(false);
            GameObject.Find("[Time]").SetActive(false);
            GameObject.Find("Time:").SetActive(false);
            GameObject.Find("Star1Image").SetActive(false);
            GameObject.Find("Star2Image").SetActive(false);
            GameObject.Find("Star3Image").SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            anim.SetBool("isClimbing", true);
            climb = true;
            Ladder l = other.gameObject.GetComponent<Ladder>();
            climbSpeed = l.transform.localScale.y * climbSpeedRatio;
            if (Vector2.Distance(transform.position,l.GetBottom) < Vector2.Distance(transform.position, l.GetTop))
            {
                start = l.GetBottom;
                end = l.GetTop;
            }
            else
            {
                end = l.GetBottom;
                start = l.GetTop;
            }

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            anim.SetBool("isClimbing", false);
            climb = false;
        }
    }

    public bool ClimbLadder
    {
        get { return climb; }
    }

}
