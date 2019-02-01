using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace robot {
    public class debug : MonoBehaviour {

        public Text debugtext;
        public AIManager ai;
        public StateManager state;
        // Use this for initialization
        void Start() {
           
        }

        // Update is called once per frame
        void Update() {
           debugtext.text = state.EnemyInsight.ToString();

        }
    }

}
