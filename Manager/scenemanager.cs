using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace robot
{
    public class scenemanager : MonoBehaviour
    {

        public EnemyStateManager es;
        public StateManager ps;
        public string title;
        public string game_win;
        public string game_lose;
        public string ingame;
        private int singleton =1;
        public bool crosskey;
        public bool pouse;//オプションキー
        public bool start;
        public float enemyhp;
        bool isstopping;
        const int GAMEISACTIVE = 1;
        const int GAMEISNOTACTIVE = 0;
        public enum SceneState
        {
            GAME_TITLE,
            GAME_WIN,
            GAME_LOSE,
            GAME_IN,


        }
        public SceneState S;
        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);
            S = SceneState.GAME_TITLE;

            Time.timeScale = GAMEISACTIVE;
            //PAUSE.SetActive(false);


        }

        public void GameStart()
        {
            SceneManager.LoadScene(ingame);
            S = SceneState.GAME_IN;
        }


        // Update is called once per frame
        void FixedUpdate()
        {

            if (es != null)
            {
                enemyhp = es.characterStats._health;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
            Ingamemanager();
        }




        void Ingamemanager()
        {
            if (enemyhp < 0)
            {
                Debug.Log("Win");
                GameClear();
            }
            if (ps != null)
            {
                if (ps.characterStats.hp < 0)
                {
                    GameOver();
                }
            }
            //敵が死んだら
            //gamewin
            //プレイヤーが死んだら
            //gamelose
        }

        //ゲームをポーズする
        void PoseManager()
        {
            //ifstateがingameで
            if (S == SceneState.GAME_IN)
            {
                if (pouse && isstopping == false)
                {
                    isstopping = true;
                    Time.timeScale = GAMEISNOTACTIVE;
                    //PAUSE.SetActive(true);
                }

                if (pouse && isstopping == true)
                {
                    isstopping = false;
                    Time.timeScale = GAMEISACTIVE;
                    //PAUSE.SetActive(false);
                }
            }
            //pouseが押されてかつisstoppingが無効なら
            //画面真ん中の「ポーズ」文字のゲームオブジェクトをactiveにする
            //ゲームスピードをGAMEISNOTACTIVEにする
            //optionが押されてかつisstoppingが有効なら
            ///画面真ん中の「ポーズ」文字のゲームオブジェクトをnotactiveにする
            //ゲームスピードをGAMEISACTIVEにする
        }



        public void GameClear()
        {

            if (singleton == 1)
            {
                S = SceneState.GAME_WIN;
                SceneManager.LoadScene(game_win);
            }
            singleton = 0;
        }

        public void GameOver()
        {
            if (S != SceneState.GAME_LOSE && S == SceneState.GAME_IN)
            {
                S = SceneState.GAME_LOSE;
                SceneManager.LoadScene(game_lose);
            }
        }

        public void GameRestart()
        {
            if (S == SceneState.GAME_LOSE || S == SceneState.GAME_WIN)
            {
                SceneManager.LoadScene(title);
            }

        }
    }
}