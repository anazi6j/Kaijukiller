using UnityEngine;
using System.Collections;

public class U2TPSmove : MonoBehaviour
{
    public float speed = 1f;
    public float dashspeed = 1.15f;
    public float curspeed = 6f;
    Animator robot;
    C1Stats stats;
    public float jumpspeed = 8.0f;
    public float rotatespeed = 10f;
    public float gravity = 1f;
    public bool CanMove;
    public int movetype;
    public Vector3 moveDirection = Vector3.zero;
    public float V;
    public float H;
    public float moveparamater;
    
   
    CharacterController controller;




    //使用したプログラムソース:http://unitylab.wiki.fc2.com/wiki/%E3%83%97%E3%83%AC%E3%83%BC%E3%83%A4%E3%83%BC%E3%82%AD%E3%83%A3%E3%83%A9%E3%82%AF%E3%82%BF%E3%83%BC%E3%82%92%E5%8B%95%E3%81%8B%E3%81%99
    // Use this for initialization
    void Start()
    {
        CanMove = true;
        movetype = 1;
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

        moveDirection = (H * right + V * forward);
        moveDirection *= 0.02f / Time.deltaTime;
        
        moveparamater = moveDirection.magnitude;

        checkdash();
        attachMove();//移動
        attachRotation();
       
    }

   
    void checkdash()
    {
        if (Input.GetButtonDown("dash"))
        {
            if ( moveparamater>0f&&movetype != 2)
            {
                movetype =2;
                Debug.Log("ダッシュできる");
            } else if(movetype == 2){
                movetype = 1;
                Debug.Log("ダッシュできない");
            }
           
        }
       
        //走っているときにR3ボタンを押すと
        //dashisokがtrueになる
    }
   

    void attachMove()
    {
        moveDirection.y = 0f;
        moveDirection.y -= gravity * Time.deltaTime;
        if (stats.health > 0)
        {
            if (CanMove)
            {
                if (movetype==1)
                {
                    controller.Move(moveDirection * Time.deltaTime);
                }
                else if (movetype==2)
                {
                    controller.Move((moveDirection * dashspeed) * Time.deltaTime);
                }

            }
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
                if (CanMove)
                {
                    transform.rotation = Quaternion.LookRotation(newDir);
                }
            }
        }
    }


}