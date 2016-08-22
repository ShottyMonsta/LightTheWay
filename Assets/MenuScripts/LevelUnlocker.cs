using UnityEngine;
using UnityEngine.UI;


public class LevelUnlocker : MonoBehaviour {

    public int levelCurrent = 1;
	// Use this for initialization
	void Start () {
        SetLevelsLock();
        int totalStars = 0;
        for (int x = 1; x<=6; x++)
        {
            if(PlayerPrefs.GetInt("HighestScore" + x.ToString()) == 0)
            {
                GameObject.Find("Star" + x.ToString() + "3").SetActive(false);
                GameObject.Find("Star" + x.ToString() + "2").SetActive(false);
                GameObject.Find("Star" + x.ToString() + "1").SetActive(false);
            }
            else if (PlayerPrefs.GetInt("HighestScore" + x.ToString()) == 1)
            {
                GameObject.Find("Star" + x.ToString() + "3").SetActive(false);
                GameObject.Find("Star" + x.ToString() + "2").SetActive(false);
            }
            else if (PlayerPrefs.GetInt("HighestScore" + x.ToString()) == 2)
            {
                GameObject.Find("Star" + x.ToString() + "3").SetActive(false);
            }
            totalStars += PlayerPrefs.GetInt("HighestScore" + x.ToString());
        }
        GameObject.Find("StarTotal").GetComponent<Text>().text = totalStars.ToString() + "/18";

        GameObject panelThing = GameObject.Find("Panel");

        panelThing.transform.position = new Vector2(panelThing.transform.position.x + (PlayerPrefs.GetInt("HighestLevel") - 1) * -5, panelThing.transform.position.y);

    }

    void SetLevelsLock()
    {
        for (int x = 1; x <= 6; x++)
        {
            if (PlayerPrefs.GetInt("HighestLevel") == x)
            {
                GameObject SetTop = GameObject.Find("LockedLevel" + x);
                if(SetTop) SetTop.SetActive(false);

                GameObject SetComplete = GameObject.Find("Level" + x + "Complete");
                if(SetComplete) SetComplete.SetActive(false);
            }
            if (PlayerPrefs.GetInt("HighestLevel") > x)
            {
                GameObject SetLock = GameObject.Find("LockedLevel" + x);
                if(SetLock) SetLock.SetActive(false);
            }
            if (PlayerPrefs.GetInt("HighestLevel") < x)
            {
                GameObject SetLight = GameObject.Find("Level" + x + "Complete");
                if(SetLight) SetLight.SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        levelCurrent = PlayerPrefs.GetInt("HighestLevel");
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerPrefs.SetInt("HighestLevel", 5);
            SetLevelsLock();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerPrefs.SetInt("HighestLevel", 2);
            SetLevelsLock();
        }
    }
}
