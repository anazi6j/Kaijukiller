using UnityEngine;
using System.Collections;

public class Player_attackzone : MonoBehaviour {
    float power;
    C1Stats stats;
    GD_animationcontroller gd;
    public Collider thiscollider;
    void Start()
    {
        stats = transform.root.GetComponent<C1Stats>();
        power = stats.attackpower;
        gd = GameObject.Find("ギガントデューク").GetComponent<GD_animationcontroller>();
    }
    void OnTriggerEnter(Collider hit) {


        if (hit.gameObject.tag == "Enemy")
        {
            
            hit.gameObject.SendMessage("Damage", (power + gd.addattack));
          
            Debug.Log("ダメージを与えた" + (power + gd.addattack));
        }
    }
}
