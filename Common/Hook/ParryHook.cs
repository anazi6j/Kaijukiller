using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHook : MonoBehaviour {

    public GameObject parryCollider;
    public float parrystrength; //パリィ強度。これより敵の攻撃の強度が大きければ「はじく」
    public void CloseParryCollider()
    {
        parryCollider.SetActive(false);
    }
    public void OpenParryCollider()
    {
        parryCollider.SetActive(true);
    }
}
