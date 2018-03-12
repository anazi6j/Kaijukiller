using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class C2EnemyStats : MonoBehaviour
{
    AudioSource source;
    public AudioClip damageclip;

    public Text enemyarmor;


    public float health;
    public float maxhealth;
    public float attackpower;
    public float defence;
    //public float stamina = 100;
    //public float maxstamina = 
    //public float staminaregenRate = 5;
    //public float healthrgenrate = 1;
    public int getmoney=1000;
    public bool dead;
    public bool invisible;
    public float invtimemax = 3f;
    public bool guarding;
    int death = 1;
    public Animator animator;
    ScoreSystem score;
    // Use this for initialization
    void Start()
    {
        health = maxhealth;
        source = gameObject.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        defence = defence / 100;
        //score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreSystem>();

    }

    // Update is called once per frame
    void Update()
    {

        if (health < 0)
        {
            if (death == 1)
            {
                animator.SetBool("Death", true);
                death = 0;
            }
            else
            {
                animator.SetBool("Death", false);
            }
            
        }
    }

    public void Damage(float hit)
    {
        
        if (!guarding)
        {
            health -= hit * defence;
            source.clip = damageclip;
            Debug.Log("ダメージを受けた" + hit * defence);
            source.Play();
        }
       

    }

    public void Ondeath()
    {
        Debug.Log("報酬金をゲット:"+getmoney+"円");
        score.SendMessage("addmoney", getmoney);
    }

}
