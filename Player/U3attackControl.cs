using UnityEngine;
using System.Collections;

public class U3attackControl : MonoBehaviour
{

    public GameObject AttackCollider;//攻撃用コライダーを用意
    Animator robot;//アニメーションコンポーネントを参照
    

    // Use this for initialization
    void Start()
    {
        robot = GetComponent<Animator>();
        AttackCollider.SetActive(false);

    }


    

    public void SetCactive() {
        AttackCollider.SetActive(true);
    }

    public void SetCfalse() {
        AttackCollider.SetActive(false);


    }
    

}
