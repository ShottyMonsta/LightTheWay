using UnityEngine;
using System.Collections;

[System.Serializable]
public class AIBehaviours : System.Object {

    

    public static void Turn(GameObject entity)
    {

        // **** keep just in case *****
        //Rigidbody2D entityBody = entity.GetComponent<Rigidbody2D>();

        //Vector2 displace = Vector2.down * 0.6f;



        //RaycastHit2D floorDetect = Physics2D.Raycast(
        //    entityBody.position + displace, Vector2.down, 0.1f);
        //Debug.DrawRay(entityBody.position + displace, Vector2.down * 0.1f, Color.red);

        //if (floorDetect.collider)
        //{  
        //        entity.GetComponent<StateMachine>().velocity.y = 1f;
        //}
        // ****** probaly not needed ******

        bool wallHit = false;

        StateMachine machine = entity.GetComponent<StateMachine>();

        LayerMask mask;

        if (entity.GetComponent<HandelColour>())
        {
            mask = ~(1 << LayerMask.NameToLayer("LightSource") | 1 << LayerMask.NameToLayer("LightArea") | 1 << entity.layer);
        }
        else
            mask = ~(1 << LayerMask.NameToLayer("LightSource") | 1 << LayerMask.NameToLayer("LightArea"));

        Vector3 direction;

        if (machine.velocity.x > 0)
            direction = Vector3.right;
        else
            direction = Vector3.left;

        float height = entity.GetComponent<BoxCollider2D>().size.y;

        float offSet = height / 4;

        Vector3 pos = entity.transform.position + new Vector3(0, -offSet, 0);


        for (int x = 0; x < 3; x++)
        {
            Vector3 rayStart = pos + new Vector3(0, offSet * x, 0) + direction;
            RaycastHit2D wallDetect = Physics2D.Raycast(rayStart, direction, 1f, mask);
            if (wallDetect)
            {
                if (wallDetect.collider.tag != "DynamicObjects")
                {
                    wallHit = true;
                    Debug.DrawRay(rayStart, direction * 1, Color.red);
                }
            }
            else
            {
                Debug.DrawRay(rayStart, direction * 1, Color.green);
                
            }
        }

        

        //if(entity.GetComponent<StateMachine>().Standing)
        //    entity.GetComponent<StateMachine>().velocity.y = 1f;

        float absVelX = System.Math.Abs(entity.GetComponent<Rigidbody2D>().velocity.x);

        bool hitwall = absVelX <= 0.1;

        if (hitwall)
        {
            if (wallHit && machine.Standing)
            {
                machine.velocity.x *= -1;
            }
            else
            {
                machine.velocity.y = 8f;
            }
        }
        else
        {
            machine.velocity.y = 1;
        }

        
    }

    public static void Move(GameObject entity)
    {
        if(entity.GetComponent<StateMachine>().Standing)
            entity.GetComponent<Rigidbody2D>().velocity = entity.GetComponent<StateMachine>().velocity;
    }


}
