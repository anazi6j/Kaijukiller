using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace robot
{
    public class ButtonManager : MonoBehaviour
    {
        public string TITLE;
        public void ReturntoTitle()
        {
            SceneManager.LoadScene(TITLE);
        }
       
    }
}
