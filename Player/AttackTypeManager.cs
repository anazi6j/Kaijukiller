using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
    
    public class AttackTypeManager : MonoBehaviour
    {
        Animator anim;
        public string[] targetAnim;
        public float animnum;
        const int Default=0;
        // Use this for initialization
        void Start()
        {
            anim = GetComponentInChildren<Animator>();
            animnum = Default;
        }

        // Update is called once per frame
        void Update()
        {
         //十字キーを押したらanimnumが増えたり減ったりする
         //
        }
    }
}