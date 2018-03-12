using UnityEngine;
using System.Collections;

public class U5AnimationControl : MonoBehaviour
{
    movemotor movemotor;
    public Animator robot;
    AudioSource playersound;
    C1Stats c1stats;
    public AudioClip punchsound;
    public AudioClip footsound;
    public AudioClip deathsound;
    public float horizontal;
    public float vertical;
    public float move;
    public bool canattack;
    public float attacklimit = 2;
    public float attacktimer;
    public bool canguard;
    


    // Use this for initialization
    void Start()
    {
         movemotor =GetComponent<movemotor>();
        robot = GetComponent<Animator>();
        playersound = GetComponent<AudioSource>();
        c1stats = GetComponent<C1Stats>();
        canattack = true;
        canguard = true;
    }

    // Update is called once per frame
    void Update()
    {
        attacktimer += Time.deltaTime;
        MovingControl();
        AttackanimationControl();
        guardanimationcontrol();
        if (c1stats.health <= 0)
        {
            death();
        }
    }

    void MovingControl()
    {

        move = movemotor.moveparamater;
        robot.SetFloat("move", move);//移動量に応じてアニメーションが歩行か走行に変化する
    }

    void AttackanimationControl()
    {
        if(Input.GetButtonDown("DS4_R1"))
            if (canattack == true)
            {
                if (attacktimer > attacklimit)
                {
                    robot.SetBool("Punch", true);

                   
                    attacktimer = 0;
                }

            }
            else
            {
                robot.SetBool("Punch", false);//もしR2ボタンを押すと
            //U2.CanMove = true;               //攻撃可能であった場合
        }
    }

    
    void guardanimationcontrol()
    {
        if(Input.GetButton("DS4_L1"))//L1ボタンを押している間
         
            {//防御可能な場合
                robot.SetBool("guard", true);
            c1stats.guarding = true;
            }else {
                robot.SetBool("guard", false);
            c1stats.guarding = false;
            }
           
    }

   
    void death()
    {
        
        robot.SetTrigger("Death");
    }

  

    void Endpunch() {
        robot.SetBool("Punch", false);
        Debug.Log("パンチおわり");
    }

    void psound()
    {
        playersound.clip = punchsound;
        playersound.Play();
    }

    void walksound()
    {
       playersound.clip = footsound;
        playersound.Play();
    }
    void losesound()
    {
        playersound.clip = deathsound;
        playersound.Play();
    }
}
