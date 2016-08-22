using UnityEngine;
using System.Collections;

public class Miasma : MonoBehaviour {


    public float width = 3f;
    public float height = 3f;
    private Material mat;
    private float matScrollSpeed = 0.1f;

    private BoxCollider2D areaOfAffect;
	// Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        areaOfAffect = GetComponent<BoxCollider2D>();
        areaOfAffect.isTrigger = true;

      //  areaOfAffect.size = new Vector2(width, height);
	}

    void Update ()
    {
        if (mat)
        {
            Vector2 newMatOffset = mat.mainTextureOffset;
            newMatOffset.x += Time.deltaTime * matScrollSpeed;
            mat.mainTextureOffset = newMatOffset;
        }
    }
	
	// Update is called once per frame
	private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(width, height, 0));
    }
}
