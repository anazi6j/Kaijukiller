using UnityEngine;
using System.Collections;

public class Enemyattackzone1 : MonoBehaviour {
    
    void OnTriggerEnter(Collider hitobject)
    {
        if(hitobject.gameObject.name== "robot 2")
        hitobject.SendMessage("Damage", 10);
    }


}
