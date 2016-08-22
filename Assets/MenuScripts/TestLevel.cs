using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevel : MonoBehaviour {

    // Use this for initialization
    public int currentLevel = 0;
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            SceneManager.LoadScene("World1");
        }
    }
}
