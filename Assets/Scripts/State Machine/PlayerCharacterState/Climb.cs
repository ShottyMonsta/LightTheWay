using UnityEngine;
using System.Collections;

public class Climb : State<GameObject>
{

    // Use this for initialization
    private static Climb instance = null;

    private static float buffer = 0.6f;
    private static float displace = 1.5f;
    private float offDirection;
    private Vector2 exitPoint;

    private LayerMask mask;

    public static Climb GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Climb>();

                if (instance == null)
                {
                    GameObject container = new GameObject("climb singleton");
                    instance = container.AddComponent<Climb>();
                }
            }
            return instance;
        }
    }

    // Update is called once per frame
    public override void Enter(GameObject entity)
    {
        mask = ~(1 << LayerMask.NameToLayer("LightSource") | 1 << LayerMask.NameToLayer("LightArea"));
        entity.GetComponent<Rigidbody2D>().gravityScale = 0;
        //entity.GetComponent<Rigidbody2D>().isKinematic = true;
    }
    public override void Excute(GameObject entity)
    {
        PlayerChSM player = entity.GetComponent<PlayerChSM>();
        Rigidbody2D body = entity.GetComponent<Rigidbody2D>();

        if (player.velocity.x >= 0)
        {

            RaycastHit2D hit = Physics2D.Raycast(player.EndLadder + Vector2.right * displace, Vector2.right, displace, mask);



            if (hit.collider)
            {
                if (hit.collider.GetComponent<HandelColour>())
                {
                    if (hit.collider.tag != "PlayerCharacter"  &&
                    hit.transform.GetComponent<HandelColour>().GetColourType != entity.GetComponent<HandelColour>().GetColourType)
                    {
                        offDirection = -1;
                        Debug.DrawRay(player.EndLadder + Vector2.left, Vector2.left * displace, Color.red);
                        exitPoint = player.EndLadder + (Vector2.left * displace);
                    }
                    else
                    {
                        offDirection = 1;
                        Debug.DrawRay(player.EndLadder + Vector2.right, Vector2.right * displace, Color.red);
                        exitPoint = player.EndLadder + (Vector2.right * displace);
                    }
                }
                else
                {
                    offDirection = -1;
                    Debug.DrawRay(player.EndLadder + Vector2.left, Vector2.left * displace, Color.red);
                    exitPoint = player.EndLadder + (Vector2.left * displace);
                }

            }
            else
            {
                offDirection = 1;
                Debug.DrawRay(player.EndLadder + Vector2.right, Vector2.right * displace, Color.red);
                exitPoint = player.EndLadder + (Vector2.right * displace);
            }
        }
        else
        {

            RaycastHit2D hit = Physics2D.Raycast(player.EndLadder + Vector2.left * displace, Vector2.right, displace, mask);

            if (hit.collider)
            {
                if (hit.collider.GetComponent<HandelColour>())
                {

                    if (hit.collider.tag != "PlayerCharacter"  &&
                    hit.transform.GetComponent<HandelColour>().GetColourType != entity.GetComponent<HandelColour>().GetColourType)
                    {
                        offDirection = -1;
                        Debug.DrawRay(player.EndLadder + Vector2.right, Vector2.right * displace, Color.red);
                        exitPoint = player.EndLadder + (Vector2.right * displace);
                    }
                    else
                    {
                        offDirection = 1;
                        Debug.DrawRay(player.EndLadder + Vector2.left, Vector2.left * displace, Color.red);
                        exitPoint = player.EndLadder + (Vector2.left * displace);
                    }
                }
                else
                {
                    offDirection = -1;
                    Debug.DrawRay(player.EndLadder + Vector2.right, Vector2.right * displace, Color.red);
                    exitPoint = player.EndLadder + (Vector2.right * displace);
                }

            }
            else
            {
                offDirection = 1;
                Debug.DrawRay(player.EndLadder + Vector2.left, Vector2.left * displace, Color.red);
                exitPoint = player.EndLadder + (Vector2.left * displace);
            }
        }



        if (body.position.y >= player.EndLadder.y - buffer && body.position.y <= player.EndLadder.y + buffer)
        {

            player.velocity.x *= offDirection;

            body.position = exitPoint;
        }
        else
        {
            body.position = Vector2.Lerp(player.StartLadder, player.EndLadder, player.StateTime / player.GetClimbSpeed);
        }

        if (!player.ClimbLadder)
        {
            player.ChangeState(Idle.GetInstance);
        }
    }

    public override void Exit(GameObject entity)
    {
        entity.GetComponent<Rigidbody2D>().gravityScale = entity.GetComponent<StateMachine>().GravityScale;
        //AIBehaviours.Move(entity);
        //entity.GetComponent<Rigidbody2D>().isKinematic = false;
    }


}
