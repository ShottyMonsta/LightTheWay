using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour {

    private HandelColour colour;

    void Start()
    {
        colour = GetComponent<HandelColour>();
    }
    // if collides with player character kill
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<StateMachine>())
        {
            if(!coll.gameObject.GetComponent<StateMachine>().Dead)
                coll.gameObject.GetComponent<StateMachine>().Dead = true;
            
        }
    }

}
