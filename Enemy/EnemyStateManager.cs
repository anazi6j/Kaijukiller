using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
namespace robot
{
    public class EnemyStateManager : MonoBehaviour
    {
        [Multiline]
        public string TODO;

        [Header("Stats")]
        public int health;
        public CharacterStats characterStats;
        [Header("Values")]
        public float delta;
        public float horizontal;
        public float vertical;
        public float distance;
        const float invisibletime = 0.5f;


        [Header("States")]
        public bool isInvisible;
        public bool dontDoAnything;
        public bool canMove;
        public bool isDead;
        public bool isrunning;
        public bool isinvisible;
        public bool hasDestination;
        public bool getparried;
        public bool Playerisin;
        public Vector3 targetDestination;
        public Vector3 dirToTarget;
        public bool rotateToTarget;

        public LayerMask ignoreLayers;

        [Header("Referrence")]
        public Animator anim;
        Enemytarget enTarget;
        AIManager ai;
        EnemyAnimationManager eam;
        public Rigidbody rigid;
        public NavMeshAgent agent;
        RuntoAttack r;
        TimeManager time;
        public Slider healthbar;
        float timer;
        public void Start()
        {
            health = 100;
            anim = GetComponentInChildren<Animator>();
            enTarget = GetComponent<Enemytarget>();
            //enTarget.init(this);
            ai = GetComponent<AIManager>();
            rigid = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            rigid.isKinematic = true;

            time = GameObject.Find("Timemanager").GetComponent<TimeManager>();
            eam = GetComponent<EnemyAnimationManager>();
            //a_manager.Init(null, this);
            healthbar = GetComponentInChildren<Slider>();

            //ignoreLayers = (1 << 14);
            
        }

        public void Update()
        {
            
            canMove = anim.GetBool(StaticStrings.onEmpty);
            distance = ai.distanceFromtarget();
            if (dontDoAnything)
            {
                dontDoAnything = !canMove;
                return;
            }

            if (rotateToTarget)
            {
                LookTowardsTarget();
            }
            if (isInvisible)
            {
                isInvisible = !canMove;
            }
            ControlAnimState();
            
            healthbar.value = characterStats._health / characterStats.hp;
        }

        void ControlAnimState()
        {
            anim.SetBool("isrunning", isrunning);
            getparried = anim.GetBool("getparried");
            Playerisin = agent.isStopped;
            anim.SetBool("InClose", Playerisin);
        }
        
        void LookTowardsTarget()
        {
            Vector3 dir = dirToTarget;
            dir.y = 0;
            if (dir == Vector3.zero)
                dir = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, delta * 5);
        }

        public void WalkToTarget(Transform t, float w)
        {
            hasDestination = false;
            anim.Play("d|Idle");//直接書き込むのではなく、string型変数を用意しておく
            if (distance > 5 && canMove)//一定の距離以上にいてcanmoveが真だったら
            {
                SetPlace(t.position, w);//目的地までwの速さで歩く
            }

            if (distance < 5)
            {
                Debug.Log("stop");
                canMove = false;
                agent.isStopped = true;
            }
        }

        //プレイヤーが立っていた場所に向かい、攻撃する
        public void RunToTargetAndAttack(Vector3 lastpos, float w)
        {
            hasDestination = false;
            isrunning = true;
            if (isrunning)
            {
             
                Debug.Log("Running");
            }
            if (distance > GetComponent<AIManager>().distancelimit && canMove)
            {
                SetPlace(lastpos, w);
            }
            if (distance <GetComponent<AIManager>().distancelimit)//制限距離まで近づいたら攻撃する()
            {
                
                canMove = false;
                agent.isStopped = true;
                anim.Play(ai.em.r_attacks[0].attackanim);
            }
        }

        public void SetPlace(Vector3 d, float movespeed)
        {
            if (!hasDestination)
            {

                hasDestination = true;
                agent.isStopped = false;
                agent.SetDestination(d);
                targetDestination = d;
                agent.speed = movespeed;
            }
        }

        public void CloseAttackaction(int attacknum)
        {
            if (canMove)
            {
                anim.Play(ai.em.c_attacks[attacknum].attackanim);
                agent.isStopped = true;
                rotateToTarget = false;
                anim.SetBool(StaticStrings.onEmpty, false);
                canMove = false;
            }

        }

        public void CloseBehindaction(int attacknum)
        {
            if (canMove)
            {
                anim.Play(ai.em.b_attacks[attacknum].attackanim);
                agent.isStopped = true;
                rotateToTarget = false;
                anim.SetBool(StaticStrings.onEmpty, false);
                canMove = false;
            }

        }


        public void DoDamage(int damage)
        {
            //怯み値が一定以上を超えたら怯みアニメーションをプレイ
            if (characterStats.strength < 0f)
            {
                anim.Play(ai.em.shrink.ShrinkingDamageAnim);
            }

            characterStats._health -= damage;
            
            isinvisible = true;
            StartCoroutine("setvisiblefalse");
        }
       

        public void beparried(float Player_Pstrength)
        {
            canMove = false;
            agent.isStopped = true;
            rotateToTarget = false;
            Player_Pstrength = Mathf.Clamp(Player_Pstrength, 0, 1);
            anim.SetFloat("parried", Player_Pstrength);
            anim.Play(GetComponent<AIManager>().em.gp.parried);//パリィモーションを再生。ブレンドアニメーション
            Debug.Log("再生");
            ai.em.w_a_behind_Hook[0].CloseDamageColliders();//バグ。aniattacknumがoutofrangeになっていた(攻撃コライダーが一つしかないのに二つの要素から選ばせようとしたのが原因）
            ai.em.w_a_close_Hook[0].CloseDamageColliders();//バグ。aniattacknumがoutofrangeになる
            ai.em.w_a_Run_Hook[0].CloseDamageColliders();
            ai.time = 10;
            
            //time.BulletTime();//n秒間時間をスローにする（臨時にここに置いておくが、最終的には演出スクリプトにまとめる）
           
        }
        //パリィダメージを喰らったらこれを発動(PlayerのStateManagerから)
        public void DoParriedDamage(float damage)
        {
            Debug.Log("パリィダメージ");
            anim.Play(ai.em.ParriedDamage.GettingParriedDamage);//これもグローバル変数として編集可能にしておく（スクリプタブルオブジェクトで編集可能にしておく）
            characterStats._health -= damage;

            isinvisible = true;
            StartCoroutine("setvisiblefalse");
        }

        IEnumerator setvisiblefalse()
        {
            yield return new WaitForSeconds(invisibletime);
            isinvisible = false;

            yield return null;
        }

        private void BulletTime()
        {
            Debug.Log("slow");
            float bullettime=0f;
            float limit = 2f;
            //一定時間スローになる
            while (bullettime < limit)
            {
                Time.timeScale = 0.05f;
                
                bullettime += Time.deltaTime;
            }

            Time.timeScale = 1f;
        }

        private void Death()
        {
            anim.Play("d|Death");


            //数秒経ったら勝利のなにかをする
        }

    }


    
}