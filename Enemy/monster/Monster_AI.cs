using UnityEngine;
using System.Collections;

public class Monster_AI : MonoBehaviour {
   
    public Animator animator;
    
    public float range;
    public bool rotate;// Use this for initialization
    public float rotatespeed;//振り向くスピード
    public Transform player;
    public Transform navigator;
    Vector3 movedir = Vector3.zero;
    void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
        movedir.y -= 9.8f * Time.deltaTime;
        GetComponent<CharacterController>().Move(movedir * Time.deltaTime);
        CheckRange();
      
        if (rotate)
        {
            rotation();
        }

    }

    void CheckRange()
    {
        //http://gamesonytablet.blogspot.jp/2013/02/unityai6.html
       
       
        range = Vector3.Distance(transform.position, player.position);
        animator.SetFloat("Range", range);
        
    }

    void rotation()
    {
        Quaternion LookatPlayer = Quaternion.LookRotation(navigator.position - transform.position);
        LookatPlayer.x = 0;
        LookatPlayer.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, LookatPlayer, Time.deltaTime * rotatespeed);
        navigator.position = player.position;
    }

    void Setrotateactive()
    {
        rotate = true;
    }

    void Setrotateinactive()
    {
        rotate = false;
    }

}
