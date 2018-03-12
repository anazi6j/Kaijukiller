using UnityEngine;
using System.Collections;

public class Club_AI : MonoBehaviour {
    public Animator animator;
    
    CharacterController controll;
    public Transform player;
    //**突進関連**//
    public bool move;//突進時に利用
    public bool rotate;//突進時、プレイヤーの方向に振り向く
    public float rotatespeed;//振り向くスピード
   //**突進関連終わり**//
    public bool idlemove;//スフィアコライダーが入ってない場合に真となる
    public float speed;
    public float angle;
    public float range;
	// Use this for initialization
	void Start () {
        controll = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool("ColliderIn", false);
	}
	
	// Update is called once per frame
	void Update () {
      
       
        CheckAngleandRange();
        
        
        if (move)
        {
            tacklemove();
        }
        if (rotate)
        {
            rotation();
        }
	}

    void CheckAngleandRange()
    {
        //http://gamesonytablet.blogspot.jp/2013/02/unityai6.html
        angle = Vector3.Angle(transform.position - player.position, transform.forward);
         animator.SetFloat("Angle", angle);
        range = Vector3.Distance(transform.position, player.position);
        animator.SetFloat("Range", range);
        //SphereColliderの半径30=Distanceの9.69。
    }

    public void Active()
    {
        
        animator.SetBool("ColliderIn", true);
    }

    public void NonActive()
    {
        
        animator.SetBool("ColliderIn", false);
    }

    //ここから突進関連の関数
    void rotation()
    {
        Quaternion LookatPlayer = Quaternion.LookRotation(player.position - transform.position);
        LookatPlayer.x = 0;
        LookatPlayer.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, LookatPlayer, Time.deltaTime*rotatespeed);
    }
    void tacklemove()
       
    {
        Vector3 movedirection;
        movedirection = Vector3.forward;
        movedirection = transform.TransformDirection(movedirection);
        movedirection *= speed;
        movedirection.y -= 9.8f * Time.deltaTime;

        controll.Move(movedirection * Time.deltaTime);

    }

    void Setrotateactive()
    {
        rotate = true;
    }

    void Setrotateinactive()
    {
        rotate = false;
    }

    void Setmoveacytive()
    {
        move = true;
    }

    void Setmoveinactive()
    {
        move = false;
    }
    
    }

