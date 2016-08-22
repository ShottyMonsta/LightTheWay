using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeamLight : MonoBehaviour {

    // store the vertices used to generate mesh
    private Vector3[] newVertices;
    // store the UV used to to apply textures to mesh
    private Vector2[] newUV;
    // store the index of vertices used to generate trigangles for mesh
    private int[] newTriangles;
    
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

    // distance ray travel
    private float castDistance;
    // distance cover by light
    private float checkDistance;

    private BeamLight parentLight;

    public bool On = true;

    private MeshFilter meshFilter;

    private bool drawGizmo = true;

    // Use this for initialization
    void Start() {

        lightColour = gameObject.GetComponent<HandleLight>();

        castDistance = maxCastDistance;

        drawGizmo = false;

        if (!On)
            TurnLightOff();

        // if used for mixed light apply parents prameaters
        if (isMixedLight)
        {
            parentLight = transform.parent.gameObject.GetComponent<BeamLight>();
            angle = parentLight.angle;
            width = parentLight.width;
            segments = parentLight.segments;
        }
        // create the first mesh
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


    // generates a mesh for the rectangle mesh
    Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        newVertices = new Vector3[segments * 2];
        newUV = new Vector2[newVertices.Length];
        // reset as no interaction with spotligh
        hitOtherLight = false;

        //calculate space between rays
        displacement = width / segments;

        // calulate how far each ray needs to be displace in each axis for rotation
        Vector2 displaceVec = ColourLayer.RotateVec(Vector2.right, angle);
        displaceVec.Normalize();

        // set the start position of the ray
        Vector3 position = new Vector3(transform.position.x - ((width/2) * displaceVec.x), transform.position.y - ((width / 2) * displaceVec.y));

        // set layerMask according to light type
        if (isMixedLight)
        {
            layerMask = gameObject.GetComponent<HandleLight>().LightMask();
        }
        else
        {
            layerMask = (1 << LayerMask.NameToLayer("LightSource")) | gameObject.GetComponent<HandleLight>().LightMask();
        }

        // calulate vertices of mesh using raycasts
        for (int x = 0; x < newVertices.Length; x += 2)
        {
            
            newVertices[x] = new Vector3(position.x + ((displaceVec.x * displacement)* (x/2)), position.y + ((displaceVec.y * displacement)*(x/2)),transform.position.z);

            Vector3 vert;

            Vector2 direction = ColourLayer.RotateVec(Vector2.down, angle);

            RaycastHit2D hit = Physics2D.Raycast(newVertices[x], direction, castDistance, layerMask);
            //check if the ray cast hit anything
            if(hit)
            {
                checkDistance = hit.distance;
                vert = new Vector3(hit.point.x, hit.point.y, transform.position.z);
                if (hit.collider.GetComponent<SlunkSM>())
                    hit.collider.GetComponent<SlunkSM>().Dead = true;
            }
            else
            {
                checkDistance = castDistance;
                vert = newVertices[x] + new Vector3(direction.x,direction.y,0) * castDistance;
            }

            RaycastHit2D[] checkHits = Physics2D.RaycastAll(newVertices[x], direction, checkDistance);

            foreach (RaycastHit2D check in checkHits)
            {
                if (isMixedLight)
                {
                    // interactiong with objects
                    if (check.collider.gameObject.GetComponent<HandelColour>())
                    {
                        check.collider.gameObject.GetComponent<HandelColour>().HitByMixLight(lightColour.GetColourType());
                    }
                }
                else
                {
                    // interactiong with lightSources
                    if (check.collider.gameObject.layer == LayerMask.NameToLayer("LightSource"))
                    {
                        hitOtherLight = true;
                        otherLightColour = check.collider.gameObject.GetComponent<HandleLight>().currentColour;
                    }
                    // interaction with objects
                    if (check.collider.gameObject.GetComponent<HandelColour>())
                    {
                        check.collider.gameObject.GetComponent<HandelColour>().HitByLight(lightColour.GetColourType());
                    }

                }
            }


            newVertices[x + 1] = vert;           
        }



        // aline the vertices to the world space // DONT KNOW WHY THEY ARENT ALREADY BUT THEYRE NOT X/
        for (int x =0; x < newVertices.Length; x++)
        {
            Vector3 newVec = transform.InverseTransformPoint(newVertices[x]);
            newVertices[x] = newVec;
        }
        // calculate uv for textures
        for (int x = 0; x < newUV.Length; x++)
        {
            if ((x % 2) == 0)
            {
                newUV[x] = new Vector2(0, 0);
            }
            else
            {
                newUV[x] = new Vector2(1, 1);
            }
        }
        // generate tirangles for mesh
        newTriangles = new int[3 * (newVertices.Length - 2)];
        int a = 0;
        int b = 1;
        int c = 2;
        for (int x = 0; x < newTriangles.Length; x += 3)
        {
            newTriangles[x] = a;
            newTriangles[x + 1] = b;
            newTriangles[x + 2] = c;
            a++;
            b++;
            c++;

        }
        // pass generated arrays to mesh
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;


        // optermise mesh
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();



        return mesh;
    }




    // Update is called once per frame
    void Update () {
        // clear old mesh and create new according to surrowndings 
        meshFilter.sharedMesh.Clear();
        meshFilter.sharedMesh = CreateMesh();

        if (isMixedLight)
        {
            angle = parentLight.angle;
            castDistance = parentLight.castDistance;
            GetComponentInChildren<CircleCollider2D>().transform.localPosition = meshFilter.sharedMesh.vertices[(meshFilter.sharedMesh.vertexCount - 1 )/ 2];
            GetComponentInChildren<CircleCollider2D>().radius = Vector3.Distance(meshFilter.sharedMesh.vertices[1],
                meshFilter.sharedMesh.vertices[meshFilter.sharedMesh.vertexCount - 1]) * 2f;
        }
        
    }

    public void OnDrawGizmos()
    {
        if (drawGizmo)
        {
            if(isMixedLight)
            {
                width = transform.parent.gameObject.GetComponent<BeamLight>().width;
            }
            float rayAngle = angle;

            Vector2 displaceVec = ColourLayer.RotateVec(Vector2.right, angle);
            displaceVec.Normalize();
            Gizmos.DrawRay(transform.position + new Vector3(displaceVec.x * (width / 2), displaceVec.y * (width / 2), 0), ColourLayer.RotateVec(Vector2.down, rayAngle) * 10);
            Gizmos.DrawRay(transform.position - new Vector3(displaceVec.x * (width / 2), displaceVec.y * (width / 2), 0), ColourLayer.RotateVec(Vector2.down, rayAngle) * 10);
        }
    }


}
