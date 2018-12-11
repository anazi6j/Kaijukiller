using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WeaponHook : MonoBehaviour {
    public GameObject damageCollider;

    float attackpower;

    float attackstrength;

    public void Start()
    {
        damageCollider.SetActive(false);
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
