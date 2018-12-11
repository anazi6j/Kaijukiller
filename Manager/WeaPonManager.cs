using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace robot
{
    
    public class WeaPonManager : MonoBehaviour
    {

        public List<AttackMode> attacks;//攻撃
        AttackMode mode;
        public string parryanim;
        public bool SwordisActive;
        public GameObject sword;
        public int AttackModeNum;
        public bool Right;
        public bool Left;
        public void Init()
        {
            mode.EquipMent_ChangeAnim = null;
            for(int i=0;i<mode.R1AttackAnim.Length;i++)
            {
                mode.R1AttackAnim[i] = null;
            }
            mode.R2AttackAnim = null;
            mode.ParryAttackAnim = null;
        }
        public void Update()
        {


            if (SwordisActive)
            {

                AttackModeNum = 1;
                Debug.Log("攻撃変更＝１");
                
            }//十字キーを押すと
            if (!SwordisActive)
            {
                AttackModeNum = 0;
            }//modeの内容は、modenum番目の要素のオブジェクト
        }

    }
    [System.Serializable]
    public class AttackMode
    {
        public string[] R1AttackAnim;
        public string R2AttackAnim;
        public string ParryAttackAnim;
        public string EquipMent_ChangeAnim;
    }
}