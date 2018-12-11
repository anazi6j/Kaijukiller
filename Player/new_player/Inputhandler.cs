using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace robot
{
    public class Inputhandler : MonoBehaviour
    {
        public float vertical;
        public float horizontal;
        bool maru_input;
        public bool batsu_input;
        public float batsu_delta;
        bool shikaku_input;
        bool sankaku_input;
        bool crosskey;
        bool pouse;//オプションキー
        public bool r2_input;
        public float r2_time;
        public bool r1_input;
        public bool l2_input;
        public bool l1_input;
        public bool leftAxis_down;
        public bool rightAxis_down;
        public bool DpadXRight;
        public bool DpadXLeft;
        StateManager state;
        WeaPonManager WeaPonManager;
        public TimeManager time;
        New_CameraControll cameracontroll;
        public scenemanager scene;

        float delta;
        void Start()
        {
            state = GetComponent<StateManager>();

            state.Init();
            WeaPonManager = GetComponent<WeaPonManager>();
            cameracontroll = New_CameraControll.singleton;
            cameracontroll.Init(this.transform);
        }

        void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            GetInput();
            UpdateStates();
            UpdatePause();
            state.FixedTick(delta);
            cameracontroll.Tick(delta);


        }

        void Update()
        {
            delta = Time.deltaTime;
            ResetInputNStates();
            state.Tick(delta);
            //WeaPonManager.Tick();
        }
        void GetInput()
        {
            //差し込まれたパッドに応じてstringを変えるのもありか
            //ex.l1_input=Input.GetButtonDown(GamePadString.leftup(xboxでいうLT,PS4でいうL1)
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            r1_input = Input.GetButtonDown("DS4_R1");
            r2_input = Input.GetButtonDown("DS4_R2");
            
            l1_input = Input.GetButtonDown("DS4_L1");
            l2_input = Input.GetButtonDown("DS4_L2");
            maru_input = Input.GetButtonDown("DS4_maru");
            sankaku_input = Input.GetButtonDown("DS4_sankaku");
            shikaku_input = Input.GetButtonDown("DS4_shikaku");
            batsu_input = Input.GetButton("DS4_batsu");
            DpadXRight = Input.GetAxis("DS4_DpadX") >0;
            DpadXLeft = Input.GetAxis("DS4_DpadX") <0;

            
            //l1とl2は後回し
            rightAxis_down = Input.GetButtonUp("DS4_R3");//R3ボタン
            //ポーズ画面で使用
            crosskey = Input.GetButtonDown("DS4_crosskey");//十字キー左
            
            pouse = Input.GetButtonDown("DS4_OPTIONS");//オプションキー
            if (batsu_input)
                batsu_delta += delta;

            
        }

        void UpdatePause()
        {
            time.pause = maru_input;
        }

        void UpdateStates()
        {
            state.horizontal = horizontal;
            state.vertical = vertical;
            Vector3 v = state.vertical * cameracontroll.transform.forward;//カメラのZ方向のベクトルを取得
            Vector3 h = horizontal * cameracontroll.transform.right;//カメラのX方向のベクトルを取得
            state.moveDir = (v + h).normalized;//方向を取得
            float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);//絶対値を返す。つまり左右上下どれを入力しても1になる。
            state.moveAmount = Mathf.Clamp01(m);//mの最小値、最大値を0~1の範囲で返す。
            
            if(batsu_input&&batsu_delta>0.5f)
            {
                state.run = (state.moveAmount > 0f);
            }
            

            
            

            if (rightAxis_down)//R3ボタンを押したら
            {
                if (state.lockonTarget == null)
                {
                    state.lockOn = false;
                }
                else
                {
                    state.lockOn = true;
                }
                //state.lockOn = !state.lockOn;



                cameracontroll.lockonTarget = state.lockonTarget;//カメラ側のlockonTargetはState側のそれと同じになる
                state.lockOnTransform = cameracontroll.lockonTransform;//ステート側のL_transformをカメラ側に代入する
                cameracontroll.LockOn = state.lockOn;//cameraとステート側両方を「ロックオンしている」状態にする
            }

            state.r1 = r1_input;
            state.r2 = r2_input;
            state.l1 = l1_input;
            state.l2 = l2_input;
            WeaPonManager.Right = DpadXRight;
            WeaPonManager.Left = DpadXLeft;
             

            if (rightAxis_down)
            {
                state.lockOn = !state.lockOn;

                if (state.lockonTarget == null)
                {
                    state.lockOn = false;
                }
                Debug.Log("LOCK");
                cameracontroll.lockonTarget = state.lockonTarget;
                state.lockOnTransform = cameracontroll.lockonTransform;
                cameracontroll.LockOn = state.lockOn;
            }


        }

        void ResetInputNStates()
        {
            if (batsu_input == false)
            {
                state.run = false;
                batsu_delta = 0;
            }
        }
      
    }
    
}

