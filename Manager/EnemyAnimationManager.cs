using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{ //敵のアニメーションの統率を行う。(スクリプタブルオブジェクトに登録されたアニメーションの情報を読み込む）AIManagerと連動させる
    public class EnemyAnimationManager : MonoBehaviour
    {

        public int animattacknum;
        public List <CloseAttacks> c_attacks=new List<CloseAttacks>();
        public List <BehindAttacks> b_attacks=new List<BehindAttacks>();
        public List<RuntoAttack> r_attacks = new List<RuntoAttack>();
        public GetParried gp;
        public ShrinkDamage shrink;
        public ParriedDamage ParriedDamage;
        public WeaponHook[] w_a_close_Hook;
        public WeaponHook[] w_a_behind_Hook;
        public WeaponHook[] w_a_Run_Hook;
        public GameObject CloseAttackCollider;
        public GameObject BehindAttackCollider;
        public GameObject RuntoAttackCollider;
        

        [Header("スクリプタブルオブジェクト")]
        public EnemyParamTable epm;

        EnemyStateManager states;
        EnemyAnimationHook ea_hook;
       
        // Use this for initialization
        public void Start()
        {
            //InitParam();
            InitAttackparams(epm);
            InitObject();




        }
        

        // Update is called once per frame
        void Tick()
        {

        }

        public void InitParam()
        {
            /*for (int i = 0; i < epm.closeAttacks.Count; i++)
            {
                c_attacks[i].attackanim = null;
                c_attacks[i].attackpower = 0;
                c_attacks[i].attackstrength = 0;
                c_attacks[i].intervaltime = 0;
                //c_attacks[i].colliders = null;
                
            }*/


            /*
            for (int j = 0; j < epm.behindAttacks.Count; j++)
            {

                b_attacks[j].attackanim = null;
                b_attacks[j].attackpower = 0;
                b_attacks[j].attackstrength = 0;
                b_attacks[j].intervaltime = 0;


                //b_attacks[j].colliders = null;
            }
            */


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

        
            

       

       public  void InitAttackparams(EnemyParamTable param)
        {
            /*
            for (int i = 0; i < param.closeAttacks.Count; i++)
            {
                c_attacks[i].attackanim = param.closeAttacks[i].attackanim;
                c_attacks[i].attackpower = param.closeAttacks[i].attackpower;
                c_attacks[i].attackstrength = param.closeAttacks[i].attackstrength;
                c_attacks[i].intervaltime = param.closeAttacks[i].intervaltime;

               //c_attacks[i].colliders = param.closeAttacks[i].colliders;
               
            }
            */
            for(int i=0;i<epm.closeAttacks.Count;i++)
            {
                c_attacks.Add(new CloseAttacks(param.closeAttacks[i].attackanim, param.closeAttacks[i].attackpower,
                    param.closeAttacks[i].attackstrength, param.closeAttacks[i].intervaltime, param.closeAttacks[i].canbeParried,
                    CloseAttackCollider));
            }




            for (int j = 0; j < epm.behindAttacks.Count; j++)
            {

                b_attacks.Add(new BehindAttacks(param.behindAttacks[j].attackanim, param.behindAttacks[j].attackpower,
                     param.behindAttacks[j].attackstrength, param.behindAttacks[j].intervaltime, param.behindAttacks[j].canbeParried,
                     BehindAttackCollider));
                
            }



            for (int k = 0; k < epm.runtoAttacks.Count; k++)
            {

               r_attacks.Add(new RuntoAttack(param.runtoAttacks[k].attackanim, param.runtoAttacks[k].attackpower,
                      param.runtoAttacks[k].attackstrength, param.runtoAttacks[k].intervaltime, param.runtoAttacks[k].canbeParried,
                      RuntoAttackCollider));


            }


            gp.parried = param.getParried.parried;
            gp.intervaltime = param.getParried.intervaltime;
            
           
            
        }



        private void InitObject()
        {
           
            Debug.Log("InitObject");

            //敵の攻撃オブジェクト（攻撃力などをつかさどるweaponhookオブジェクト,その子オブジェクトであるcolliderオブジェクト）を取得
            GameObject[] cattacks = GameObject.FindGameObjectsWithTag("EnemyWeaponHook_Close");
            GameObject[] battacks = GameObject.FindGameObjectsWithTag("EnemyWeaponHook_Behind");
            GameObject[] rattacks = GameObject.FindGameObjectsWithTag("EnemyWeaponHook_RunToAttack");
            Debug.Log("書き込み");

            //各攻撃に対応したWeaponHookオブジェクトのインスタンスを、上記で取得した攻撃オブジェクトの数だけ取得する
            
            w_a_close_Hook = new WeaponHook[cattacks.Length];
            w_a_behind_Hook = new WeaponHook[battacks.Length];
            w_a_Run_Hook = new WeaponHook[r_attacks.Count];

            //EnemyWeaponHook_Closeのタグがついており、かつweaponhookがアタッチされているHookオブジェクトにCloseAttackクラスのメンバをコピーする

            for (int i = 0; i < cattacks.Length; i++)
            {
                w_a_close_Hook[i] = cattacks[i].GetComponent<WeaponHook>();
                w_a_close_Hook[i].CopyCattackColliderandAttackPowerandAttackStrength(c_attacks);
                Debug.Log("コピー");
                w_a_close_Hook[i].CloseDamageColliders();

                Debug.Log("コピー１");
            }
            //複数のゲームオブジェクトを取得してスクリプトを参照する方法
            //https://answers.unity.com/questions/783076/get-component-in-findgameobjectswithtags.html
            //EnemyWeaponHook_Behindのタグがついており、かつweaponhookがアタッチされているHookオブジェクトにBehindattackクラスのメンバをコピーする

            for (int j = 0; j < battacks.Length; j++)
            {
                w_a_behind_Hook[j] = battacks[j].GetComponent<WeaponHook>();
                w_a_behind_Hook[j].CopyBattackColliderandAttackPowerandAttackStrength(b_attacks);
                w_a_behind_Hook[j].CloseDamageColliders();
                Debug.Log("コピー２");

            }
            //RuntoAttackのタグがついており、かつweaponhookがアタッチされているHookオブジェクトにRuntoAttackクラスのインスタンスをコピーする
            for (int k = 0; k < rattacks.Length; k++)
            {
                w_a_Run_Hook[k] = rattacks[k].GetComponent<WeaponHook>();
                w_a_Run_Hook[k].CopyRattackColliderandAttackPowerandAttackStrength(r_attacks);
                w_a_Run_Hook[k].CloseDamageColliders();
            }
        }

    }


}