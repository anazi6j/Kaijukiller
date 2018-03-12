using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class C2scenemanager : MonoBehaviour {
    public Text GameText;
    public bool clear;
    public bool lose;
  
	// Use this for initialization
	void Start () {
        clear = false;
        lose = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("mainmenu");
            }
        }

        if (clear)
        {
            if (Input.GetButtonDown("DS4_X"))
            {
                SceneManager.LoadScene("mainmenu");
            }
        }
        if (lose)
        {
            if (Input.GetButtonDown("Submit"))
            {
                SceneManager.GetActiveScene();
            }
        }
	}
    
    public void getmonster()
    {
        SceneManager.LoadScene("RobotAction");
    }

    public void getclub()
    {
        SceneManager.LoadScene("fourclub3");
    }

    void Lose()
    {
        GameText.text = "○ボタンでリトライ。Xボタンでメニュー画面";
        lose = true;
        clear = true;
    }

    void Win()
    {
        GameText.text = "you win";
        clear = true;
    }
}
