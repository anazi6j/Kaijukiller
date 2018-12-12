using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace robot
{
    public class WeaponHook : MonoBehaviour
    {
        public GameObject damageCollider;
        
        public int weaponid;//WeaponHookのID。ID
        public int attackpower;
        int animationnumber;
        public float attackstrength;
        WeaponHook w_a;
        


        private void Start()
        {
            if(damageCollider == null)
            {
                return;
            }
            damageCollider.SetActive(false);

            //AIManagerのC_attackからコライダーと攻撃力、攻撃強度を取得する
        }

        //AIManagerに格納されたコライダーや攻撃力、攻撃強度をコピーする
        public void CopyCattackColliderandAttackPowerandAttackStrength(List<CloseAttacks> from)
        {

            damageCollider = from[weaponid].colliders;
            attackpower = from[weaponid].attackpower ;
            attackstrength=from[weaponid].attackstrength ;
            
        }

        public void CopyBattackColliderandAttackPowerandAttackStrength(List<BehindAttacks> from)
        {
            damageCollider = from[weaponid].colliders;
            attackpower = from[weaponid].attackpower;
            attackstrength = from[weaponid].attackstrength;
        }

        public void CopyRattackColliderandAttackPowerandAttackStrength(List<RuntoAttack> from)
        {
            damageCollider = from[weaponid].colliders;//左が変数を受け取る側で、右側が変数を渡す側である
            attackpower = from[weaponid].attackpower;
            attackstrength = from[weaponid].attackstrength;
        }

       

        public void OpenDamageColliders()
        {
            
            damageCollider.SetActive(true);
            
        }


        public void CloseDamageColliders()
        {
            
           damageCollider.SetActive(false);
            
        }


    }

}