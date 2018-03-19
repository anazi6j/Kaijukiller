using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace robot
{
    public class EnemyStateManager : MonoBehaviour
    {
        [Header("Stats")]
        public int health;
        [Header("Values")]
        public float delta;
        public float horizontal;
        public float vertical;
        public float distance;

        //public CharacterStats characterStats;

        [Header("States")]
        public bool isInvisible;
        public bool dontDoAnything;
        public bool canMove;
        public bool isDead;
        public bool isrunning;

        public bool hasDestination;
        public Vector3 targetDestination;
        public Vector3 dirToTarget;
        public bool rotateToTarget;

        public LayerMask ignoreLayers;

        [Header("Referrence")]
        public Animator anim;
        Enemytarget enTarget;
        AIManager ai;
        Animationmanager a_manager;
        public Rigidbody rigid;
        public NavMeshAgent agent;
        RuntoAttack r;
        float timer;
        public void Init()
        {
            health = 100;
            anim = GetComponentInChildren<Animator>();
            enTarget = GetComponent<Enemytarget>();
            //enTarget.init(this);
            ai = GetComponent<AIManager>();
            rigid = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            rigid.isKinematic = true;

            a_manager = GetComponent<Animationmanager>();
            //a_manager.Init(null, this);


            ignoreLayers = ~(1 << 9);
        }

        public void Tick(float d)
        {
            delta = d;
            canMove = anim.GetBool(StaticStrings.onEmpty);
            distance = ai.distanceFromtarget();
            if (dontDoAnything)
            {
                dontDoAnything = !canMove;
                return;
            }

            if (rotateToTarget)
            {
                LookTowardsTarget();
            }
            if (isInvisible)
            {
                isInvisible = !canMove;
            }

        }

        void LookTowardsTarget()
        {
            Vector3 dir = dirToTarget;
            dir.y = 0;
            if (dir == Vector3.zero)
                dir = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, delta * 5);
        }

        public void WalkToTarget(Transform t, float w)
        {
            hasDestination = false;
            anim.Play("d|Idle");
            if (distance > 5 && canMove)
            {
                SetDestination(t.position, w);
            }

            if (distance < 5)
            {
                Debug.Log("stop");
                canMove = false;
                agent.isStopped = true;
            }
        }


        public void RunToTargetAndAttack(Vector3 lastpos, float w)
        {
            hasDestination = false;
            isrunning = true;
            anim.Play("d|Run");

            if (distance > 5 && canMove)
            {
                SetDestination(lastpos, w);
            }
            if (distance < 5)
            {
                canMove = false;
                agent.isStopped = true;
                anim.Play(ai.r.attackanim);
            }
        }

        public void SetDestination(Vector3 d, float movespeed)
        {
            if (!hasDestination)
            {

                hasDestination = true;
                agent.isStopped = false;
                agent.SetDestination(d);
                targetDestination = d;
                agent.speed = movespeed;
            }
        }

        public void CloseAttackaction(int attacknum)
        {
            if (canMove)
            {
                anim.Play(ai.c_attacks[attacknum].attackanim);
                agent.isStopped = true;
                rotateToTarget = false;
                anim.SetBool(StaticStrings.onEmpty, false);
                canMove = false;
            }

        }

    }

}