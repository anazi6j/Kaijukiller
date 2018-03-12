using UnityEngine;
using System.Collections;

public class Monster_damageControl : MonoBehaviour {
    public float power = 30;
	// Use this for initialization
	
	// Update is called once per frame
	void OnTriggerEnter(Collider collider) {//受け取るコライダー
    if(collider.gameObject.tag =="Player") {
            
          
                Debug.Log("ダメージを与えた");
                collider.gameObject.SendMessage("Damage", power);//sendmessageはゲームオブジェクトの参照を必要とする。
           
    }
    }
}
