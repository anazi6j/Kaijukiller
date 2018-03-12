using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Attack_waza : MonoBehaviour {
    C2EnemyStats stats;//本体の攻撃力をベースに攻撃力を決定す
   
	// Use this for initialization
	void Start () {
        stats = GameObject.Find("monster2").GetComponent<C2EnemyStats>();
       
	}
	
	void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            if (gameObject.name == "しっぽ攻撃")
            {
                Debug.Log("しっぽHit");
                player.SendMessage("Damage", stats.attackpower + 20f);
                player.SendMessage("damage");
               
            }else if(gameObject.name == "Explosionzone")
            {
                Debug.Log("SoundHit");
                player.SendMessage("Damage", stats.attackpower + 30f);
                player.SendMessage("damage");
            }
           
        }
    }
}
