using UnityEngine;
using System.Collections;

public class ResetData : MonoBehaviour {
    public void resetData(int resetLevel)
    {
        PlayerPrefs.SetInt("HighestLevel", resetLevel);
        for(int x = 1; x<=6; x++)
        {
            PlayerPrefs.SetInt("HighestScore" + x.ToString(), 0);
        }
    }
}
