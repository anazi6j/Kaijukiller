using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Flamethrower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("炎hit");
        if (other.tag == "Player")
        {
            
            other.SendMessage("Damage", 40);
            other.SendMessage("damage");
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
