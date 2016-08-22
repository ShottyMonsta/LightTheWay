using UnityEngine;
using System.Collections;

public class SwitchEffect : MonoBehaviour {

    protected HandelColour colour;

    public bool Revert = true;

    private void Start()
    {
        colour = GetComponent<HandelColour>();
    }

    public virtual void ApplyEffect(GameObject entity)
    {
    }

    public virtual void ApplyEffect()
    {

    }

    public virtual void RemoveEffect(GameObject entity)
    {
    }

    public virtual void RemoveEffect()
    {
    }

    

}
