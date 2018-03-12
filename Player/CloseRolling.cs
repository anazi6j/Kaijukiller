using UnityEngine;
using System.Collections;

public class CloseRolling : StateMachineBehaviour {

    // Use this for initialization
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAnimation pl = animator.GetComponent<PlayerAnimation>();

        pl.rolling = false;
        pl.hasDirection = false;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
