using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal : MonoBehaviour {

    // Use this for initialization

    public int currentLevel = 0;

    public GameObject GameComplete;

    public GameObject Canvas;

    private GameObject LoseText;
    public float time1;
    public float time2;

    public float timeT;

    private bool levelOver;

    private GameObject star;

    private GameObject timer;

    public Sprite doorOpen;

    void Start () {
        LoseText = GameObject.Find("You Died");
        timeT = 0;
        levelOver = false;
        GameObject.Find("[Time1]").GetComponent<Text>().text = time1.ToString() + ".00";
        GameObject.Find("[Time2]").GetComponent<Text>().text = time2.ToString() + ".00";
        timer = GameObject.Find("TimerClock");
    }

    void Update()
    {
        timer.GetComponent<Text>().text = ((Mathf.RoundToInt(timeT * 100)) / 100.0f).ToString();
        if (!levelOver)
        {
            timeT += Time.deltaTime;
        }

        if (PlayerPrefs.GetInt("HighestLevel") == currentLevel)
        {
            PlayerPrefs.SetInt("HighestLevel", currentLevel + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "PlayerCharacter")
        {
            //Time.timeScale = 0.01f;
            if (PlayerPrefs.GetInt("HighestLevel") == currentLevel)
            {
                PlayerPrefs.SetInt("HighestLevel", currentLevel + 1);
            }
            if (PlayerPrefs.GetInt("HighestScore" + currentLevel.ToString()) == 0)
            {
                PlayerPrefs.SetInt("HighestScore" + currentLevel.ToString(), 1);
            }
            if (timeT < time1 && PlayerPrefs.GetInt("HighestScore" + currentLevel.ToString()) < 2 && !levelOver)
            {
                PlayerPrefs.SetInt("HighestScore" + currentLevel.ToString(), 2);
            }
            if (timeT <= time2 && !levelOver)
            {
                PlayerPrefs.SetInt("HighestScore" + currentLevel.ToString(), 3);
            }

            GameComplete.transform.position = Canvas.transform.position;
            LoseText.SetActive(false);
            GameObject.Find("[Time]").GetComponent<Text>().text = (Mathf.Round(timeT * 100) / 100).ToString();
            if (timeT > time2 && !levelOver)
            {
                GameObject.Find("Star3Image").SetActive(false);
            }
            if (timeT > time1 && !levelOver)
            {
                GameObject.Find("Star2Image").SetActive(false);
            }
            GetComponent<SpriteRenderer>().sprite = doorOpen;
            levelOver = true;
        }
    }
}
