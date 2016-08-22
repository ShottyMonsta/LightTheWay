using UnityEngine;
using System.Collections;

public class HandleLight : MonoBehaviour {

    public ColourLayer.Type baseColour = ColourLayer.Type.RED;
    public ColourLayer.Type currentColour;
    private ColourLayer.Type prevColour;
    private SpriteRenderer sprite;
    private MeshRenderer rend;
    public int layer;

    public LayerMask defaultMask;

    private HandleLight childColour;
    private ConeLight coneLight;
    private BeamLight beamLight;
	// Use this for initialization
    void Awake()
    {

        sprite = GetComponent<SpriteRenderer>();
        rend = GetComponent<MeshRenderer>();
        coneLight = GetComponent<ConeLight>();
        beamLight = GetComponent<BeamLight>();
        if(transform.childCount > 0)
        {
            childColour = transform.GetChild(0).gameObject.GetComponent<HandleLight>();
        }
        
        if(childColour)
        {
            childColour.baseColour = baseColour;
        }
    }
	void Start () {
        currentColour = baseColour;
        prevColour = baseColour;
        SetColour();
        
	}

    public LayerMask LightMask()
    {
        LayerMask mask = defaultMask;
        switch (currentColour)
        {
            case ColourLayer.Type.BLUE:
                mask = (1 << LayerMask.NameToLayer("Orange")) | 
                    (1 << LayerMask.NameToLayer("Lime"))|
                    (1 << LayerMask.NameToLayer("Turquoise"))|
                    (1 << LayerMask.NameToLayer("Pink")) | defaultMask;
                break;
            case ColourLayer.Type.RED:
                mask = (1 << LayerMask.NameToLayer("Purple"))|
                    (1 << LayerMask.NameToLayer("Lime")) |
                    (1 << LayerMask.NameToLayer("SkyBlue")) |
                    (1 << LayerMask.NameToLayer("Turquoise")) | defaultMask;
                break;
            case ColourLayer.Type.GREEN:
                mask = (1 << LayerMask.NameToLayer("Purple")) |
                    (1 << LayerMask.NameToLayer("Pink")) |
                    (1 << LayerMask.NameToLayer("SkyBlue")) |
                    (1 << LayerMask.NameToLayer("Orange")) | defaultMask;
                break;
            case ColourLayer.Type.YELLOW:
                mask = (1 << LayerMask.NameToLayer("Purple")) |
                    (1 << LayerMask.NameToLayer("Pink")) |
                    (1 << LayerMask.NameToLayer("SkyBlue")) |
                    (1 << LayerMask.NameToLayer("Magenta")) |
                    (1 << LayerMask.NameToLayer("Cyan")) |
                    (1 << LayerMask.NameToLayer("Turquoise")) | defaultMask;
                break;
            case ColourLayer.Type.MAGENTA:
                mask = (1 << LayerMask.NameToLayer("Lime")) |
                    (1 << LayerMask.NameToLayer("Turquoise")) |
                    (1 << LayerMask.NameToLayer("SkyBlue")) |
                    (1 << LayerMask.NameToLayer("Yellow")) |
                    (1 << LayerMask.NameToLayer("Cyan")) |
                    (1 << LayerMask.NameToLayer("Orange")) | defaultMask;
                break;
            case ColourLayer.Type.CYAN:
                mask = (1 << LayerMask.NameToLayer("Purple")) |
                    (1 << LayerMask.NameToLayer("Pink")) |
                    (1 << LayerMask.NameToLayer("Lime")) |
                    (1 << LayerMask.NameToLayer("Magenta")) |
                    (1 << LayerMask.NameToLayer("Yellow")) |
                    (1 << LayerMask.NameToLayer("Orange")) | defaultMask;
                break;
            case ColourLayer.Type.ORANGE:
                mask = (1 << LayerMask.NameToLayer("Purple")) |
                    (1 << LayerMask.NameToLayer("Pink")) |
                    (1 << LayerMask.NameToLayer("SkyBlue")) |
                    (1 << LayerMask.NameToLayer("Magenta")) |
                    (1 << LayerMask.NameToLayer("Cyan")) |
                    (1 << LayerMask.NameToLayer("Green")) |
                    (1 << LayerMask.NameToLayer("Blue")) |
                    (1 << LayerMask.NameToLayer("Lime")) |
                    (1 << LayerMask.NameToLayer("Turquoise")) | defaultMask;
                break;
            case ColourLayer.Type.PINK:
                mask = (1 << LayerMask.NameToLayer("Lime")) |
                    (1 << LayerMask.NameToLayer("Turquoise")) |
                    (1 << LayerMask.NameToLayer("SkyBlue")) |
                    (1 << LayerMask.NameToLayer("Yellow")) |
                    (1 << LayerMask.NameToLayer("Cyan")) |
                    (1 << LayerMask.NameToLayer("Purple")) |
                    (1 << LayerMask.NameToLayer("Blue")) |
                    (1 << LayerMask.NameToLayer("Green")) |
                    (1 << LayerMask.NameToLayer("Orange")) | defaultMask;
                break;
            case ColourLayer.Type.TURQUOISE:
                mask = (1 << LayerMask.NameToLayer("Purple")) |
                    (1 << LayerMask.NameToLayer("Pink")) |
                    (1 << LayerMask.NameToLayer("Lime")) |
                    (1 << LayerMask.NameToLayer("Magenta")) |
                    (1 << LayerMask.NameToLayer("Yellow")) |
                    (1 << LayerMask.NameToLayer("Blue")) |
                    (1 << LayerMask.NameToLayer("Red")) |
                    (1 << LayerMask.NameToLayer("SkyBlue")) |
                    (1 << LayerMask.NameToLayer("Orange")) | defaultMask;
                break;
            case ColourLayer.Type.SKYBLUE:
                mask = (1 << LayerMask.NameToLayer("Purple")) |
                    (1 << LayerMask.NameToLayer("Pink")) |
                    (1 << LayerMask.NameToLayer("Lime")) |
                    (1 << LayerMask.NameToLayer("Magenta")) |
                    (1 << LayerMask.NameToLayer("Yellow")) |
                    (1 << LayerMask.NameToLayer("Blue")) |
                    (1 << LayerMask.NameToLayer("Red")) |
                    (1 << LayerMask.NameToLayer("Turquoise")) |
                    (1 << LayerMask.NameToLayer("Orange")) | defaultMask;
                break;
            case ColourLayer.Type.LIME:
                mask = (1 << LayerMask.NameToLayer("Purple")) |
                    (1 << LayerMask.NameToLayer("Pink")) |
                    (1 << LayerMask.NameToLayer("SkyBlue")) |
                    (1 << LayerMask.NameToLayer("Magenta")) |
                    (1 << LayerMask.NameToLayer("Cyan")) |
                    (1 << LayerMask.NameToLayer("Green")) |
                    (1 << LayerMask.NameToLayer("Blue")) |
                    (1 << LayerMask.NameToLayer("Orange")) |
                    (1 << LayerMask.NameToLayer("Turquoise")) | defaultMask;
                break;
            case ColourLayer.Type.PURPLE:
                mask = (1 << LayerMask.NameToLayer("Lime")) |
                    (1 << LayerMask.NameToLayer("Turquoise")) |
                    (1 << LayerMask.NameToLayer("SkyBlue")) |
                    (1 << LayerMask.NameToLayer("Yellow")) |
                    (1 << LayerMask.NameToLayer("Cyan")) |
                    (1 << LayerMask.NameToLayer("Pink")) |
                    (1 << LayerMask.NameToLayer("Blue")) |
                    (1 << LayerMask.NameToLayer("Green")) |
                    (1 << LayerMask.NameToLayer("Orange")) | defaultMask;
                break;
        }

        return mask;
    }

    void SetColour()
    {
        switch (currentColour)
        {
            case ColourLayer.Type.BLACK:
                if (sprite)
                    sprite.color = UnityEngine.Color.black;
                if (rend)
                    rend.material.SetColor("_TintColor", Color.black);
                layer = ColourLayer.BlackLayer;
                break;
            case ColourLayer.Type.BLUE:
                if (sprite)
                {
                    sprite.color = UnityEngine.Color.blue;
                }
                if (rend)
                {
                    rend.material.SetColor("_TintColor", Color.blue);
                }
                layer = ColourLayer.BlueLayer;
                break;
            case ColourLayer.Type.CYAN:
                if (sprite)
                    sprite.color = UnityEngine.Color.cyan;
                if (rend)
                    rend.material.SetColor("_TintColor", Color.cyan);
                layer = ColourLayer.CyanLayer;
                break;
            case ColourLayer.Type.GREEN:
                if (sprite)
                    sprite.color = UnityEngine.Color.green;
                if (rend)
                    rend.material.SetColor("_TintColor", Color.green);
                layer = ColourLayer.GreenLayer;
                break;
            case ColourLayer.Type.MAGENTA:
                if (sprite)
                    sprite.color = UnityEngine.Color.magenta;
                if (rend)
                    rend.material.SetColor("_TintColor", Color.magenta);
                layer = ColourLayer.MagentaLayer;
                break;
            case ColourLayer.Type.RED:
                if (sprite)
                    sprite.color = UnityEngine.Color.red;
                if (rend)
                    rend.material.SetColor("_TintColor", Color.red);
                layer = ColourLayer.RedLayer;
                break;
            case ColourLayer.Type.WHITE:
                if (sprite)
                    sprite.color = UnityEngine.Color.white;
                if (rend)
                    rend.material.SetColor("_TintColor", Color.white);
                layer = ColourLayer.WhiteLayer;
                break;
            case ColourLayer.Type.YELLOW:
                if (sprite)
                    sprite.color = UnityEngine.Color.yellow;
                if (rend)
                    rend.material.SetColor("_TintColor", Color.yellow);
                layer = ColourLayer.YellowLayer;
                break;
            case ColourLayer.Type.PINK:
                if (sprite)
                    sprite.color = new Color(1f, 0.4f, 1f, 1f);
                if (rend)
                    rend.material.SetColor("_TintColor", new Color(1f, 0.4f, 1f, 1f));
                layer = ColourLayer.PinkLayer;
                break;
            case ColourLayer.Type.ORANGE:
                if (sprite)
                    sprite.color = new Color(1f, 0.647f, 0f, 1f);
                if (rend)
                    rend.material.SetColor("_TintColor", new Color(0.8f, 0.3f, 0f, 1f));
                layer = ColourLayer.OrangeLayer;
                break;
            case ColourLayer.Type.PURPLE:
                if (sprite)
                    sprite.color = new Color(0.627f, 0.125f, 0.941f, 1f);
                if (rend)
                    rend.material.SetColor("_TintColor", new Color(0.3f, 0.0f, 1f, 1f));
                layer = ColourLayer.PurpleLayer;
                break;
            case ColourLayer.Type.SKYBLUE:
                if (sprite)
                    sprite.color = new Color(0.529f, 0.808f, 0.980f, 1f);
                if (rend)
                    rend.material.SetColor("_TintColor", new Color(0.3f, 0.6f, 0.75f, 1f));
                layer = ColourLayer.SkyblueLayer;
                break;
            case ColourLayer.Type.TURQUOISE:
                if (sprite)
                    sprite.color = new Color(0.250f, 0.878f, 0.815f, 1f);
                if (rend)
                    rend.material.SetColor("_TintColor", new Color(0.08f, 0.22f, 0.26f, 1f));
                layer = ColourLayer.TurquoiseLayer;
                break;
            case ColourLayer.Type.LIME:
                if (sprite)
                    sprite.color = new Color(0.196f, 0.804f, 0.196f, 1f);
                if (rend)
                    rend.material.SetColor("_TintColor", new Color(0.196f, 0.804f, 0.196f, 1f));
                layer = ColourLayer.LimeLayer;
                break;
        }
        
    }

    // Update is called once per frame
    void Update () {

        if(coneLight)
        {
            if(coneLight.hitOtherLight)
            {
                childColour.currentColour = ColourLayer.BlendColour(coneLight.otherLightColour, currentColour);
            }
            else if(childColour)
            {
                childColour.currentColour = currentColour;
            }
        }
        if(beamLight)
        {
            if (beamLight.hitOtherLight)
            {
                childColour.currentColour = ColourLayer.BlendColour(beamLight.otherLightColour, currentColour);
            }
            else if(childColour)
            {
                childColour.currentColour = currentColour;
            }
            
        }

        if (prevColour != currentColour)
        {
            prevColour = currentColour;
            SetColour();
        }
    }
    public ColourLayer.Type GetColourType()
    {
        return currentColour;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (baseColour != ColourLayer.Type.BLACK)
        {
            if(col.gameObject.GetComponent<HandleLight>())
            {
                baseColour = currentColour;
                currentColour = ColourLayer.BlendColour(col.gameObject.GetComponent<HandleLight>().GetColourType(), baseColour);
            }           
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        //currentColour = baseColour;
    }
}
