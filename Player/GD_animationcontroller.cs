using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GD_animationcontroller : MonoBehaviour
{
    public Animator ani;
    C1Stats stats;
    U2TPSmove move;
    CharacterController con;
    /*溜め攻撃関連*/
    public float addattack = 0;
    public float addrate;
    public float limit;

    // Use this for initialization
    void Start()
    {
        con = transform.root.GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
        stats = transform.root.GetComponent<C1Stats>();
        move = transform.root.GetComponent<U2TPSmove>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            superdamage();
        }
        ani.SetBool("isgrounded",con.isGrounded);
        ani.SetFloat("movedir", move.moveparamater);
        R1attack();//R1の通常攻撃
        R2Chargeattack();//R2の溜め攻撃
        Checkisdead();//体力がゼロになった時、ダウンアニメーションを再生させ動けなくする
    }

    void R1attack()
    {
        /*R1を押すと縦斬り*/
        if (Input.GetButtonDown("DS4_R1"))
        {
            move.CanMove = false;
            ani.SetBool("コンボ可", true);
            ani.SetTrigger("attack");
            Debug.Log("縦斬り");

        }
    }

    void R2Chargeattack()
    {
        /*R2ボタンを押し続けている間溜め動作をし、離すと溜め攻撃を行う。溜めた時間に応じて攻撃力が上がる*/
        if (Input.GetButton("DS4_R2"))
        {
            move.CanMove = false;
            addattack += addrate;
            if (ani.GetBool("ischarging") == false)
            {

                ani.SetBool("ischarging", true);

                Debug.Log("溜め攻撃を行ってる");

            }
        }

        if (Input.GetButtonUp("DS4_R2"))
        {
            move.CanMove = false;
            if (ani.GetBool("ischarging") == true)
            {
                Chargeattack();
                Debug.Log("溜め攻撃を放った");
            }
        }
    }

    void Checkisdead()
    {

        if (stats.health <= 0)
        {
            if (stats.dead == false)
            {
                ani.SetTrigger("death");
                stats.dead = true;
                move.CanMove = false;
            }
        }

    }
    
    void Chargeattack()
    {
        stats.attackpower = addattack + stats.attackpower;
        Debug.Log("攻撃力：" + stats.attackpower);
        ani.SetBool("ischarging", false);
        ani.SetTrigger("attack");
       
        
    }

    void EnableMove()
    {
        if (!move.CanMove)
        {

            move.CanMove = true;
            addattack = 0f;
           
        }
    }
    void damage()
    {

        ani.SetTrigger("damage");
        Debug.Log("ダメージアニメーション");
    }
    void superdamage()
    {
        Debug.Log("ダメージ");
        
        if (ani.GetBool("isgrounded"))
        {
            move.CanMove = false;
            ani.SetTrigger("damage");
           
        }     
    }
}
