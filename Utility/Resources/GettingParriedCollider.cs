using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace robot
{
    
    public class GettingParriedCollider : MonoBehaviour
    {
        EnemyStateManager esm;
        // Use this for initialization
        void Start()
        {
            esm = transform.root.GetComponent<EnemyStateManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Parry")
            {
                Debug.Log("Parent:" + other.transform.parent.name);
                esm.beparried(other.transform.parent.GetComponent<ParryHook>().parrystrength);
            }
        }
    }
}