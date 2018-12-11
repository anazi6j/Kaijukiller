using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
    public class Enemytarget : MonoBehaviour
    {
        public int index;
        public List<Transform> targets = new List<Transform>();//Transformの配列


        public Transform GetTarget(bool negative = false)//Targetを取得する
        {
            if (targets.Count == 0)
                return transform;

            if (negative == false)
            {
                if (index < targets.Count - 1)
                    index++;
                else
                    index = 0;
            }
            else
            {
                if (index <= 0)
                    index = targets.Count - 1;
                else
                    index--;
            }
            index = Mathf.Clamp(index, 0, targets.Count);
            return targets[index];
        }
    }
}