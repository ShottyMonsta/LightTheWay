using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public int spawnCount = 1;

    public GameObject prefab;

    public float spawnDelay = 2f;

    private float elapsedTime;

    private GameObject[] prefabs;
	// Use this for initialization
	void Start () {

        prefabs = new GameObject[spawnCount];

        for(int x = 0; x < spawnCount; x++)
        {
            GameObject clone = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
            clone.GetComponent<StateMachine>().Spawned = true;
            prefabs[x] = clone;
            
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        

        foreach(GameObject clone in prefabs)
        {
            if(clone.GetComponent<SlunkSM>())
            {
                SlunkSM machine = clone.GetComponent<SlunkSM>();
                if(machine.DeathAniFinished)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > spawnDelay)
                    {
                        machine.Spawn(transform.position);
                        elapsedTime = 0f;
                    }
                }
            }
        }
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
