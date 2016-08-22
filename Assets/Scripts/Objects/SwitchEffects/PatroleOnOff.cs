using UnityEngine;
using System.Collections;

public class PatroleOnOff : SwitchEffect {

    public bool TurnOn = true;

    public override void ApplyEffect(GameObject entity)
    {
        entity.GetComponent<PatroleScript>().active = TurnOn;
    }

    public override void RemoveEffect(GameObject entity)
    {
        if (Revert)
            entity.GetComponent<PatroleScript>().active = !TurnOn;
    }
}
