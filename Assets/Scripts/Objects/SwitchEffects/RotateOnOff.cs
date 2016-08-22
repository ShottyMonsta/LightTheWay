using UnityEngine;
using System.Collections;

public class RotateOnOff : SwitchEffect{

    

    public bool TurnOn = true;

    public override void ApplyEffect(GameObject entity)
    {
        entity.GetComponent<RotateBetween>().active = TurnOn;
    }

    public override void RemoveEffect(GameObject entity)
    {
        if (Revert)
            entity.GetComponent<RotateBetween>().active = !TurnOn;
    }

}
