using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{   //アニメーションイベントとして呼び出す関数を集めたクラス
    public class EnemyAnimationHook : MonoBehaviour
    {
        AIManager ai;
        EnemyStateManager est;
        
        public GameObject ParriedCol;
    
        public void Init()
        {
            ai = GetComponent<AIManager>();
            est = GetComponent<EnemyStateManager>();
            ParriedCol.SetActive(false);
            
        }


        //コライダーオブジェクトのOn・Off(アニメーションイベントで実行）
        public void Open_Close_DamageColliders()
        {
            ai.em.w_a_close_Hook[0].OpenDamageColliders();
        }
        public void Close_Close_DamageColliders()
        {
            ai.em.w_a_close_Hook[0].CloseDamageColliders();
        }
        

        

        public void Open_Behind_AttackColliders()
        {

            ai.em.w_a_behind_Hook[0].OpenDamageColliders();

        }
        public void Close_Behind_AttackColliders()
        {
            ai.em.w_a_behind_Hook[0].CloseDamageColliders();
        }

        public void OpenRun_AttackColliders()
        {

            ai.em.w_a_Run_Hook[0].OpenDamageColliders();
        
        }

        public void Close_Run_AttackColliders()
        {

            ai.em.w_a_Run_Hook[0].CloseDamageColliders();

        }

        //コライダーオブジェクトのOn・Off


        //アニメーションイベントで呼び出す関数

        public void SetIntervalTime_Close()
        {
            int attackanimno = Random.Range(0, GetComponent<AIManager>().em.c_attacks.Count);
            GetComponent<AIManager>().AddTime_Close(attackanimno);
        }

        public void SetIntervalTime_Behind()
        {
            int attackanimno = Random.Range(0, GetComponent<AIManager>().em.b_attacks.Count);
            GetComponent<AIManager>().AddTime_Behind(attackanimno);
        }

        public void SetintervalTime_Run()
        {
            
            GetComponent<AIManager>().AddTime_Run();
            GetComponent<EnemyStateManager>().isrunning = false;
            GetComponent<AIManager>().lastpos = Vector3.zero;
        }

        public void SetintervalTime_GetParried()
        {
            GetComponent<AIManager>().AddTime_GetParried();
            GetComponent<EnemyStateManager>().isrunning = false;
            GetComponent<AIManager>().lastpos = Vector3.zero;
        }

        public void SetintervalTime_ShrinkingFromDamage()
        {
            GetComponent<AIManager>().AddTime_ShrinkfromDamage();
            GetComponent<EnemyStateManager>().isrunning = false;
            GetComponent<AIManager>().lastpos = Vector3.zero;
        }
        
        
        //パリィされるためのコライダーをonにしたりoffにしたりする

            
        public void SetEnablingtobeparried()
        {
            ParriedCol.SetActive(true);
        }

        public void SetDisablingtobeparried()
        {
            ParriedCol.SetActive(false);
        }
        
        //以上、アニメーションイベントで呼ぶ関数
        
    }
}

   