using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot {
    public class Motiontest : MonoBehaviour {

        public Animator test;
        public float animblendvalue;

        private void Start()
        {
            test = GetComponent<Animator>();
        }
        private void OnTriggerEnter(Collider other)
        {
            animblendvalue = AnimationStaticFunction.SetGettingParriedvalue(0.5f);

            test.SetFloat("color", animblendvalue);

            test.Play("TestAnim");

            Debug.Log("当たった");

        }
    }
}