using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
    public class Animationmanager : MonoBehaviour
    {
        public Animator anim;
        StateManager state;

        public float rm_speed;
        bool dodging;
        float dodge_t;

        public void Init(StateManager st)
        {
            state = st;
            anim = st.anim;

           
        }

        public void Initfordodge()
        {
            dodging = true;
            dodge_t = 0;

        }
        public void Closedodge()
        {
            if (dodging == false)
                return;

            rm_speed = 1;
            dodge_t = 0;
            dodging = false;
        }
        void OnAnimatorMove()
        {
            if (state.canMove)
                return;
            

            if (state.canMove)
             return;
            

            state.rigid.drag = 0;

            if (rm_speed == 0)
                rm_speed = 1;

          
         
           

            if (dodging == false)
            {
                Debug.Log("BackStep");
                Vector3 delta = anim.deltaPosition;
                delta.y = 0;
                Vector3 v = (delta * rm_speed) / state.delta;
                state.rigid.velocity = v;

            }
            else
            {
                Debug.Log("Dodge");
                dodge_t += state.delta / 0.6f;

                if (dodge_t > 1)
                {
                    dodge_t = 1;
                }
                float zvalue = state.dodge_curve.Evaluate(dodge_t);
                Vector3 v1 = Vector3.forward * zvalue;
                Vector3 relative = transform.TransformDirection(v1);
                Vector3 v2 = (relative * rm_speed);

                state.rigid.velocity = v2;
            }

        }

        public void OpenDamageColliders()
        {
            state.Ainfo.curAction.w_hook.OpenDamageColliders();
        }

        public void CloseDamageColliders()
        {
            state.Ainfo.curAction.w_hook.CloseDamageColliders();
        }

        public void OpenParryCollider()
        {
            if (state == null)
                return;

            state.Ainfo.OpenParryCollider();
        }
        public void CloseParryCollider()
        {
            if (state == null)
                return;

            state.Ainfo.CloseParryCollider();
        }

    }


    }



  