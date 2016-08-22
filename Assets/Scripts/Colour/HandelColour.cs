using UnityEngine;
using System.Collections;

public class HandelColour : MonoBehaviour {


    // the objects start colour and the colour it will return to
    public ColourLayer.Type baseColour = ColourLayer.Type.BLACK;
    // the colour is displaying with interactions
    public ColourLayer.Type currentColour = ColourLayer.Type.BLACK;
    // the colour it previosly was
    private ColourLayer.Type prevColour = ColourLayer.Type.BLACK;
    // sprite attached to the object
    private SpriteRenderer sprite;
    //used for object light interaction
    private bool hitByLight;
    // the colour of the light the object is interacting with
    private ColourLayer.Type lightColour;
    //used for intercation with mixed light 
    private bool hitByMixLight;
    // colour of the mixedlight
    private ColourLayer.Type mixLightColour;
    // if the object is hit by a spot light
    private bool hitBySpotlight = false;

    public bool Locked = false;

    private bool inMiasma = false;

    public GameObject lockSymbol;

    public Vector2 lockOffSet;

    public float holdColourTime = 2f;

    private float elapsedTime;

    public bool RetainColour = false;

    // Use this for initialization
    void Awake()
    {
        //get the sprite attach to object
        sprite = GetComponent<SpriteRenderer>();
        //set the colour of the object
        currentColour = baseColour;
        prevColour = currentColour;
        SetColourAndLayer();
    }

    private void Start()
    {
        if(Locked)
        {
            if (lockSymbol)
            {
                GameObject lockObject = Instantiate(lockSymbol, new Vector3(transform.position.x + lockOffSet.x, transform.position.y + lockOffSet.y, -1), Quaternion.identity) as GameObject;
                lockObject.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(RetainColour)
        {
            elapsedTime += Time.deltaTime;
        }
        

        // check if there any light interactions
        // if none reset colour to base colour
        if (currentColour != ColourLayer.Type.BLACK)
        {
            if (!Locked)
            {
                if (hitByLight)
                {
                    // if hit by cone or beam light

                    currentColour = ColourLayer.BlendColour(lightColour, baseColour);
                }
                else if (hitByMixLight)
                {
                    //ResetColour();
                    // if hit by a mix of cone/beam and spotlight
                    currentColour = ColourLayer.BlendColour(mixLightColour, baseColour);
                }
                else if (hitBySpotlight && !inMiasma)
                {
                    // if hit by spot light

                    currentColour = ColourLayer.BlendColour(lightColour, baseColour);
                }
                else
                {
                    if(RetainColour)
                    {
                        elapsedTime += Time.deltaTime;
                        if (elapsedTime > holdColourTime)
                            ResetColour();
                    }
                    else
                        ResetColour();
                }
            }
        }

        //reset hitBy bools  
        hitByLight = false;
        hitByMixLight = false;
        // change the colour if need  current colour has changed
        if (prevColour != currentColour)
        {
            if (RetainColour)
                elapsedTime = 0;
            prevColour = currentColour;
            SetColourAndLayer();
        }



    }
    // set the colour of sprite and layer of object based on current colour
    void SetColourAndLayer()
    {
        if (sprite)
        {
            switch (currentColour)
            {
                case ColourLayer.Type.BLACK:
                    sprite.color = UnityEngine.Color.black;
                    gameObject.layer = ColourLayer.BlackLayer;
                    break;
                case ColourLayer.Type.BLUE:
                    sprite.color = UnityEngine.Color.blue;
                    gameObject.layer = ColourLayer.BlueLayer;
                    break;
                case ColourLayer.Type.CYAN:
                    sprite.color = UnityEngine.Color.cyan;
                    gameObject.layer = ColourLayer.CyanLayer;
                    break;
                case ColourLayer.Type.GREEN:
                    sprite.color = UnityEngine.Color.green;
                    gameObject.layer = ColourLayer.GreenLayer;
                    break;
                case ColourLayer.Type.MAGENTA:
                    sprite.color = UnityEngine.Color.magenta;
                    gameObject.layer = ColourLayer.MagentaLayer;
                    break;
                case ColourLayer.Type.RED:
                    sprite.color = UnityEngine.Color.red;
                    gameObject.layer = ColourLayer.RedLayer;
                    break;
                case ColourLayer.Type.WHITE:
                    sprite.color = UnityEngine.Color.white;
                    gameObject.layer = ColourLayer.WhiteLayer;
                    break;
                case ColourLayer.Type.YELLOW:
                    sprite.color = UnityEngine.Color.yellow;
                    gameObject.layer = ColourLayer.YellowLayer;
                    break;
                case ColourLayer.Type.PINK:
                    sprite.color = new Color(1f, 0.75f, 1f, 1f);
                    gameObject.layer = ColourLayer.PinkLayer;
                    break;
                case ColourLayer.Type.ORANGE:
                    sprite.color = new Color(1f, 0.647f, 0f, 1f);
                    gameObject.layer = ColourLayer.OrangeLayer;
                    break;
                case ColourLayer.Type.PURPLE:
                    sprite.color = new Color(0.627f, 0.125f, 0.941f, 1f);
                    gameObject.layer = ColourLayer.PurpleLayer;
                    break;
                case ColourLayer.Type.SKYBLUE:
                    sprite.color = new Color(0.529f, 0.808f, 0.980f, 1f);
                    gameObject.layer = ColourLayer.SkyblueLayer;
                    break;
                case ColourLayer.Type.TURQUOISE:
                    //sprite.color = new Color(0.250f, 0.878f, 0.815f, 1f);
                    sprite.color = new Color(0.18f, 0.46f, 0.54f, 1f);
                    gameObject.layer = ColourLayer.TurquoiseLayer;
                    break;
                case ColourLayer.Type.LIME:
                    sprite.color = new Color(0.36f, 1f, 0.5f, 1f);
                    gameObject.layer = ColourLayer.LimeLayer;
                    break;
            }

            if (transform.childCount > 0)
            {
                foreach (SpriteRenderer childSprite in GetComponentsInChildren<SpriteRenderer>())
                {
                    childSprite.color = sprite.color;
                }
            }
        }
    }



    // get the current colour
    public ColourLayer.Type GetColourType
    {
        get { return currentColour; }
    }

    // when hit by a spotlight
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Miasma>())
        {
            inMiasma = true;
        }

        if (currentColour != ColourLayer.Type.BLACK)
        {
            if (col.gameObject.GetComponent<HandleLight>())
            {
                hitBySpotlight = true;
                lightColour = col.gameObject.GetComponent<HandleLight>().GetColourType();
            }
        }
    }
    // when a spot light leaves the object
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Miasma>())
        {
            inMiasma = false;
        }
        if (col.gameObject.GetComponent<HandleLight>())
        {
            hitBySpotlight = false;
        }
    }

    // when hit by a cone or beam light
    public void HitByLight(ColourLayer.Type newLightColour)
    {
        hitByLight = true;
        lightColour = newLightColour;
    }
    // when hit by a mix of cone/beam and spotlight
    public void HitByMixLight(ColourLayer.Type newLightColour)
    {
        hitByMixLight = true;
        mixLightColour = newLightColour;
    }


    // reset the current colour to base
    public void ResetColour()
    {
        currentColour = baseColour;
    }
}


