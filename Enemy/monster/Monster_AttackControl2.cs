using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_AttackControl2 : MonoBehaviour
{
    
        public Animator animator;
        int move = Animator.StringToHash("Base Layer.move");
        int Shippo = Animator.StringToHash("Base Layer.しっぽ");
        CharacterController controller;
        AudioSource[] audiosource;
        //**咆哮
        public GameObject pressexplosion;
        public GameObject presszone;
    public GameObject pressattackzone;
        //**火炎放射攻撃
        public GameObject flame;
        public GameObject flamezone;
    //**しっぽ攻撃
    public GameObject taleattack;
        [SerializeField]
        int behabiorno;//idlemoveのアニメーションが終わったら、行動番号を更新する
    Vector3 movedir=Vector3.zero;
    public float movespeed;//突進
    public float timer;
        

        void Start()
        {
        animator = GetComponent<Animator>();
        audiosource = GetComponents<AudioSource>();
        controller = GetComponent<CharacterController>();
        taleattack.SetActive(false);
        pressattackzone.SetActive(false);
        animator.SetBool("moving", true);
        }

      void Update()
    {
        
        AnimatorStateInfo stateinfo = this.animator.GetCurrentAnimatorStateInfo(0);
        if (stateinfo.fullPathHash == move)
        {
            movedir = Vector3.forward;
            movedir = transform.TransformDirection(movedir);
            movedir *= movespeed;
         
            controller.Move(movedir * Time.deltaTime);
            timer += Time.deltaTime;
        }else if(stateinfo.fullPathHash == Shippo)
        {
            movedir *= 0;
        }
        if(timer >1.5f)
        {
            animator.SetBool("moving", false);
            timer = 0;
        }
    }

   

        void Setbehaviorno()
        {
            behabiorno = Random.Range(1, 5);
            animator.SetInteger("behaviorno", behabiorno);
           if(behabiorno == 4)
        {
            Debug.Log("突進が来る");
            animator.SetBool("moving", true);
            
        }
        }

        void Settaleattackactive()
        {
        taleattack.SetActive(true);
        }

        void Settaleattackinactive()
        {
        taleattack.SetActive(false);
        }

    void roar()//咆哮
    {
        audiosource[0].Play();
        GameObject effect = Instantiate(pressexplosion, presszone.transform.position, presszone.transform.rotation) as GameObject;
        Destroy(effect, 4f);
    }
     void setroarattackactive()
    {
        pressattackzone.SetActive(true);

    }
   void setroarattackinactive()
    {
        pressattackzone.SetActive(false);
    }
         

    //突進関連
    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.tag=="Player")
        {
            Debug.Log("突進終了");
            animator.SetBool("moving", false);
        }

    }

    void StartFlamethrower()
        {
            GameObject flamethrower = Instantiate(flame, flamezone.transform.position, flamezone.transform.rotation) as GameObject;
        }
    }
