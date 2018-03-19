using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{   [System.Serializable]
    public class CharacterStats : MonoBehaviour
    {
        [Header("体力・スタミナ")]
        public float _health;
        [Header("ベースパワー")]
        public int hp = 100;

        [Header("攻撃力")]
        public int attackpower;

        [Header("防御力")]
        public int physical;
        public int vs_strike;
        public int vs_thrust;
        public int vs_slash;




        public void InitHpandStamina()
        {
            _health = hp;
        }

        [System.Serializable]
        public class Attributes
        {
            public int level;
            public int exp = 0;
        }

        [System.Serializable]
        public class WeaponStats
        {
            public int physical;
            public int strike;
            public int slash;
            public int thrust;
        }
    }
}
