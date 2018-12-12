using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
    public class AIManager : MonoBehaviour
    {
        [Multiline]
        public string TODO;
        public int animattacknum=0;
        /*
       
        public CloseAttacks[]c_attacks;
        public BehindAttacks[] b_attacks;
        public RuntoAttack[] r_attacks;
        public GetParried gp;
       *///EnemyAnimationManagerに移植した変数。念のためここにコメントアウト
        /*
       public  WeaponHook[] w_a_close;
       public  WeaponHook[] w_a_behind;
       public WeaponHook[] w_a_Run;
        *///
        public float distance;//プレイヤーと敵との距離
        
        public float angle;//プレイヤーと敵との角度
        public float time;//インターバル時間
        public float delta;
        public bool insight;//見えてるかどうか
        public Transform target;//敵の標的、現時点ではプレイヤー
        public Vector3 lastpos;//突進用

        [Header("inspectorで編集可能な項目")]
        public float distancelimit;//遠い/近いを区別する距離の境目。inspectorで編集する項目
        public float walkspeed;//歩行スピード。inspectorで変数
        public float runspeed;
        public float fov_angle;//見える範囲（角度）
        public float sight;//視力
        public float homingurgency;//ホーミングの強さ。

        [Header("スクリプタブルオブジェクト")]
        public EnemyParamTable epm;







        RaycastHit hit;
        Vector3 dirtotarget;

        EnemyStateManager states;
        EnemyAnimationHook ea_hook;
        public EnemyAnimationManager em;
        
        //突進位置を決定する
        public Vector3 SetTransformforRunandAttack()
        {
            

            Vector3 origin = transform.position;
            origin.y = 0.5f;
            Vector3 dir = dirtotarget;
            dir.y = 0.5f;
            Vector3 p = new Vector3(hit.point.x, 0, hit.point.z);

            if (time < 0 && time > -1f)//-1fをfloat型の変数homingtimeに変更する
            {
                if (Physics.Raycast(origin, dir, out hit, sight,states.ignoreLayers))
                {
                    Debug.DrawRay(origin, dir,Color.green);
                    Debug.Log(hit.transform.name);
                    p = new Vector3(hit.point.x, 0, hit.point.z);//自分自身を衝突判定に入れないように、ignoreLayersでプレイヤー以外を無視する


                }
            }

            return p;
        }
        //プレイヤーと敵の距離を測る
        public float distanceFromtarget()
        {
            if (target == null)
                return 100;

            
            if (time < 0f)
                return Vector3.Distance(lastpos, transform.position);//プレイヤーが最後に立っていた場所と、
            //このスクリプトをつけたオブジェクトの距離を返す

            return Vector3.Distance(target.position, transform.position);
        }

        //プレイヤーと敵との角度を計算する
        float angleToTarget()
        {
            float a = 180;
            if (target)
            {
                Vector3 DirectionBetweenTargetandEnemy = dirtotarget;
                a = Vector3.Angle(DirectionBetweenTargetandEnemy, transform.forward);
            }

            return a;
        }
        //視界に入っているかどうか
        bool CheckInsight()
        {

            RaycastHit hit;
            Vector3 origin = transform.position;
            origin.y = 0.5f;
            Vector3 dir = dirtotarget;
            dir.y = 0.5f;
            //Debug.DrawRay(origin, dir, Color.green);
            if (Physics.Raycast(origin, dir, out hit, sight, states.ignoreLayers))
            {
                if (angle < fov_angle)
                    return true;
            }
            return false;
        }
        

        //EnemyAnimationManagerに移植したソースコード。念のためコメントアウトにとどめる
        /* 
         void InitParam()
         {
             for (int i = 0; i < epm.closeAttacks.Count; i++)
             {
                 c_attacks[i].attackanim = null;
                 c_attacks[i].attackpower = 0;
                 c_attacks[i].attackstrength = 0;
                 c_attacks[i].intervaltime = 0;
                 //c_attacks[i].colliders = null;
                 //ゲームオブジェクトのプレハブを取得すると、なぜかvector3.distanceで返したプレイヤーと敵との距離の値が不正に書き換えられる
             }



             for (int j = 0; j < epm.behindAttacks.Count; j++)
             {
                 b_attacks[j].attackanim = null;
                 b_attacks[j].attackpower = 0;
                 b_attacks[j].attackstrength = 0;
                 b_attacks[j].intervaltime = 0;
                 //b_attacks[j].colliders = null;
             }



             for (int k = 0; k < epm.runtoAttacks.Count; k++)
             {
                 r_attacks[k].attackanim = null;
                 r_attacks[k].attackpower = 0;
                 r_attacks[k].attackstrength = 0;
                 r_attacks[k].intervaltime = 0;
                 //r_attacks[k].colliders = null;
             }


             gp.parried = null;
         }

         //参照されたEnemyAttackParamManagerの情報を、こちらに書き込む

         void InitAttackparams(EnemyParamTable param)
         {

             for (int i = 0; i < param.closeAttacks.Count; i++)
             {
                 c_attacks[i].attackanim = param.closeAttacks[i].attackanim;
                 c_attacks[i].attackpower = param.closeAttacks[i].attackpower;
                 c_attacks[i].attackstrength = param.closeAttacks[i].attackstrength;
                 c_attacks[i].intervaltime = param.closeAttacks[i].intervaltime;

                 //c_attacks[i].colliders = param.closeAttacks[i].colliders;
                 //ゲームオブジェクトのプレハブを取得すると、なぜかvector3.distanceで返したプレイヤーと敵との距離の値が不正に書き換えられる
             }



             for(int j =0;j<epm.behindAttacks.Count; j++)
             {
                 b_attacks[j].attackanim = param.behindAttacks[j].attackanim;
                 b_attacks[j].attackpower = param.behindAttacks[j].attackpower;
                 b_attacks[j].attackstrength = param.behindAttacks[j].attackstrength;
                 b_attacks[j].intervaltime = param.behindAttacks[j].intervaltime;
                 //b_attacks[j].colliders = param.behindAttacks[j].colliders;
             }



             for(int k = 0; k < epm.runtoAttacks.Count; k++)
             {
                 r_attacks[k].attackanim = param.runtoAttacks[k].attackanim;
                 r_attacks[k].attackpower = param.runtoAttacks[k].attackpower;
                 r_attacks[k].attackstrength = param.runtoAttacks[k].attackstrength;
                 r_attacks[k].intervaltime = param.runtoAttacks[k].intervaltime;
                 //r_attacks[k].colliders = param.runtoAttack[k].colliders;
             }


             gp.parried = param.getParried.parried;

         }
         */

        /*
      //攻撃モーションに応じて、コライダー、攻撃力、攻撃強度を各"WeaponHook"ゲームオブジェクトにアタッチされた"weaponhook"に格納する
       GameObject[] cattacks = GameObject.FindGameObjectsWithTag("EnemyWeaponHook_Close");
       GameObject[] battacks = GameObject.FindGameObjectsWithTag("EnemyWeaponHook_Behind");

       w_a_close = new WeaponHook[cattacks.Length];
       w_a_behind = new WeaponHook[battacks.Length];

       for(int i = 0; i < cattacks.Length; i++)
       {
           w_a_close[i] = cattacks[i].GetComponent<WeaponHook>();
           w_a_close[i].CopyCattackColliderandAttackPowerandAttackStrength(c_attacks);
           w_a_close[i].CloseDamageColliders();
       }

       for(int j = 0; j < battacks.Length; j++)
       {
           w_a_behind[j] = battacks[j].GetComponent<WeaponHook>();
           w_a_behind[j].CopyBattackColliderandAttackPowerandAttackStrength(b_attacks);
           w_a_behind[j].CloseDamageColliders();
       }

       w_a_Run= new WeaponHook[r_attacks.Length];
       GameObject[] rattacks = GameObject.FindGameObjectsWithTag("EnemyWeaponHook_RunToAttack");
           w_a_Run[0] = rattacks[0].GetComponent<WeaponHook>();
           w_a_Run[0].CopyRattackColliderandAttackPowerandAttackStrength(r_attacks);
           w_a_Run[0].CloseDamageColliders();

       */

        void Start()
        {

           
            time = 5f;
            states = GetComponent<EnemyStateManager>();
            //states.Init();
            ea_hook = GetComponent<EnemyAnimationHook>();
            ea_hook.Init();
            /*
            em = GetComponent<EnemyAnimationManager>();
            em.Initalize1st();
            */


            float homingtime = 0f;
            homingurgency = (homingurgency == 0) ? 1 : homingurgency;
            homingtime = -homingurgency;

            
        }
        

       

        void Update()
        {

            //states.Tick(delta);
            if (target)
                dirtotarget = target.position - transform.position;

            angle = (time > 0) ? angleToTarget() : Vector3.Angle(lastpos, transform.position);//インターバル時間が0以上の場合、プレイヤーとの角度を返すが、そうでなければlastpos(突進先）との角度を返す
            distance = distanceFromtarget();//インターバル時間がゼロ以上の場合、プレイヤーとの距離を返すが、そうでない場合突進先との距離を返す
            time -= Time.deltaTime;
            delta = Time.deltaTime;
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
            if (distance <= distancelimit && insight && !states.isrunning)
            {
                aiState = AIstate.closeinsight;

            }

            insight = CheckInsight();
            
                if (time > 0&&insight)
                {

                    states.WalkToTarget(target, walkspeed);
                }


            if (time < 0 &&insight)
            {
                
                states.RunToTargetAndAttack(lastpos, runspeed);
            }

            


            //インターバル時間がゼロ以上で視界に入っていた場合近寄る
            //そうでない場合突進する（歩く速度よりも速く、制限距離まで近づく。）

        }




        void HandleCloseinsight()
        {
            insight = CheckInsight();
            //距離が一定以上で視界に入っていたら、「farinsight」
            if (distance > distancelimit && insight && time > -5f)
            {
                aiState = AIstate.farinsight;
            }
            if (distance < distancelimit && !insight)//距離が一定以下で視界に入ってない場合
            {
                aiState = AIstate.behindclose;
            }
            if (time < 0)//行動制限が解除されたら
            {
                if (states.canMove&& insight)
                {
                    int c_attacknum = Random.Range(0, em.c_attacks.Count);//呼び出す近接攻撃を決定する
                    animattacknum = c_attacknum;//animattacknumはc_attacknumを代入した数になる
                    states.CloseAttackaction(c_attacknum);//c_attacksに格納されたアニメーションの中からランダムに選び近接攻撃を仕掛ける
                    
                 
                    states.canMove = false;
                }
            }

        }//クールダウン時間がゼロで、視界に入っていたら近接攻撃する

        
        //バグ：この状態で遠くに離れると、敵が動かなくなりそこでインターバルが0になるたびにbehindattackをし続けるので、振り向かせる必要がある
        void HandleCloseNotinsight()
        {
            insight = CheckInsight();

            if (distance < distancelimit && insight && states.canMove)
            {//距離が一定以下で視界に入っており、canmoveがtrueの場合、closeinsightになる
                aiState = AIstate.closeinsight;
            }
            //距離が離れていたら、「遠くにいる」
            if (distance > distancelimit)
            {
                aiState = AIstate.farinsight;
            }
            //視界に入っておらず、行動制限時間が０以下の場合
            if (!insight && time <= 0)
            {
                if (states.canMove)
                {
                    int b_attacknum = Random.Range(0, em.b_attacks.Count);//背後への攻撃のアニメーションを決定する
                    animattacknum = b_attacknum;
                    states.CloseBehindaction(b_attacknum);//背後への攻撃アニメーションを行う
                    
                    states.canMove = false;
                }
            }
        }
        

       



        //eahookの各SetInterval関数から引き数を受け取り、それに応じて各アニメーションに格納されたインターバル時間を制限時間に代入する（インターバル時間を決定する）
        public void AddTime_Close(int num)
        {
            time = em.c_attacks[num].intervaltime;
        }

        public void AddTime_Behind(int num)
        {
            time = em.b_attacks[num].intervaltime;
        }

        public void AddTime_Run()
        {
            time = em.r_attacks[0].intervaltime;
        }

        public void AddTime_GetParried()
        {
            time = em.gp.intervaltime;
        }

        public void AddTime_ShrinkfromDamage()
        {
            time = em.shrink.intervaltime;
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



/*

namespace robot
{

    public class Attacks
    {
        public string attackanim;
        public int attackpower;
        public float attackstrength;//攻撃強度。相手がパリィしてきた際に、数値が上回っていたら削りダメージを与える
        public float intervaltime;
        public GameObject colliders;
       
        

        
    }


    [System.Serializable]
    public class CloseAttacks : Attacks
    {

        private int closeattacknum;
        
        
    }

    [System.Serializable]
    public class BehindAttacks : Attacks
    {


        private int behindattacknum;//攻撃アニメーションをランダムに決定する
        
       
    }

    [System.Serializable]
    public class RuntoAttack : Attacks
    {
        WeaponHook w_a;
        public void Start()
        {

        }
    }

    [System.Serializable]
    public class GetParried
    {
        public string parried;
        public float intervaltime;
    }
    */



