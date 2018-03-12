using UnityEngine;
using System.Collections;

public class Enemy_Bress : MonoBehaviour {
    public float attack = 30;
    
    void Update()
    {
        transform.position += transform.forward*30 * Time.deltaTime;
        Destroy(gameObject, 3f);
    }
	void OnTriggerEnter(Collider hit)
    {
      
        if(hit.gameObject.tag == "Player")
        {
            hit.gameObject.SendMessage("Damage", attack);
            Destroy(this.gameObject);
        }
    }
	
}
