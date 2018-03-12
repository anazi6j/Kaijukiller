using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
    PlayerControl plc;

    public float rollspeed = 15;

    float horizontal;
    float vertical;
    bool rollInput;

    public bool rolling;
    bool hasrolled;

    Vector3 directionPos;
    Vector3 stoneDir;
    Transform camholder;
    Rigidbody rb;
    Animator ani;
    public bool hasDirection;

    Vector3 dirForwards;
    Vector3 dirsides;
    Vector3 dir;

    // Use this for initialization
    void Start() {
        plc = GetComponent<PlayerControl>();
        camholder = plc.camHolder;
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }
        // Update is called once per frame
        void FixedUpdate () {
        this.rollInput = plc.rollInput;
        this.horizontal = plc.horizontal;
        this.vertical = plc.vertical;
        stoneDir = camholder.right;

        if(rollInput) {
        if(!rolling) {
                plc.canmove = false;
                rolling = true;

        }
        }
        if(rolling) {

            if(!hasDirection) {
                dirForwards = stoneDir * horizontal;
                dirsides = camholder.forward * vertical;

                directionPos = transform.position + (stoneDir * horizontal) + (camholder.forward * vertical);
                dir = directionPos - transform.position;
                dir.y = 0;

                ani.SetTrigger("Roll");
                hasDirection = true;
            }
            rb.AddForce((dirForwards + dirsides).normalized * rollspeed);

            float angle = Vector3.Angle(transform.forward, dir);
       if(angle !=0) 
       {
                rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 15 * Time.deltaTime);
       }
         }
        }
       }
        

    

