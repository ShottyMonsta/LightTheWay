using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeleportManager : MonoBehaviour {

    private static TeleportManager instance = null;

    private List<Teleporter> teleporters = new List<Teleporter>();

    public static TeleportManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TeleportManager>();

                if (instance == null)
                {
                    GameObject container = new GameObject("TeleManager singleton");
                    instance = container.AddComponent<TeleportManager>();
                }
            }
            return instance;
        }
    }


    public void Register(Teleporter newTeleporter)
    {
        teleporters.Add(newTeleporter);
        newTeleporter.setID(teleporters.Count - 1);
    }

    public Teleporter FindActiveTeleporter(Teleporter currentTeleporter)
    {
        Teleporter activeTeleporter = null;
        foreach(Teleporter t in teleporters)
        {
            if(t.GetID != currentTeleporter.GetID)
            {
                if(currentTeleporter.GetColour == t.GetColour)
                {
                    activeTeleporter = t;
                }
            }
        }

        return activeTeleporter;
    }
	
	
}
