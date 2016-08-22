using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    // Use this for initialization
    public bool isActive = false;

    public GameObject prefab;

    

    public float timer = 0f;
    public float elapsedTime = 0;
    
    private Collider2D objCollider;

    private HandelColour colour;

    void Awake()
    {
        objCollider = GetComponent<BoxCollider2D>();
        colour = GetComponent<HandelColour>();
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        

        if(isActive)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= timer)
            {
                isActive = false;
                TurnOnObjects();
                TurnOnCollision();
            }
        }     
	}

    void TurnOffCollision()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("DynamicObjects"))
        {
            if(obj.GetComponent<Collider2D>())
            Physics2D.IgnoreCollision(objCollider, obj.GetComponent<Collider2D>(), true);
        }

        GameObject player = GameObject.FindGameObjectWithTag("PlayerCharacter");
        Physics2D.IgnoreCollision(objCollider, player.GetComponent<Collider2D>(), true);
    }

    void TurnOnCollision()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("DynamicObjects"))
        {
            if (obj.GetComponent<Collider2D>())
                Physics2D.IgnoreCollision(objCollider, obj.GetComponent<Collider2D>(), false);
        }

        GameObject player = GameObject.FindGameObjectWithTag("PlayerCharacter");
        Physics2D.IgnoreCollision(objCollider, player.GetComponent<Collider2D>(), false);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "PlayerCharacter")
        {       
            if (!isActive)
            {
                elapsedTime = 0;
                isActive = true;
                TurnOffCollision();
                //Physics2D.IgnoreCollision(objCollider, coll.collider, true);
                TurnOffObjects();
            }
        }
    }

    void TurnOffObjects()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag(prefab.tag))
        {
            if(obj.GetComponent<Barrier>())
            {
                obj.GetComponent<Barrier>().HideBarrier(colour.GetColourType);
            }
        }
    }

    void TurnOnObjects()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(prefab.tag))
        {
            if (obj.GetComponent<Barrier>())
            {
                obj.GetComponent<Barrier>().RevealBarrier(colour.GetColourType);
            }
        }
    }

    
}
