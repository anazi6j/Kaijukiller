using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
    public static class StatsCaluclation
    {

        public static int CalculateBaseDamage(WeaponStats w,CharacterStats st)
        {
            int physical = w.physical - st.physical;
            int strike = w.strike - st.vs_strike;
            int slash = w.slash - st.vs_slash;
            int thrust = w.thrust - st.vs_thrust;

            int sum = physical + strike + slash + thrust;

            return sum;
        }

    }
}