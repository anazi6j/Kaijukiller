using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
    public class AIManager : MonoBehaviour
    {
        public CloseAttacks[] c_attacks;
        public BehindAttacks[] b_attacks;
        public RuntoAttack r;
        public float fov_angle;//見える範囲（角度）
        public float sight;//視力
        public float distance;//プレイヤーと敵との距離
        public float angle;//プレイヤーと敵との角度
        public float limit;//遠い/近いを区別する距離の境目
        public float walkspeed;
        public float runspeed;
        public float time;//インターバル時間
        public float delta;
        public bool insight;//見えてるかどうか



        public Transform target;//敵の標的、現時点ではプレイヤー
        public Vector3 lastpos;//突進用
        
        
        RaycastHit hit;
        Vector3 dirtotarget;

        EnemyStateManager states;
        EnemyAnimationHook ea_hook;


        public Vector3 SetTransformforRunandAttack()
        {

            Vector3 origin = transform.position;
            origin.y = 0.5f;
            Vector3 dir = dirtotarget;
            dir.y = 0.5f;
           Vector3 p = new Vector3(hit.point.x, 0, hit.point.z);

            if (time < 0 && time > -1f)//-1fをfloat型の変数homingtimeに変更する
            {
                if (Physics.Raycast(origin, dir, out hit, sight, states.ignoreLayers))
                {
                    Debug.DrawRay(origin, dir);
                    Debug.Log(hit.transform.gameObject.name);
                    p = new Vector3(hit.point.x, 0, hit.point.z);
                   

                }
            }

            return p ;
        }

        public float distanceFromtarget()
        {
            if (target == null)
                return 100;

            if (time < 0f)
                return Vector3.Distance(lastpos, transform.position);

            return Vector3.Distance(target.position, transform.position);
        }

        float angleToTarget()
        {
            float a = 180;
            if (target)
            {
                Vector3 d = dirtotarget;
                a = Vector3.Angle(d, transform.forward);
            }

            return a;
        }

        bool CheckInsight()//視界に入っているかどうか
        {

            RaycastHit hit;
            Vector3 origin = transform.position;
            origin.y = 0.5f;
            Vector3 dir = dirtotarget;
            dir.y = 0.5f;
            Debug.DrawRay(origin, dir);
            if (Physics.Raycast(origin, dir, out hit, sight, states.ignoreLayers))
            {
                if (angle > fov_angle)
                    return true;
            }
            return  false;
        }

        void Start()
        {

            time = 5f;
            states = GetComponent<EnemyStateManager>();
            states.Init();
            ea_hook = GetComponent<EnemyAnimationHook>();
            ea_hook.Init();
        }

        void Update()
        {


            states.Tick(delta);
            if (target)
                dirtotarget = target.position - transform.position;

            angle =(time>0)? angleToTarget(): Vector3.Distance(lastpos, transform.position); 
            distance = distanceFromtarget();
            time -= Time.deltaTime;
            delta = Time.deltaTime;
            HandleFarInSight();
            lastpos = SetTransformforRunandAttack();



            switch (aiState)
            {
                case AIstate.farinsight:
                    HandleFarInSight();
                    break;
                case AIstate.closeinsight:
                    HandleCloseinsight();
                    break;
                case AIstate.behindclose:
                    HandleCloseNotinsight();
                    break;
                default:
                    break;


            }
        }

       

        public AIstate aiState;



        public enum AIstate
        {
            farinsight, closeinsight, behindfar, behindclose
            //左から順に、視界には入ってるが距離が遠い場合。
            //一足一挙動の距離の場合
            //視界に入っておらず遠い場合
            //視界に入っておらず近い場合
        }

        void HandleFarInSight()
        {
            //距離が一定以下なら「視界に入っており、近くにいる」状態になる
            if (distance <= limit && insight && !states.isrunning)
            {
                aiState = AIstate.closeinsight;

            }

            insight = CheckInsight();
           
            
            
            if (time > 0 && insight)
            {
                
                states.WalkToTarget(target, walkspeed);
            }
            else if(time<0&&insight)
            {
              
                states.RunToTargetAndAttack(lastpos, runspeed);
            }//インターバル時間がゼロ以上で視界に入っていた場合近寄る
             //そうでない場合突進する（歩く速度よりも速く、制限距離まで近づく。）

        }
       



        void HandleCloseinsight()
        {
            insight = CheckInsight();
            //距離が一定以上で視界に入っていたら、「farinsight」
            if (distance > limit && insight&&time>-5f)
            {
                aiState = AIstate.farinsight;
            }
            if (distance < limit && !insight)//距離が一定以下で視界に入ってない場合
            {
                aiState = AIstate.closeinsight;
            }
            if (insight && time <= 0)
            {
                if (states.canMove)
                {
                    int c_attacknum = Random.Range(0, c_attacks.Length);
                    states.CloseAttackaction(c_attacknum);
                    ea_hook.SetcloseAttacknum(c_attacknum);
                    states.canMove = false;
                }
            }

        }//クールダウン時間がゼロで、視界に入っていたら近接攻撃する

        void HandleCloseNotinsight()
        {
            insight = CheckInsight();

            if (distance < limit && insight && states.canMove)
            {//距離が一定以下で視界に入っており、canmoveがtrueの場合、closeinsightになる
                aiState = AIstate.closeinsight;
            }
            if (!insight && time <= 0)
            {
                if (states.canMove)
                {
                    int b_attacknum = Random.Range(0, b_attacks.Length);
                    states.CloseAttackaction(b_attacknum);
                    ea_hook.SetcloseAttacknum(b_attacknum);
                    states.canMove = false;
                }
            }
        }

       
        public void AddTime_Close(int num)
        {
            time = c_attacks[num].intervaltime;
        }

       
          
        public void AddTime_Run()
        {
            time = r.intervaltime;
        }
    }









    /*       void HandleFarNotInSight()
           {
               //振り向く
           }

           void HandleCloseNotInSight()
           {
               //視界に入っておらず、制限距離以下まで近づいていたら後方に攻撃する
           }*/

   



}
    
  
    

   


    public class Attacks
    {
        public string attackanim;
        public int attackpower;
        public bool canBeparried;
        public float intervaltime;
     public GameObject colliders;
    }

    [System.Serializable]
    public class CloseAttacks:Attacks
    {
        
        private int closeattacknum;
        
    }

    [System.Serializable]
    public class BehindAttacks:Attacks
    {
       
      
       private int behindattacknum;
      
    }

    [System.Serializable]
    public class RuntoAttack:Attacks
    {
       
    }
    
