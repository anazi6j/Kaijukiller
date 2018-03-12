using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(CharacterController))]

public class E1enemymove : MonoBehaviour
{

    Vector3 posY;
    Quaternion quaX;

   

    public Animator animator;
    C2EnemyStats stats;
    AudioSource[] audiosource;
    CharacterController contorller;
    public static float attack1 = 10;
    public float attackpower;
    public float health;
    public float behTime;//行動開始タイマー。ランダムに決まる
    public float range;//プレイヤーと敵との距離
    
    public GameObject navigator;//突進を誘導する空のオブジェクトを宣言
    GameObject score;
    GameObject player;
    public Transform bressattack;
    public GameObject bullet;
    public float moverange;
    public float attackspeed=5f;
    Vector3 movedirection = Vector3.zero;

    void Start()
    {
        audiosource = gameObject.GetComponents<AudioSource>();
        navigator = GameObject.FindGameObjectWithTag("navigator");//誘導用のオブジェクトを宣言
        player = GameObject.FindGameObjectWithTag("Player");
        contorller = GetComponent<CharacterController>();
        behTime = 4f;
        stats = GetComponent<C2EnemyStats>();
        score = GameObject.FindGameObjectWithTag("Score");
       
    }

    

     

    // Update is called once per frame
    public void Update()
    {
        Vector3 freezepositionY;
        freezepositionY = new Vector3(transform.position.x, 1f, transform.position.z);

        health = stats.health;
        animator.SetFloat("HP", health);
      range = Vector3.Distance(navigator.transform.position, transform.position);
        animator.SetFloat("range", range);

     

        if (behTime > 0.5f)
        {
            Setnavigation(true);
            Debug.Log("行動前");
           
            animator.SetBool("moving", true);
            behTime -= Time.deltaTime;

        }
        else if (behTime < 0.6f)
        {
            Debug.Log("行動開始");

            Setnavigation(false);
            animator.SetBool("moving", false);
        }
       
        
    }

    void idlemove()
    {
        audiosource[3].Play();
        Vector3 attackrange = navigator.transform.position - transform.position;
        Vector3 direction = attackrange.normalized;//向きを取得
        Quaternion targetrot = Quaternion.LookRotation(direction);//dirの方を向く
        transform.rotation = Quaternion.Slerp(transform.rotation, targetrot, Time.deltaTime * 50);
       
        if (range > 5f)
        {
            Vector3 freezepositionY;
            freezepositionY = new Vector3(transform.position.x, 1f, transform.position.z);
            transform.position = freezepositionY;
            movedirection = direction;
                movedirection *= attackspeed;
                contorller.Move(movedirection * Time.deltaTime);
                Debug.Log("前進中");
            
        }
        else
        {
            Vector3 freezepositionY;
            freezepositionY = new Vector3(transform.position.x, 1f, transform.position.z);
            transform.position = freezepositionY;
            return;
        }
        
        
    }
    
    

    void Setnavigation(bool hasinformed)
    {
        switch (hasinformed) {
            case true:
        navigator.transform.position = player.transform.position;//誘導オブジェクトの座標を、プレイヤーの座標にセットする
                Debug.Log("座標セット");
                break;
            case false:
                Debug.Log("うぇひひ");
                break;

        
    }
    }

    void bress()
    {
        //弾を生成する
        Debug.Log("ブレスを吐いた");
        
      
        Instantiate(bullet, bressattack.transform.position, transform.rotation);
    }

    void endbressattack()
    {
       
            Debug.Log("行動終了1");
            behTime = Random.Range(1, 4);
        animator.SetBool("moving", true);


    }

    void endpressattack()
    {
        Debug.Log("行動終了2");
        behTime = Random.Range(2, 4);
        animator.SetBool("moving", true);

    }

    void endtaleattack()
    {

        
            Debug.Log("行動終了3");
            behTime = Random.Range(2, 5);
        animator.SetBool("moving", true);

        

    }


   void sound_taleattack()
    {

        audiosource[0].Play();
    }

    void sound_pressattack()
    {
        audiosource[1].Play();
    }

    void sound_Bressattack()
    {
        audiosource[2].Play();
    }
}
  