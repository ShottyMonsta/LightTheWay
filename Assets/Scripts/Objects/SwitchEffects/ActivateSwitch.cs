using UnityEngine;
using System.Collections;

public class ActivateSwitch : SwitchEffect
{

    public bool TurnOn = true;

    public override void ApplyEffect(GameObject entity)
    {
        entity.GetComponent<newSwitch>().isActive = TurnOn;
    }

    public override void RemoveEffect(GameObject entity)
    {
        if (Revert)
            entity.GetComponent<newSwitch>().isActive = !TurnOn;
    }
}

