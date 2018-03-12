using System.Collections;
using System.Collections.Generic;
using UnityEngine;




    namespace robot
{
    public class StateManager : MonoBehaviour
    {   [Header("Init")]
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
        
        [Header("States")]
        public bool run;
        public bool Onground;
        public bool lockOn;
        public bool inAction;
        public bool canMove;
        [Header("Components")]
        public Animator anim;
        public Rigidbody rigid;
        public AttackActionManager attackactionManager;
        public PlayerAttackInfoManager Ainfo;

        [Header("Other")]
        public Enemytarget lockonTarget;
        public Transform lockOnTransform;
        public AnimationCurve dodge_curve;

        Animationmanager AM;

        C1Stats stats;


        public float delta;
        public LayerMask ignoreLayers;
        // Use this for initialization
        public void Init()
        {
            stats = GetComponent<C1Stats>();
            canMove = true;
            SetupAnimator();
            rigid = GetComponent<Rigidbody>();
            rigid.angularDrag = 999;
            rigid.drag = 4;
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            Ainfo = GetComponent<PlayerAttackInfoManager>();
            Ainfo.Init(this);

            attackactionManager = GetComponent<AttackActionManager>();
            attackactionManager.Init(this);
            
            AM = player.AddComponent<Animationmanager>();
            AM.Init(this);

            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);
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

        

        public void Tick(float d)
        {
            delta = d;//inputhandlerのTime.deltaTimeを指す
            Onground = OnGround();
        }

        public void FixedTick(float d)//1フレームごとに呼ばれる
        {
           

            delta = d;//inputHandlerのTime.deltaTimeを指す

            FindAttackaction();

            if (inAction)
            {
              
            }

            canMove = anim.GetBool("canMove");

            float targetspeed = moveSpeed;

            if (!canMove)
                return;
            
           
                rigid.drag = (moveAmount > 0 || Onground == false) ? 0 : 4;
           
            if (run)
                targetspeed = runSpeed;

            if (!canMove)
                return;

            AM.Closedodge();
            HandleDodge();

            rigid.velocity = moveDir * (targetspeed * moveAmount);
                                                  
            Vector3 targetDir = (lockOn == false) ? moveDir : (lockOnTransform != null) ? 
                lockOnTransform.position - transform.position 
                : moveDir;
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
                HandleMovementLockOnAnimation(moveDir);//左スティックの入力（movedir)を渡し、これをワールド座標からローカル座標のベクトルに変換する。図示する
            }
            //プレイヤーの向き関連
            Quaternion tr = Quaternion.LookRotation(targetDir);//左スティックを倒した方向にキャラクターが向く
            Quaternion targetrotation = Quaternion.Slerp(transform.rotation, tr,delta*moveAmount*rotateSpeed);//キャラクターが向くときの動作を滑らかにする
            transform.rotation = targetrotation;//左スティックを倒した方向に滑らかに向く
            HandleMovementAnimations();
           
        }

        public void FindAttackaction()
        {

            if (canMove == false)
                return;

            if (r1 == false && r2 == false && l1 == false && l2 == false)
                return;

            string targetAnim = null;


            Action slot = attackactionManager.GetActionSlot(this);
            if (slot == null)
                return;
            targetAnim = slot.targetAnim;

            if (string.IsNullOrEmpty(targetAnim))
                return;

            canMove = false;
            inAction = true;
            anim.CrossFade(targetAnim, 0.2f);


        }



        void HandleMovementAnimations()
        {
            anim.SetBool("canMove", canMove);
            anim.SetFloat("vertical", moveAmount,0.4f,delta);
        }

        void HandleMovementLockOnAnimation(Vector3 moveDir)
        {
            Vector3 relativeDir = transform.InverseTransformDirection(moveDir);//これがないとどうなるか検証
            float v = relativeDir.z;
            float h = relativeDir.x;
            anim.SetFloat("vertical", v);
            anim.SetFloat("horizontal", h);
        }

      
        public bool OnGround()
        {
            bool r = false;

            Vector3 origin = transform.position + (Vector3.up * toGround);
            Vector3 dir = -Vector3.up;
            float dis = toGround + 0.3f;
            RaycastHit hit;
            Debug.DrawRay(origin, dir * dis);
            if (Physics.Raycast(origin, dir, out hit, dis, ignoreLayers))
            {
                r = true;
                Vector3 targetPosition = hit.point;
                transform.position = targetPosition;
            }

            return r;
        }
    }
}

