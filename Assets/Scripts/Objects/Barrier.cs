using UnityEngine;
using System.Collections;


public class Barrier : MonoBehaviour {

    public int barrierWidth = 0;
    public int barrierHeight = 0;
    public GameObject prefab;

    public bool isActive = true;

    
    private HandelColour colour;

    public float blockScale = 1f;

    public float displacement = 1f;

    private GameObject[] blocks;

    private Vector3 position;
    
	// Use this for initialization

	void Start ()
    {
        displacement *= blockScale;
        colour = GetComponent<HandelColour>();
        prefab.GetComponent<Transform>().localScale = new Vector3(blockScale, blockScale, blockScale);
        prefab.GetComponent<HandelColour>().baseColour = colour.baseColour;

        
        blocks = new GameObject[barrierHeight * barrierWidth];

        position = new Vector3(transform.position.x - ((barrierWidth*prefab.transform.localScale.x) / 2), 
            transform.position.y - ((barrierHeight * prefab.transform.localScale.x) / 2), transform.position.z);

        int counter = 0;

        for (int y = 0; y < barrierHeight; y++)
        {
            for (int x = 0; x < barrierWidth; x++)
            {
                GameObject clone  = Instantiate(prefab, new Vector3(position.x + (x * displacement),position.y + (y * displacement),position.z),Quaternion.identity) as GameObject;
                clone.transform.parent = transform;
                clone.GetComponent<HandelColour>().Locked = colour.Locked;
                blocks[counter] = clone;
                counter++;
            }
        }

        if (!isActive)
            HideBarrier();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HideBarrier()
    {
        for (int x = 0; x < blocks.Length; x++)
        {
            blocks[x].GetComponent<Renderer>().enabled = false;
            blocks[x].GetComponent<Collider2D>().enabled = false;
        }
    }

    public void HideBarrier(ColourLayer.Type hideColour)
    {
        for(int x = 0; x < blocks.Length; x++)
        {
            if(blocks[x].GetComponent<HandelColour>().GetColourType == hideColour)
            {
                blocks[x].GetComponent<Renderer>().enabled = false;
                blocks[x].GetComponent<Collider2D>().enabled = false;
            }            
        }
    }

    public void RevealBarrier()
    {
        for (int x = 0; x < blocks.Length; x++)
        {
            blocks[x].GetComponent<Renderer>().enabled = true;
            blocks[x].GetComponent<Collider2D>().enabled = true;
        }
    }

    public void RevealBarrier(ColourLayer.Type revealColour)
    {
        for (int x = 0; x < blocks.Length; x++)
        {
            if (blocks[x].GetComponent<HandelColour>().GetColourType == revealColour)
            {
                blocks[x].GetComponent<Renderer>().enabled = true;
                blocks[x].GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void ChangeBarrierColour(ColourLayer.Type changeColour)
    {
        for (int x = 0; x < blocks.Length; x++)
        {
            if (colour.Locked)
            {
                blocks[x].GetComponent<HandelColour>().currentColour = changeColour;
            }
            else
            {
                blocks[x].GetComponent<HandelColour>().baseColour = changeColour;
            }
           
        }
    }

    public void OnDrawGizmos()
    {


        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawCube(transform.position - new Vector3(0.1f,0.1f,0), new Vector3(barrierWidth * blockScale, barrierHeight * blockScale, 0));
        //Gizmos.DrawCube(transform.position , new Vector3(barrierWidth * blockScale, barrierHeight * blockScale, 0));
    }
}
