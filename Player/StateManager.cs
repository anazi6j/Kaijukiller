using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Cinemachine;
using UnityEngine;




    namespace robot
{
    public class StateManager : MonoBehaviour
        




    {
        [Multiline]
        public string TODO;
        [Header("Init")]
        public GameObject player;

        [Header("Inputs")]
        public float vertical;
        public float horizontal;
        public float moveAmount;
        public Vector3 moveDir;//カメラの向いている方向を基準に左スティックを倒した方向のベクトル(InputHandlerを参照）
        public bool sankaku, shikaku, maru, bastu;
        public bool r1, r2, l1, l2;
        public bool dodgeinput;
        [Header("Stats")]
        public float moveSpeed=2;//移動速度
        public float runSpeed=3.5f;
        public float rotateSpeed = 5;
        public float toGround = 0.5f;
        public float stepspeed = 1f;
        public float chargerate;
        public float chargepower = 0;
        public float chargelimit=80;
        public float AngleBetweenPlayerandEnemy;
        const float ANGLEDEFAULT = 120f;
        const float INVISIBLETIME = 0.5f;

        [Header("States")]
        public bool run;
        public bool Onground;
        public bool lockOn;
        public bool inAction;
        public bool canMove;
        public bool isinvisible;
        public bool EnemyInsight;
        public bool inParryattack;
        [Header("Components")]
        public Animator anim;
        public Rigidbody rigid;
        public ParryHook parry;
        public AttackActionManager attackactionManager;
        public PlayerAttackInfoManager Ainfo;
        public CharacterStats characterStats;
      
        WeaPonManager WeaPonManager;
        
        public Transform target;

        anim_test anim_test;

        [Header("Other")]
        public Enemytarget lockonTarget;
        public Transform lockOnTransform;
        public AnimationCurve dodge_curve;
       public int Weaponmode = 0;//攻撃アニメーションの格納変数。0がパンチ。1がブレード展開
        public int R1ComboNum = 0;//R1攻撃アニメーションの格納変数。0がコンボ第一、1がコンボ第二
        Animationmanager AM;
       

        public float airTimer;
        public int attacknum;
        /*
        public ActionInput storePrevAction;
        public ActionInput storeActionInput;
        */

        public float delta;
        public LayerMask hitlayers;
        RaycastHit hit;
        public Action currentAction;
        // Use this for initialization
        public void Init()
        {
            attacknum = 0;
            canMove = true;
            SetupAnimator();
            rigid = GetComponent<Rigidbody>();
            rigid.angularDrag = 999;
            rigid.drag = 4;
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            parry = GetComponentInChildren<ParryHook>();
            parry.CloseParryCollider();
            WeaPonManager = GetComponent<WeaPonManager>();
            target = GameObject.Find("Axesdragon").transform;
           

            //Ainfo = GetComponent<PlayerAttackInfoManager>();
            //Ainfo.Init(this);

            //attackactionManager = GetComponent<AttackActionManager>();
            //attackactionManager.Init(this);


            AM = player.AddComponent<Animationmanager>();
            AM.Init(this);

            anim_test = GetComponent<anim_test>();

            gameObject.layer = 8;
            
        }

        void SetupAnimator()
        {
            if (player == null)
            {
                anim = GetComponentInChildren<Animator>();

                player = anim.gameObject;
            }

            if (anim == null)
                anim = player.GetComponent<Animator>();

          


        }

      

        
        float returnAngle()
        {
            float angle=0;

            if(target.tag =="Enemy")
            {
                angle = Vector3.Angle(transform.forward, target.position);

            }
            else if( target.tag!="Enemy"||target ==null)
            {
                angle = ANGLEDEFAULT;
               
            }

            return angle;
        }

        bool IsinSight()
        {
            if (AngleBetweenPlayerandEnemy <= 180f)
            {
                if (target.tag == "Enemy")
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
           
        }

        public void Tick(float d)
        {
            Debug.Log("更新中");
            delta = d;//inputhandlerのTime.deltaTimeを指す
            Onground = OnGround();

            inParryattack = anim.GetBool("inparryattack");

            

            Debug.DrawRay(transform.position, transform.forward,Color.red);
           

            AngleBetweenPlayerandEnemy = returnAngle();

            EnemyInsight = IsinSight();
            

            
            Weaponmode = WeaPonManager.AttackModeNum;
        }

        public void FixedTick(float d)//1フレームごとに呼ばれる
        {
           

            delta = d;//inputHandlerのTime.deltaTimeを指す

            this.FindAttackaction();
            

            canMove = anim.GetBool("canMove");
           
            anim.SetBool("isrunnning", run);
           
            float targetspeed = moveSpeed;

            if (!canMove)
                return;
            
           
                rigid.drag = (moveAmount > 0 || Onground == false) ? 0 : 4;
           
            if (run)
                targetspeed = runSpeed;

            if (!canMove)
                return;

            AM.Closedodge();
            //HandleDodge();

            rigid.velocity = moveDir * (targetspeed * moveAmount);

            Vector3 targetDir = (lockOn == false) ? moveDir : (lockOnTransform != null) ? lockOnTransform.position - transform.position : moveDir;
            //何かロックオンしていなければ、targetDirはベクトルmoveDir(左スティックを倒した方向）を取得するが、ロックオンボタンを押した際、ロックオンするターゲットが格納されていたらロックオンしたオブジェクトとプレイヤーの向きを取得する。

            targetDir.y = 0f;
            if (targetDir == Vector3.zero)//左スティックの入力がない場合
                targetDir = transform.forward;//targetDirはこのスクリプトをアタッチした物体のZ座標の正の方向を取得する

            //プレイヤーのロックオン時のアニメーション関連。
            anim.SetBool("lockon", lockOn);//ロックオンを操作
            if (lockOn==false)
            {
                HandleMovementAnimations();
            }
            else
            {
                //HandleMovementLockOnAnimation(moveDir);//左スティックの入力（movedir)を渡し、これをワールド座標からローカル座標のベクトルに変換する。図示する
            }
            //プレイヤーの向き関連
            Quaternion tr = Quaternion.LookRotation(targetDir);//左スティックを倒した方向にキャラクターが向く
            Quaternion targetrotation = Quaternion.Slerp(transform.rotation, tr,delta*moveAmount*rotateSpeed);//キャラクターが向くときの動作を滑らかにする
            transform.rotation = targetrotation;//左スティックを倒した方向に滑らかに向く
            HandleMovementAnimations();

            ChangeEquipMent();
        }


        //十字キーを押すと装備を変更する
        void ChangeEquipMent()
        {
            
            if (Input.GetAxis("DS4_DpadX")<0)
            {
                anim.Play(WeaPonManager.attacks[0].EquipMent_ChangeAnim);
                WeaPonManager.SwordisActive = false;

            }

            if(Input.GetAxis("DS4_DpadX")>0)
            {
                anim.Play(WeaPonManager.attacks[1].EquipMent_ChangeAnim);
                WeaPonManager.SwordisActive = true;
            }

            WeaPonManager.sword.SetActive(WeaPonManager.SwordisActive);
            //十字キー横を押したら
            //Weaponクラスのi番目のアニメーション1、アニメーション2を呼び出す
        }

        public void FindAttackaction()
        {

            

            if (canMove == false)
                return;

            if (r1 == false && r2 == false && l1 == false && l2 == false)
                return;


            if (r1)
            {

                if (EnemyInsight)//敵が視界に入ったら、パリィを喰らっているかどうか調べる
                {
                   
                    if (target.GetComponent<EnemyStateManager>()!=null&& target.GetComponent<EnemyStateManager>().getparried)
                    {
                        anim.Play(WeaPonManager.attacks[Weaponmode].ParryAttackAnim);
                       target.GetComponent<EnemyStateManager>().DoParriedDamage(characterStats.attackpower);
                    }
                    else if(target.GetComponent<EnemyStateManager>()!=null&&!target.GetComponent<EnemyStateManager>().getparried) {
                        //anim.Play(anim_test.r1attackanim1[Weaponmode]);
                        anim.Play(WeaPonManager.attacks[Weaponmode].R1AttackAnim[R1ComboNum]);
                        R1ComboNum++;
                    }//パリィを喰らっていたらパリィアタック。iは加算しない
                }//パリィを喰らっていなかったら通常攻撃。iを加算する
             
                Debug.Log(+R1ComboNum);
                //anim.Play(at.r1attackanim1[i]);
                

                if (R1ComboNum == 2)
                {
                    R1ComboNum = 0;
                }



            }
            if (r2)
            {

                //anim.Play(anim_test.r2attackanim1);
                anim.Play(WeaPonManager.attacks[Weaponmode].R2AttackAnim);
            }
            if(l1)
            {
                
                //anim.Play(anim_test.l1attackanim[i]);
            }
            if (l2)
            {
                
                anim.Play(anim_test.parryanim);
            }

            inAction = true;
            canMove = false;
           

            //ActionInput targetInput = attackactionManager.GetActionInput(this);
            //storeActionInput = targetInput;
            /* 
             if(onEmpty ==false)
               targetInput =storePrecAction;
             */

             /*
            storePrevAction = targetInput;
            Action slot = attackactionManager.GetActionSlot(this);
            if (slot == null)
                return;

            switch(slot.type)
            {
                case ActionType.attack:
                    AttackAction(slot);
                    break;
                case ActionType.block:
                    BlockAction(slot);
                    break;
                case ActionType.parry:
                    ParryAction(slot);
                    break;
                default:
                    break;
            }
            */
        }

        
       

        void AttackAction(Action slot)
        {
            string targetAnim = null;
            /*targetAnim =
                slot.GetActionSteps(ref attackactionManager.actionIndex)
                .GetBranch(storeActionInput).targetAnim;*/
            
            //

            
           //AttackActionからアニメーション情報を受け取る

            if (targetAnim == null)
                return;
           

            currentAction = slot;
            //canAttack =false;
            canMove = false;
            inAction = true;
            //onEmpty =false;
            

            anim.CrossFade(targetAnim, 0.2f);

            
            
            Debug.Log("R1attack");
        }

        void BlockAction(Action slot)
        {
            //isBlocking =true;
            
        }

        void ParryAction(Action slot)
        {
            string targetAnim= null;

            if (targetAnim == null)
                return;

            canMove = false;
            inAction = true;
            anim.CrossFade(targetAnim, 0.2f);
        }



        /*
        void HandleDodge()
        {
           
            if (!dodgeinput)
                return;
           
            float v = vertical;
            float h = horizontal;

            //左スティックの入力があった場合、アニメーションがブレンドされて滅茶苦茶にならないようにする
             v = (moveAmount > 0.3f) ? 1 : 0;
             h = 0;
            
            if (v != 0)
            {
                if (moveDir == Vector3.zero)
                    moveDir = transform.forward;
                Quaternion targetRot = Quaternion.LookRotation(moveDir);
                transform.rotation = targetRot;
                AM.Initfordodge();
                AM.rm_speed =stepspeed;//前方ステップのスピードを設定する
            }
            else
            {
                AM.rm_speed = 1.3f;
                //左スティックの入力がない場合、バックステップをする
            }

            anim.SetFloat("vertical", v);
            anim.SetFloat("horizontal", h);
           
            canMove = false;
          
            inAction = true;
            anim.Play("Dodge");
        }
        */
        void HandleMovementAnimations()
        {
            anim.SetBool("canMove", canMove);
            anim.SetFloat("vertical", moveAmount,0.4f,delta);
        }
        /*
        void HandleMovementLockOnAnimation(Vector3 moveDir)
        {
            Vector3 relativeDir = transform.InverseTransformDirection(moveDir);//これがないとどうなるか検証
            float v = relativeDir.z;
            float h = relativeDir.x;
            anim.SetFloat("vertical", v);
            anim.SetFloat("horizontal", h);
        }
        */
       public void DoDamage(int damage)
        {
           
              characterStats._health -= damage;
            isinvisible = true;
            StartCoroutine("setvisiblefalse");
        }
        IEnumerator setvisiblefalse()
        {
            yield return new WaitForSeconds(INVISIBLETIME);
            isinvisible = false;

            yield return null;
        }
        /*
      public  void R2charge()
        {
            
           
            if (anim.GetBool("ischarging") == false)
            {
                anim.SetBool("ischarging", true);
                
            }

            if (chargepower < chargelimit)
            {
                chargepower += Time.deltaTime*chargerate;
            }
            else
            {
                return;
            }


        }
        */
        /*
       public void R2attack()
        {
           
            anim.applyRootMotion = true;
            anim.SetBool("ischarging", false);
            anim.SetTrigger("comboattack");
            StartCoroutine("SetChargepowerZero");
        }
        */
       IEnumerator SetChargepowerZero()
        {
            yield return new WaitForSeconds(0.2f);
            chargepower = 0;
            
        }
        public bool OnGround()
        {
            bool r = false;

            Vector3 origin = transform.position + (Vector3.up * toGround);
            Vector3 dir = -Vector3.up;
            float dis = toGround + 0.3f;
            RaycastHit hit;
            Debug.DrawRay(origin, dir * dis);
            if (Physics.Raycast(origin, dir, out hit, dis, hitlayers))
            {
                r = true;
                Vector3 targetPosition = hit.point;
                transform.position = targetPosition;
            }

            return r;
        }
    }
}

