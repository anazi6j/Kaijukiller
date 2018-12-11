using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
    public class PlayerAttackInfoManager : MonoBehaviour
    {
        public Weapon curAction;

        StateManager states;
        public GameObject parryCollider;
        public void Init(StateManager st)
        {
            states = st;
            SetAction(curAction);
            curAction.w_hook.CloseDamageColliders();
            
        }

        public void SetAction(Weapon a)
        {
            string targetIdle = a.idle_name;
            
            states.anim.Play(targetIdle);
        }
        public void CloseParryCollider()
        {
            parryCollider.SetActive(false);
        }
        public void OpenParryCollider()
        {
            parryCollider.SetActive(true);
        }

    }
    [System.Serializable]
    public class Weapon
    {
        public string idle_name;
        public WeaponHook w_hook;


        public Action GetAction(List<Action>l,ActionInput inp)
        {
            if (l == null)
                return null;

            for(int i = 0; i < l.Count; i++)
            {
                if(l[i].input == inp)
                {
                    return l[i];
                }
            }
            return null;
        }
    }
}