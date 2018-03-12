using UnityEngine;
using System.Collections;

public class enemyattackzone2 : MonoBehaviour {
  
    public float attackpower;
	// Use this for initialization
	void Start () {
       
	}

    
    void OnTriggerEnter(Collider hitobject)
    {
       
        if (hitobject.gameObject.name == "robot 2")

            hitobject.gameObject.SendMessage("Damage", 20);
        
        Debug.Log("プレス攻撃");
    }

}
