using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
    
   
    public class DamageCollider : MonoBehaviour
    {
        public Animator anim;
        void OnTriggerEnter(Collider other)
        {
            int testnum = 12;
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.transform.GetComponent<EnemyStateManager>().DoDamage(testnum);
            }
            else if(other.gameObject.tag == "Player"){
                other.gameObject.transform.GetComponent<StateManager>().DoDamage(testnum);
            }
            SetHitStop();
        }

        void SetHitStop()
        {
            anim.speed = 0f;

            StartCoroutine("SetAnimSpeedNormal");
        }

        IEnumerator SetAnimSpeedNormal()
        {
            new WaitForSeconds(0.1f);

            anim.speed = 1f;
            yield return null;
        }

    }
}
