using UnityEngine;
using System.Collections;

public class BarrierOnOff : SwitchEffect {

    

    public override void ApplyEffect(GameObject entity)
    {
        entity.GetComponent<Barrier>().HideBarrier(colour.currentColour);
    }

    public override void ApplyEffect()
    {

    }

    public override void RemoveEffect(GameObject entity)
    {

        if (Revert)
        {
            entity.GetComponent<Barrier>().RevealBarrier(colour.currentColour);
        }
    }

    public override void RemoveEffect()
    {
    }
}
