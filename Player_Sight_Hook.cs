using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace robot
{
    public class Player_Sight_Hook : MonoBehaviour
    {
        StateManager s;
        public Transform target;
        public void Init(StateManager state)
        {
            s = state;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Enemy")
            {
                s.target = other.transform;
            }
        }


    }
}