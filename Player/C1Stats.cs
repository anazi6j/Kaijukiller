using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class C1Stats : MonoBehaviour {

    /// <summary>
    /// http://qiita.com/amano-kiyoyuki/items/43fa92cce1a44a8030b5　音の再生
    /// </summary>

    public Text armortext;
    AudioSource aud;
    public AudioClip damageclip;
    public Camera _cam;
    Camera_Shake camerashake;
    public float health=100f;
    public float maxhealth = 100f;

    public float attackpower = 30f;
    public float defence = 0.2f;

    public float stamina = 100;
    public float maxstamina = 100;

    public float staminaregenRate = 5;
    public float healthrgenrate = 1;

    public bool dead;
    public bool invisible;
    public float invtimemax = 3f; 
    public bool guarding;
    
    
    // Use this for initialization
    void Start () {


        _cam = Camera.main;
        health = maxhealth;
        stamina = maxstamina;
        dead = false;
        aud = GetComponent<AudioSource>();
        camerashake = _cam.GetComponent<Camera_Shake>();
       
        
	}
	
	// Update is called once per frame
	void Update () {
        //test
       
        //armortext.text = health.ToString();
        if (attackpower > 29f)
        {
            attackpower--;
        }
    }

    void HandleRegeneration() {
        
    if(health<100)
    {
            health += Time.deltaTime * healthrgenrate;


    }else  {

            health = maxhealth;
    }

    if(stamina <100)
    {
            stamina += Time.deltaTime * staminaregenRate;
    }else
    {
            stamina = maxstamina;


    }
        
   
    }
   
    public void Damage(float hit) {
        camerashake.Shake();
        if (!guarding)
        {

            health -= hit;
            Debug.Log("ダメージを受けた" + hit);
            aud.clip = damageclip;
            aud.Play();
          
           
        }
        else
        {
            Debug.Log("ダメージを軽減");
            health -= (0.2f*defence) * hit;
        }

    }
}
