using UnityEngine;
using System.Collections;

public class LightOnOff : SwitchEffect {

    //*** no relation between switch colour and light colour ***

    public override void ApplyEffect(GameObject entity)
    {

        

        if (entity.GetComponent<BeamLight>())
        {
            if(entity.GetComponent<BeamLight>().On)
                entity.GetComponent<BeamLight>().TurnLightOff();
            else
                entity.GetComponent<BeamLight>().TurnLightOn();
        }

        if(entity.GetComponent<ConeLight>())
        {
            if (entity.GetComponent<ConeLight>().On)
                entity.GetComponent<ConeLight>().TurnLightOff();
            else
                entity.GetComponent<ConeLight>().TurnLightOn();
        }

        
    }

    public override void ApplyEffect()
    {

    }

    public override void RemoveEffect(GameObject entity)
    {
        if (Revert)
        {
            if (entity.GetComponent<BeamLight>())
            {
                if (entity.GetComponent<BeamLight>().On)
                    entity.GetComponent<BeamLight>().TurnLightOff();
                else
                    entity.GetComponent<BeamLight>().TurnLightOn();
            }

            if (entity.GetComponent<ConeLight>())
            {
                if (entity.GetComponent<ConeLight>().On)
                    entity.GetComponent<ConeLight>().TurnLightOff();
                else
                    entity.GetComponent<ConeLight>().TurnLightOn();
            }
        }

    }

    public override void RemoveEffect()
    {
    }
}
