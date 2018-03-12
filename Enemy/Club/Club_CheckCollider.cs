using UnityEngine;
using System.Collections;

public class Club_CheckCollider : MonoBehaviour {

    
    Club_AI club;
    void Start()
    {

        club = GameObject.Find("fourclub3").GetComponent<Club_AI>();
        club.NonActive();
    }

    void OnTriggerStay(Collider target)
    {
        if (target.tag == "Player")
        {
           
            club.Active();
            Debug.Log("IN");
        }
     }

    void OnTriggerExit(Collider target)
    {
        if(target.tag == "Player")
        {
            club.NonActive();
            Debug.Log("OUT");
        }
    }
    }
