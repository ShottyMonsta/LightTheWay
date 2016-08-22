using UnityEngine;
using System.Collections;

public class SlunkIdle : State<GameObject> {

    private static SlunkIdle instance = null;

    public static SlunkIdle GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SlunkIdle>();

                if (instance == null)
                {
                    GameObject container = new GameObject("SlunkIdle singleton");
                    instance = container.AddComponent<SlunkIdle>();
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
        SlunkSM machine = entity.GetComponent<SlunkSM>();
        Rigidbody2D body = entity.GetComponent<Rigidbody2D>();

        if (machine.Standing)
        {
            // check if floor below slunk
            RaycastHit2D floorDetect = Physics2D.Raycast(
                body.position + (machine.velocity.normalized),
                Vector2.down, 2f);
            // draw for bebug
            Debug.DrawRay(
                body.position + (machine.velocity.normalized),
                Vector2.down * 2,
                Color.red);

            // turn around if no floow
            if (!floorDetect.collider)
            {
                if (machine.nearEdge)
                    machine.velocity.x *= -1;
                else
                    machine.nearEdge = true;
            }
            else
            {
                machine.nearEdge = false;
                if (floorDetect.collider.tag == "LightArea")
                    machine.velocity.x *= -1;
            }
        }
        AIBehaviours.Turn(entity);
        AIBehaviours.Move(entity);

        Vector2 sightDirection = new Vector2(machine.velocity.x, 0);

        RaycastHit2D lineOfSight = Physics2D.Raycast(
            body.position + machine.velocity.normalized,
            sightDirection, 10f);
        Debug.DrawRay(
            body.position + machine.velocity.normalized,
            sightDirection * 10,
            Color.blue);

        if (lineOfSight.collider)
        {
            if (lineOfSight.collider.gameObject.tag == "PlayerCharacter")
            {
                machine.ChangeState(Chase.GetInstance);
            }

        }

        if (machine.NearLight)
            machine.ChangeState(SlunkFlee.GetInstance);


        

    }
    public override void Exit(GameObject entity)
    {

    }
}
