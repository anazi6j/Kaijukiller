using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{

    public class Parametar_Cameracontroll : MonoBehaviour
    {
        public GameObject[] button;
        Camera cam;
        public int i=0;
        public float campos_Z;
        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            //このスクリプトをアタッチしたゲームオブジェクトの位置は各ボタンの位置と連動する
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, GetCamerapos(),Time.deltaTime*100);
            if (Input.GetAxis("DS4_DpadX")>0)
            {
                i = 0;
            }
            else if(Input.GetAxis("DS4_DpadX") <0)
            {
                i = 1;
            }
            
        }


        Vector3 GetCamerapos()
        {
            Vector3 campos=new Vector3(0,0,0);
            campos.x = button[i].transform.position.x;
            campos.y = button[i].transform.position.y;
            campos.z = cam.transform.position.z;
            return campos;
        }
    }


}
