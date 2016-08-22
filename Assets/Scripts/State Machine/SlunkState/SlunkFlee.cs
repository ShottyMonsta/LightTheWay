using UnityEngine;
using System.Collections;

public class SlunkFlee : State<GameObject>
{
    private static float fleeTime = 4f;
    private static float resetTime = 1f;

    private static SlunkFlee instance = null;

    public static SlunkFlee GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SlunkFlee>();

                if (instance == null)
                {
                    GameObject container = new GameObject("SlunkFlee singleton");
                    instance = container.AddComponent<SlunkFlee>();
                }
            }
            return instance;
        }
    }

    // Update is called once per frame
    public override void Enter(GameObject entity)
    {
        entity.GetComponent<SlunkSM>().Fleeing = true;
        entity.GetComponent<StateMachine>().velocity.x *= -2;
    }
    public override void Excute(GameObject entity)
    {
        SlunkSM machine = entity.GetComponent<SlunkSM>();

        if (fleeTime < machine.StateTime)
            machine.ChangeState(SlunkIdle.GetInstance);

        if (resetTime < machine.StateTime)
        {
            
            if (machine.NearLight)
            {
                machine.velocity.x *= -1;
                machine.StateTime = 0f;
            }
        }

        AIBehaviours.Turn(entity);
        AIBehaviours.Move(entity);

    }

    public override void Exit(GameObject entity)
    {
        entity.GetComponent<SlunkSM>().Fleeing = false;
        entity.GetComponent<StateMachine>().velocity.x /= 2;
    }
}
