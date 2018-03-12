using UnityEngine;
using System.Collections;

public class Monster_AttackControl : MonoBehaviour {
    public Animator animator;
    AudioSource[] audiosource;
    //**プレス攻撃
    public GameObject pressexplosion;
    public GameObject presszone;
    //**火炎放射攻撃
    public GameObject flame;
    public GameObject flamezone;
    [SerializeField]
    int behabiorno;//idlemoveのアニメーションが終わったら、行動番号を更新する
                   //突進

	void Start () {
        animator = GetComponent<Animator>();
        audiosource = GetComponents<AudioSource>();
        animator.SetBool("moving", false);

    }
	
	void Setbehaviorno()
    {
        behabiorno = Random.Range(1, 5);
       animator.SetInteger("behaviorno", behabiorno);
       
    }

  void hogehoge()
    {

    }

    void roar()//咆哮
    {
        audiosource[0].Play();
        GameObject effect = Instantiate(pressexplosion, presszone.transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 4f);
    }

  
   

    void StartFlamethrower()
    {
        GameObject flamethrower = Instantiate(flame, flamezone.transform.position, Quaternion.identity) as GameObject;
    }
}
