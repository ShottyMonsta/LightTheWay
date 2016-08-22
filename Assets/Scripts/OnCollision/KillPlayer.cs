using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PlayerCharacter")
        {
            if (!other.gameObject.GetComponent<StateMachine>().Dead)
                other.gameObject.GetComponent<StateMachine>().Dead = true;
        }
        
    }
}
