using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake_Trigger_test : MonoBehaviour {

    public Camera_Shake_test test;

	// Use this for initialization
	void Start () {
      
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
             StartCoroutine(test.Shake(0.15f, 1f));
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(test.Shake(0.15f, 1f));
        }
    }
   
        
}
