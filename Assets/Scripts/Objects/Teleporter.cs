using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    private bool regersted = false;

    private int id;

    private bool active;

    private HandelColour colour;

    private Teleporter exitTeleporter;

    private Vector3 exitOffset = new Vector3(1.5f, 0,0);

    private Animator anim;
    private bool isAnimating = false;
    private float animStartTime;
    private float animLength = 0.5f;

    public void setID(int index)
    {
        regersted = true;
        id = index;
    }

    public int GetID
    {
        get { return id; }
    }
    public ColourLayer.Type GetColour
    {
        get { return colour.GetColourType; }
    }
	// Use this for initialization
	void Start ()
    {
        anim = GetComponentInChildren<Animator>();
        colour = GetComponent<HandelColour>();
        TeleportManager.GetInstance.Register(this);
	}
	
	// Update is called once per frame
	void Update () {

        exitTeleporter = TeleportManager.GetInstance.FindActiveTeleporter(this);

        if(exitTeleporter)
        {
            Debug.DrawLine(transform.position, exitTeleporter.transform.position,Color.yellow);
        }
        if (isAnimating)
        {
            if (Time.time >= animStartTime + animLength)
            {
                anim.SetBool("isAnimating", false);
                isAnimating = false;
            }
        }       
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(exitTeleporter)
        {
            if(other.tag == "PlayerCharacter"|| other.tag == "DynamicObjects")
            {
                if(other.attachedRigidbody.velocity.x > 0)
                {
                    other.transform.position = exitTeleporter.transform.position + exitOffset;
                }
                else
                {
                    other.transform.position = exitTeleporter.transform.position - exitOffset;
                }
                anim.SetBool("isAnimating", true);
                animStartTime = Time.time;
                isAnimating = true;
            }
        }
    }
}
