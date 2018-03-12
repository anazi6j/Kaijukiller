using UnityEngine;
using System.Collections;

public class Monster_animationcontroll : MonoBehaviour {
    ParticleSystem particle;
   public Animator animator;
	// Use this for initialization
	void Start () {
        particle = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	

    void  Setbressfalse() {

        animator.SetBool("Longattack", false);
    }

    void SetTalseattackfalse() {
        Debug.Log("アニメーション２終了");
        animator.SetBool("nearattack", false); //一度falseになるが、そのあとすぐにtrueが返ってくる

    }
}
