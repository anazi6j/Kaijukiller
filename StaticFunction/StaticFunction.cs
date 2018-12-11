using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace robot
{
    public class StaticFunction : MonoBehaviour
    {
        public static void DeepCopyWeapon(Weapon from,Weapon to)
        {
           
        }

        public static void DeepCopyActionToAction(Action a,Action w_a)
        {
            a.input =w_a.input;
            a.targetAnim = w_a.targetAnim;
            a.type = w_a.type;
        }

        public static void DeepCopyAction(Weapon w,ActionInput inp,ActionInput assign,List<Action> actionList)
        {
            Action a = GetAction(assign, actionList);
            //Action w_a = w.GetAction(w.actions, inp);
            /*if(w_a == null)
            {
                return;
            }
            a.targetAnim = w_a.targetAnim;
            a.type = w_a.type;*/
        }

        public static Action GetAction(ActionInput inp, List<Action> actionSlots)
        {
            for (int i = 0; i < actionSlots.Count; i++)
            {
                if (actionSlots[i].input == inp)
                    return actionSlots[i];
            }

            return null;
        }
    }

}