using UnityEngine;
using System.Collections;

public class Idle : State<GameObject> {

    // Use this for initialization
    private static Idle instance = null;

    public static Idle GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Idle>();

                if (instance == null)
                {
                    GameObject container = new GameObject("idle singleton");
                    instance = container.AddComponent<Idle>();
                }
            }
            return instance;
        }
    }
	
	// Update is called once per frame
	public override void Enter(GameObject entity)
    {
        
    }
    public override void Excute(GameObject entity)
    {
       

        AIBehaviours.Turn(entity);
        

        AIBehaviours.Move(entity);


        if (entity.GetComponent<PlayerChSM>().ClimbLadder)
            entity.GetComponent<PlayerChSM>().ChangeState(Climb.GetInstance);

    }
    public override void Exit(GameObject entity)
    {

    }

}
