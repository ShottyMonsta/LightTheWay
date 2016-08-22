using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {
    public Animator fader;
    private bool loading = false;
    private string levelName;

	// Use this for initialization
    public void clickChange(string sceneName)
    {
        fader.SetBool("fadeOut", true);
        this.levelName = sceneName;
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
