using UnityEngine;
using System.Collections;


public class ConeLight : MonoBehaviour {



    // if the cone light is used for mixed light
    public bool isMixedLight;
    // used to tell if been hit by spot light
    public bool hitOtherLight;
    // hold the spotlight colour
    public ColourLayer.Type otherLightColour;

    // the width of the beam
    public float width = 2f;
    // the direction of the beam
    public float angle = 0f;
    // how many rays used to generate mesh
    public int segments = 2;

    private HandleLight lightColour;

    // the layers the rays will collide with
    public LayerMask layerMask;

    // distance between each new ray cast
    private float displacement;

    private float maxCastDistance = 30;

    // distnace rays travel
    private float castDistance;

    private float checkDistance;

    private ConeLight parentLight;

    public bool On = true;


    private MeshFilter meshFilter;

    private float gizmoCastDistance = 30f;

    void Start()
    {
        castDistance = maxCastDistance;

        if (!On)
            TurnLightOff();

        lightColour = gameObject.GetComponent<HandleLight>();
        // if used for mixed light apply parents prameaters
        if (isMixedLight)
        {
            parentLight = transform.parent.gameObject.GetComponent<ConeLight>();
            angle = parentLight.angle;
            width = parentLight.width;
            segments = parentLight.segments;
        }
        // create the first mesh

        angle -= width/2;

        gizmoCastDistance = 0;

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = CreateMesh();
        

        
    }

    public bool TurnLightOff()
    {
        On = false;
        castDistance = 0.5f;
        return On;
    }

    public bool TurnLightOn()
    {
        On = true;
        castDistance = maxCastDistance;
        return On;
    }
    // creates a cone mesh cast out from a single point
    Mesh CreateMesh()
    {

        Mesh mesh = new Mesh();
        // store vertices used to genreate mesh
        Vector3[] newVertices = new Vector3[segments + 1];
        // stores uv to apply texture for mesh
        Vector2[] newUV = new Vector2[newVertices.Length];

        //calculate mesh angle degree between each raycast 
        displacement = width / segments;

        // set first vertice. all rays cast out from this point
        newVertices[0] = transform.position;

        // reset as no interaction with spot light 
        hitOtherLight = false;

        // set layerMask according to light type
        if(isMixedLight)
        {
            layerMask = lightColour.LightMask();
        }
        else
        {
            layerMask = (1 << LayerMask.NameToLayer("LightSource")) | lightColour.LightMask();
        }

        // calculate verties of mesh using ray cast
        for(int x = 0; x < newVertices.Length -1; x++)
        {
            Vector2 direction = ColourLayer.RotateVec(Vector2.down, angle + (displacement * x));

            RaycastHit2D hit  = Physics2D.Raycast(newVertices[0], direction, castDistance, layerMask);

            Vector3 vert;

            if (hit)
            {
                checkDistance = hit.distance;
                vert = new Vector3(hit.point.x, hit.point.y,transform.position.z);
                if (hit.collider.GetComponent<SlunkSM>())
                    hit.collider.GetComponent<SlunkSM>().Dead = true;
            }
            else
            {
                checkDistance = castDistance;
                vert = newVertices[0] + (new Vector3(direction.x, direction.y, 0) * castDistance);
            }

            RaycastHit2D[] checkHits = Physics2D.RaycastAll(newVertices[0], direction, checkDistance);

            foreach (RaycastHit2D check in checkHits)
            {



                if (isMixedLight)
                {
                    // interactions with objects
                    if (check.collider.gameObject.GetComponent<HandelColour>())
                    {
                        check.collider.gameObject.GetComponent<HandelColour>().HitByMixLight(lightColour.GetColourType());
                    }
                }
                else
                {
                    // interactiong with light sources
                    if (check.collider.gameObject.layer == LayerMask.NameToLayer("LightSource"))
                    {
                        hitOtherLight = true;
                        otherLightColour = check.collider.gameObject.GetComponent<HandleLight>().currentColour;

                    }
                    // interactions with objects
                    if (check.collider.gameObject.GetComponent<HandelColour>())
                    {
                        check.collider.gameObject.GetComponent<HandelColour>().HitByLight(lightColour.GetColourType());
                    }
                }

            }

            newVertices[x + 1] = vert;
            

        }

        // to realine with game world, dont know why its works but does. do not remove!
        for (int x = 0; x < newVertices.Length; x++)
        {
            Vector3 newVec = transform.InverseTransformPoint(newVertices[x]);
            newVertices[x] = new Vector3(newVec.x,newVec.y, transform.position.z);
        }

        // genrate UVs for textures
        for (int x = 0; x < newUV.Length; x++)
        {
            if((x%2) == 0)
            {
                newUV[x] = new Vector2(0, 0);
            }
            else
            {
                newUV[x] = new Vector2(1, 1);
            }
        }

        // generate traingle to build mesh
        int[] newTriangles = new int[3 * (newVertices.Length - 2)];

        int b = 1;
        int c = 2;
        for(int x = 0; x < newTriangles.Length; x += 3)
        {
            newTriangles[x] = 0;
            newTriangles[x + 1] = b;
            newTriangles[x + 2] = c;
            b++;
            c++;
        }


        //pass generated rays to mesh
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;

        
        //optermise mesh
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();

        return mesh;
    }

    // Update is called once per frame
    void Update () {
        // clear old mesh and create new according to surrowndings;
         

        meshFilter.sharedMesh.Clear();
        meshFilter.sharedMesh = CreateMesh();

        if (isMixedLight)
        {
            angle = parentLight.angle;
            castDistance = parentLight.castDistance;

            GetComponentInChildren<CircleCollider2D>().transform.localPosition = meshFilter.sharedMesh.vertices[meshFilter.sharedMesh.vertexCount/2];
            GetComponentInChildren<CircleCollider2D>().radius = Vector3.Distance(meshFilter.sharedMesh.vertices[1], 
                meshFilter.sharedMesh.vertices[meshFilter.sharedMesh.vertexCount - 1]) / 1.5f;
        }

    }

    public void OnDrawGizmos()
    {
        if (!isMixedLight)
        {
            float rayAngle = angle - (width / 2);
            Gizmos.DrawRay(transform.position, ColourLayer.RotateVec(Vector2.down, rayAngle) * gizmoCastDistance);
            Gizmos.DrawRay(transform.position, ColourLayer.RotateVec(Vector2.down, rayAngle + width) * gizmoCastDistance);
        }
    }




    }
