using UnityEngine;
using System.Collections;

public class E3attackcontrol : MonoBehaviour {


     
        public GameObject attack1collider;
        public GameObject attack2collider;
        //public GameObject attack3collider;
        GameObject Player;
    E1enemymove enemymove;
	// Use this for initialization
	void Start () {
        enemymove = GetComponent<E1enemymove>();
        Player = GameObject.Find("Player");
        attack1collider.SetActive(false);
        attack2collider.SetActive(false);
        //attack3collider.SetActive(false);
	}
	
	// Update is called once per frame
    void attack1active() {
        attack1collider.SetActive(true);

    }
    void attack1false() {
        attack1collider.SetActive(false);
        
    
    }
   

    void attack2active() {
        
        attack2collider.SetActive(true);
        Debug.Log("攻撃中2");
    }

    void atack2false() {
        attack2collider.SetActive(false);
        Debug.Log("攻撃終了2");
    }
   
  void attack3active() {
      //  attack3collider.SetActive(true);
  }

  void attack3false() {
       // attack3collider.SetActive(false);
  }

 
}
