using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelOpener : MonoBehaviour {
    public GameObject canvas;
    public Animator fader;
    private bool loading = false;
    private string levelName;

    public void OpenLevel(string LevelName)
    {
        fader.SetBool("fadeOut", true);
        this.levelName = LevelName;
        canvas.GetComponent<Canvas>().enabled = false;
    }

    public void Update()
    {
        if (!loading)
        {
            if (fader)
            {
                if (fader.GetCurrentAnimatorStateInfo(0).IsName("Finished"))
                {
                    loading = true;
                    SceneManager.LoadScene(this.levelName);
                }
            }  
        }   
    }
}
