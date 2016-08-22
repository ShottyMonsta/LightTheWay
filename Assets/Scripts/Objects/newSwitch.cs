using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class newSwitch : MonoBehaviour {

    public bool isActive = false;
    private bool isAnimating = false;

    private Animator animator;

    public GameObject[] objects = new GameObject[10];

    public float timer = 0f;

    private float elapsedTime = 0;

    private Collider2D objCollider;

    private SwitchEffect effect;

    private HandelColour colour;
    // Use this for initialization
    void Awake () {

        effect = GetComponent<SwitchEffect>();
        objCollider = GetComponent<BoxCollider2D>();
        colour = GetComponent<HandelColour>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator)
        {
            //if (isAnimating)
            //{
            //    int loops = (int)animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            //    float percentage = animator.GetCurrentAnimatorStateInfo(0).normalizedTime - (float)loops;
            //    if (percentage >= 0.90)
            //    {
            //        animator.SetBool("isActive", true);
            //        isAnimating = false;
            //    }
            //}
        }
        foreach (GameObject obj in objects)
        {
            if (obj)
                Debug.DrawLine(transform.position, obj.transform.position, Color.blue);
        }

        if (isActive)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timer)
            {
                isActive = false;
                TurnOnCollision();
                if(animator)
                {
                    animator.SetBool("isTurningOff", true);
                    animator.SetBool("isAnimating", false); 
                }

                foreach (GameObject obj in objects)
                {
                    if (obj)
                        effect.RemoveEffect(obj);
                    else
                        effect.RemoveEffect();
                }
            }
        }
    }

    void TurnOffCollision()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("DynamicObjects"))
        {
            if (obj.GetComponent<Collider2D>())
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

        if (coll.gameObject.tag == "PlayerCharacter"|| coll.gameObject.tag == "DynamicObjects")
        {
            if (!isActive)
            {
                elapsedTime = 0;
                isActive = true;
                TurnOffCollision();
                if (animator)
                {
                    animator.SetBool("isAnimating", true);
                    animator.SetBool("isTurningOff", false);
                    //isAnimating = true;
                }
                foreach (GameObject obj in objects)
                {
                    if (obj)
                        effect.ApplyEffect(obj);
                    else
                        effect.ApplyEffect();
                }
            }
        }
    }


}
