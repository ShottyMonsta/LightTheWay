using UnityEngine;
using System.Collections;


public class State<Entity> : MonoBehaviour {


    // Update is called once per frame
    public virtual void Enter(Entity entity) { }
    public virtual void Excute(Entity entity) { }
    public virtual void Exit(Entity entity) { }
}
