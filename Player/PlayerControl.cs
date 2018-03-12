using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {
    Rigidbody rb;
    Animator ani;
    CapsuleCollider cap;
    Transform cam;
    


    [HideInInspector]
    public Transform camHolder;
    [SerializeField] float lockspeed = 0.5f;
    [SerializeField]float normSpeed = .8f;
    float speed;
    [SerializeField]
    float turnspeed = 5;

    Vector3 directionPos;
    Vector3 storeDir;

    [HideInInspector]
    public float horizontal;

    [HideInInspector]
    public float vertical;
    [HideInInspector]
    public bool rollInput;

    public bool locktarget;
    int curTarget;
    bool changeTarget;
    float targetturnAmount;
    float curTurnAmount;
    public bool canmove;
    public List<Transform> Enemies = new List<Transform>();
    public Transform camTarget;
    public float camTargetSpeed = 5;
    Vector3 targetPos;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        camHolder = cam.parent.parent;
        cap = GetComponent<CapsuleCollider>();
        SetUpAnimator();
        GetComponent<PlayerAnimation>().enabled = true;
        	}
	
	// Update is called once per frame
	void FixedUpdate () {
        HandleInput();
        HandleCameraTarget();

        if(canmove) {
        if(!locktarget) {
                speed = normSpeed;
                HandleMovementNormal();
        }
        else {
                speed = lockspeed;
                if(Enemies.Count >0) {
                    HandleMovementLockOn();
                    HandleRotationOnLock();
                }
                else {
                    locktarget = false;
                }
        }
        }
	}
    void HandleInput() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rollInput = Input.GetButton("Fire3");
        storeDir = camHolder.right;//カメラではなくカメラホルダーの方向を取得する。
        //そのためカメラの見ている方向が原因の移動の違いが避けられる
        ChangeTargetslogic();

    }
    void ChangeTargetslogic() {
    if(Input.GetButtonUp("Fire2"))
    {
            locktarget = !locktarget;
    }
    if(Input.GetKeyUp(KeyCode.Q))
    {
    if(curTarget <Enemies.Count -1)
    {
                curTarget++;
    }else
    {

                curTarget = 0;
    }
    }
    }

    void HandleCameraTarget() {
    if(canmove) {
            if (!locktarget)
            {
                speed = normSpeed;
                HandleMovementNormal();
            }else
            {
                speed = lockspeed;

                if(Enemies.Count>0) {
                    HandleMovementLockOn();
                    HandleRotationOnLock();
                }else {
                    locktarget = false;
                }
            }
         }

    }
    void HandleMovementLockOn() {
        Transform camHolder = cam.parent.parent;
        Vector3 camForward = Vector3.Scale(camHolder.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(camHolder.right, new Vector3(1, 0, 1).normalized);
        Vector3 move = vertical * camForward + horizontal * cam.right;

        Vector3 moveForward = camForward * vertical;
        Vector3 moveSideways =camRight * horizontal;
        rb.AddForce((moveForward + moveSideways).normalized * speed / Time.deltaTime);

        ConvertMoveInputAndPassItToanimator(move);

}
    void HandleRotationOnLock() {
        Vector3 lockPos = Enemies[curTarget].position;

        Vector3 lookDir = lockPos - transform.position;
        //yの値をゼロにする
        lookDir.y = 0;

        Quaternion rot = Quaternion.LookRotation(lookDir);
        rb.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnspeed);
    }
    void HandleCamaraTarget() {
    if(!locktarget)
    {
            targetPos = transform.position;
    }
    else {
    if(Enemies.Count >0) {
    Vector3 direction = Enemies[curTarget].position - transform.position;
    direction.y = 0;
    float distance = Vector3.Distance(transform.position, Enemies[curTarget].position);
                targetPos = direction.normalized * distance / 4;            
    
    targetPos += transform.position;

                if(distance >20) {
                    locktarget = false;
                }
      }
     }
    }
    void HandleMovementNormal() {
        canmove = ani.GetBool("CanMove");
        Vector3 dirforward = storeDir * horizontal;
        Vector3 dirSides = camHolder.forward * vertical;
        if (canmove)//移動速度を抑えるため、単位化して方向だけを取り出す
            rb.AddForce((dirforward + dirSides).normalized * speed / Time.deltaTime);
        directionPos = transform.position + (storeDir * horizontal) + (cam.forward * vertical);
        Vector3 dir = directionPos - transform.position;
        dir.y = 0;

        float angle = Vector3.Angle(transform.forward, dir);   
        
        float animvalue = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        animvalue = Mathf.Clamp01(animvalue);
        ani.SetFloat("Forward", animvalue);
        ani.SetBool("LockOn", false);
        if(horizontal !=0 || vertical !=0) {
        if(angle!= 0&& canmove) {
                rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnspeed * Time.deltaTime);
        }
        }
          }
    void SetUpAnimator() {
        ani = GetComponent<Animator>();

        foreach(var childAnimator in GetComponentsInChildren<Animator>()) {
        if(childAnimator != ani) {
                ani.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
        }

        }

    }
    void ConvertMoveInputAndPassItToanimator(Vector3 moveInput) {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        float turnAmount = localMove.x;
        float forwardAmount = localMove.z;

        if (turnAmount != 0)
            turnAmount *= 2;

        ani.SetBool("LockOn", true);
        ani.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        ani.SetFloat("SideWays", turnAmount, 0.1f, Time.deltaTime);

    }
}
