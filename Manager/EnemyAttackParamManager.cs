using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

    public class EnemyAttackParamManager : MonoBehaviour
    {
        [Multiline]
        public string TODO;
        public CloseAttacks[] closeAttacks;
        public BehindAttacks[] behindAttacks;
        public RuntoAttack[] runtoAttack;
        public GetParried getParried;
        public MoveAnimation move;

    }
   

        public  class Attacks
        {
            public string attackanim;
            public int attackpower;
            public float attackstrength;//攻撃強度。相手がパリィしてきた際に、数値が上回っていたら削りダメージを与える
            public float intervaltime;
            public bool canbeParried;
            public GameObject colliders;

  


}


        [System.Serializable]
        public class CloseAttacks : Attacks
        {

            private int closeattacknum;

    public CloseAttacks(string animation ,int power,float strength,float interval,bool beparried,GameObject collider)
    {
        attackanim = animation;
        attackpower = power;
        attackstrength = strength;
        intervaltime = interval;
        canbeParried = beparried;
        colliders = collider;
    }
   
    

   
}

        [System.Serializable]
        public class BehindAttacks : Attacks
        {


            private int behindattacknum;//攻撃アニメーションをランダムに決定する

    public BehindAttacks(string animation, int power, float strength, float interval, bool beparried, GameObject collider)
    {
        attackanim = animation;
        attackpower = power;
        attackstrength = strength;
        intervaltime = interval;
        canbeParried = beparried;
        colliders = collider;
    }

}

        [System.Serializable]
        public class RuntoAttack : Attacks
        {
    public RuntoAttack(string animation, int power, float strength, float interval, bool beparried, GameObject collider)
    {
        attackanim = animation;
        attackpower = power;
        attackstrength = strength;
        intervaltime = interval;
        canbeParried = beparried;
        colliders = collider;
    }
}

        [System.Serializable]
        public struct GetParried
        {
            public string parried;
            public float intervaltime;
        }
    
         [System.Serializable]
         public struct ParriedDamage
{
    public string GettingParriedDamage;
    public float intervaltime;
}
[System.Serializable]
public struct ShrinkDamage
{
    public string ShrinkingDamageAnim;
    public float intervaltime;
}
[System.Serializable]
public class MoveAnimation
{
    public string Idle;//idle時のアニメーション

    public string PlayerInCloseAnim;//プレイヤーが近くにいる場合のアニメーション

    public string PlayerInClose_MovingAnim;//プレイヤーが近くにいる＋回り込んでくる場合のアニメーション

    public string RunningAnim;//走るアニメーション

    public string Death;//戦闘不能になった際のアニメーション
}