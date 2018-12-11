using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace robot
{
    
    public class Parrycollider : MonoBehaviour
    {
        StateManager StateManager;
        private void Start()
        {
            StateManager = GetComponentInParent<StateManager>();

        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(transform.root.name);
            if (other.gameObject.tag == "Parry")
            {
                
                other.gameObject.transform.root.GetComponent<EnemyStateManager>().beparried(GetComponentInParent<ParryHook>().parrystrength * 0.1f);
                
                Debug.Log(other.gameObject.transform.root);
            }
        }

    }
}