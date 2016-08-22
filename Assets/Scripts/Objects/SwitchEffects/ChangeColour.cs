using UnityEngine;
using System.Collections;

public class ChangeColour : SwitchEffect {


    public override void ApplyEffect(GameObject entity)
    {
        if(entity.GetComponent<Barrier>())
        {
            entity.GetComponent<Barrier>().ChangeBarrierColour(colour.GetColourType);
        }
        else if(entity.GetComponent<HandelColour>())
        {
            entity.GetComponent<HandelColour>().baseColour = colour.GetColourType;
            entity.GetComponent<HandelColour>().currentColour = colour.GetColourType;
        }
        else if(entity.GetComponent<HandleLight>())
        {
            entity.GetComponent<HandleLight>().currentColour = colour.GetColourType;
        }
    }


    public override void RemoveEffect(GameObject entity)
    {

    }

 
}

