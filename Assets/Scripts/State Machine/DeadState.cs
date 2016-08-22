using UnityEngine;
using System.Collections;

public class DeadState : State<GameObject> {

    private static DeadState instance = null;

    public static DeadState GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DeadState>();

                if (instance == null)
                {
                    GameObject container = new GameObject("DeadState singleton");
                    instance = container.AddComponent<DeadState>();
                }
            }
            return instance;
        }
    }

    // Update is called once per frame
    public override void Enter(GameObject entity)
    {
        entity.GetComponent<Animator>().SetBool("isDead", true);
    }
    public override void Excute(GameObject entity)
    {
        entity.GetComponent<StateMachine>().StateTime += Time.deltaTime;
        
        entity.GetComponent<StateMachine>().DeathAniFinished = true;

    }

    public override void Exit(GameObject entity)
    {
        entity.GetComponent<Animator>().SetBool("isDead", false);
    }
}

