using UnityEngine;
using System.Collections;

public class Chase : State<GameObject> {

    private static Chase instance = null;

    public static Chase GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Chase>();

                if (instance == null)
                {
                    GameObject container = new GameObject("Chase singleton");
                    instance = container.AddComponent<Chase>();
                }
            }
            return instance;
        }
    }

    // Update is called once per frame
    public override void Enter(GameObject entity)
    {
        entity.GetComponent<StateMachine>().velocity.x *= 2;

    }
    public override void Excute(GameObject entity)
    {
        
        AIBehaviours.Move(entity);

        GameObject playerChar = GameObject.FindGameObjectWithTag("PlayerCharacter");
        Vector2 direction =   transform.TransformPoint(playerChar.transform.position) - entity.transform.position;
        direction.Normalize();
        RaycastHit2D lineOfSight = Physics2D.Raycast(
            entity.GetComponent<Rigidbody2D>().position + entity.GetComponent<StateMachine>().velocity.normalized,
            direction, 10f);
        Debug.DrawRay(
            entity.GetComponent<Rigidbody2D>().position + (entity.GetComponent<StateMachine>().velocity.normalized),
            direction.normalized * 10,
            Color.blue);

        if (lineOfSight.collider)
        {
            if (lineOfSight.collider.gameObject.tag != "PlayerCharacter")
            {
                entity.GetComponent<SlunkSM>().ChangeState(SlunkIdle.GetInstance);
            }

        }
        else
        {
            entity.GetComponent<SlunkSM>().ChangeState(SlunkIdle.GetInstance);
        }


    }
    public override void Exit(GameObject entity)
    {
        entity.GetComponent<StateMachine>().velocity.x /= 2;
    }
}
