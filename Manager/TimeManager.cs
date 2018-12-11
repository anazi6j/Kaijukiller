using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {
    public float slowfactor = 0.5f;
    public float slowdownLength = 5f;
    public GameObject text;

    public int pausenum;
    const float GAMEISACTIVE = 1f;
    const float GAMEISNOTACTIVE = 0f;
    public bool pause;
    public bool resume;

    private void Start()
    {
        text.SetActive(false);
        Time.timeScale = GAMEISACTIVE;
    }

    private void Update()
    {


        TimeManagemant();

        pausemanagement();


    }

    
    void pausemanagement()
    {

        if (pause)
        {
            if (!text.activeSelf)
            {
                text.SetActive(true);
            }else if(text.activeSelf){
                text.SetActive(false);
            }
            
        }
        if (text.activeSelf)
        {
            if (resume)
            {
                SceneManager.LoadScene("KAIJUKILLER_TITLE");
            }
        }
       
    }
    
    public void TimeManagemant()
    {
        
        
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;//unscaledDeltatimeについて調べる
        
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

    }
    
     

    public void BulletTime()
    {

        Time.timeScale = slowfactor;


    }
}


        
    




   


