using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
    public class CamManager : MonoBehaviour
    {
        Camera cam;
        const float CAM_MAXZOOM = 35f;
        const float CAM_DEFAULT=70f;
        const float SPEED = 200f;
        public StateManager state;
        // Use this for initialization
        void Start()
        {
            cam = Camera.main;
            cam.fieldOfView = 70f;
            
            state = GameObject.Find("InputHandler").GetComponent<StateManager>();
        }

        // Update is called once per frame
        void Update()
        {//パリィ中かどうかはStateManagerのbool値"inparryattack"を参照
           

            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, CAM_MAXZOOM, CAM_DEFAULT);
           
                if (state.inParryattack)
                {
                if (cam.fieldOfView != CAM_MAXZOOM)
                {
                    cam.fieldOfView -= Time.deltaTime * SPEED;
                }

                }
            //ifパリィ中、fieldofviewがCAM_MAXZOOMの値になるまでTime.deltatime分デクリメントする
            if (!state.inParryattack) {
                if(cam.fieldOfView == CAM_DEFAULT)
                {
                    return;
                }else if(cam.fieldOfView <=CAM_DEFAULT)
                {

                    cam.fieldOfView += Time.deltaTime*SPEED;
                }

            }//ifnotパリィ中、fieldofviewが既にCAM_DEFAULTでない限りfieldofViewはCAM_DEFAULTになるまでTime.deltatime分インクリメントする
        }
    }
}