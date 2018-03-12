using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class movemotor : MonoBehaviour {
    public float speed=1f;
    public float curspeed = 6f;
    Animator robot;
    C1Stats stats;
    public float jumpspeed = 8.0f;
    public float rotatespeed = 10f;
    public float gravity = 20.0f;
    public bool CanMove;

    public Vector3 moveDirection = Vector3.zero;
    public float V;
    public float H;
    public float moveparamater;
    CharacterController controller;




    //使用したプログラムソース:http://unitylab.wiki.fc2.com/wiki/%E3%83%97%E3%83%AC%E3%83%BC%E3%83%A4%E3%83%BC%E3%82%AD%E3%83%A3%E3%83%A9%E3%82%AF%E3%82%BF%E3%83%BC%E3%82%92%E5%8B%95%E3%81%8B%E3%81%99
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        stats = GetComponent<C1Stats>();
    }
    //注意：FBXデータを0.01（Unityにおける標準サイズ）倍にしないとCharacterControllerがうまく機能しない
    void Update()
    {

        V = Input.GetAxis("Vertical");
        H = Input.GetAxis("Horizontal");
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

        moveDirection =  (H * right + V * forward);
        moveDirection *= 0.02f / Time.deltaTime;
        moveDirection.y = 0f;
        moveparamater = moveDirection.magnitude;
       

        attachMove();
        attachRotation();
    }

   

   /* void CameraAxisControl()
    {
        if (controller.isGrounded)
        {
            Vector3 forward = -Camera.main.transform.TransformDirection(Vector3.forward);
            Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

            moveDirection = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward;
            moveDirection *= speed;
        }
    }*/

        void attachMove (){
       
        moveDirection.y -= gravity * Time.deltaTime;
        if (stats.health > 0)
        {
            
            controller.Move(moveDirection * Time.deltaTime);

        }
        }

    void attachRotation()
    {
        var moveDirectionYzero = moveDirection;
        moveDirectionYzero.y = 0;

        //ベクトルの２乗の長さを返しそれが0.001以上なら方向を変える（０に近い数字なら方向を変えない） 
        if (moveDirectionYzero.sqrMagnitude > 0.001)
        {

            //２点の角度をなだらかに繋げながら回転していく処理（stepがその変化するスピード） 
            float step = rotatespeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, moveDirectionYzero, step, 0f);

            if (stats.health > 0)
            {
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }
    }

    

    /*
    // Update is called once per frame
    void Update()
    {

        attachmove();
        attachrotation();
        if (SceneManager.GetActiveScene().name == "SelectingCharacter")
        {
            GetComponent<CharacterController>().enabled = false;
        }
        else
        {
            GetComponent<CharacterController>().enabled = true;
        }
    }

    void FixedUpdate()
    {


                V = Input.GetAxis("Vertical");
                H = Input.GetAxis("Horizontal");
                Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
                Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

                moveDirection = H * right + -V * forward;
                moveDirection *= 0.02f / Time.deltaTime;
                moveparamater = moveDirection.sqrMagnitude;



    }

    void attachmove()
    {
        moveDirection.y -= gravity * Time.deltaTime;
        if (CanMove)
        {
            controller.Move(moveDirection);
        }
    }

    void attachrotation()
    {
        var moveDirectionYzero = moveDirection;

        moveDirectionYzero.y = 0;
        if (moveDirectionYzero.sqrMagnitude > 0.001f)
        {
            float step = rotatespeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, moveDirectionYzero, step, 0f);

            transform.rotation = Quaternion.LookRotation(newDir);

        }

    }*/

}

