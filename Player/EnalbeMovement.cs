using UnityEngine;
using System.Collections;

public class EnalbeMovement :StateMachineBehaviour {
    //http://qiita.com/toRisouP/items/b6540b7f514d18b9a426
    // Use this for initialization
   override public void OnStateExit(Animator animator,AnimatorStateInfo stateInfo,int layerIndex) {
        animator.GetComponent<PlayerControl>().canmove = true;
   }
	//このスクリプトはアニメーションステート"stand to rollにくっつける
	// Update is called once per frame
	void Update () {
	
	}
}
