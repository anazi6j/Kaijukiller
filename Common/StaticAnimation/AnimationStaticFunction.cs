using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace robot
{

    public static class AnimationStaticFunction
    {

        public static float SetGettingParriedvalue(float parriedvalue)
        { //攻撃をはじき返された際のはじかれモーションをどの程度の値でブレンドするか決める

            //このメソッドを利用するには

            parriedvalue = Mathf.Clamp(parriedvalue, 0, 1);

            return parriedvalue;

        }

        public static bool cannotmovewhilegettingparried(float cannotmovetime)//上記メソッドと併用。
        {
            float time = 0;

            time += Time.deltaTime;

            if (time >= cannotmovetime)
            {
                return false;
            }

            return true;
        }
    }
}