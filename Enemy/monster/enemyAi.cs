using UnityEngine;
using System.Collections;

public class enemyAi : MonoBehaviour {
    C1Stats enstats;
    UnityEngine.AI.NavMeshAgent agent;
    Animator anim;
    public Transform curtarget;
    public float attackRange = 5;
    public bool currentlyAttacking;
    public float attackRate = 2;
    float attTimer;
    bool attackonce;
    bool stopRotating;
    public float attackcurve;

    public GameObject damageCollider;

    // Use this for initialization
    void Start() {
        enstats = GetComponent<C1Stats>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetupAnimator();
        agent.stoppingDistance = attackRange; }

    // Update is called once per frame
    void Update() {
        if (!enstats.dead) {
            if (!currentlyAttacking)
            {
                movementHandler();
            }
            AttackHandler();

        }
    }
    void movementHandler() {
        if (curtarget != null)
        {
            agent.SetDestination(curtarget.position);
            Vector3 relDirection = transform.InverseTransformDirection(agent.desiredVelocity); //http://magcat.php.xdomain.jp/brog/unity%E3%81%AEinversetransformdirection%E3%81%AB%E3%81%A4%E3%81%84%E3%81%A6-418.html
            anim.SetFloat("movement", relDirection.z, 0.5f, Time.deltaTime);

            float distance = Vector3.Distance(transform.position, curtarget.position);
            if (distance <= attackRange)
            {
                attTimer += Time.deltaTime;

                if (attTimer > attackRate) {
                    currentlyAttacking = true;
                    attTimer = 0;
                }
            }


        }

    }


    void SetupAnimator() {
        //これはroot上のアニメーターコンポを参照するということ
        anim = GetComponent<Animator>();
        //子のアニメーターコンポーネントを使う（存在する場合）
        //キャラクターモデルを子のノードとしてあつかえるようにする

        foreach (var childanimator in GetComponentsInChildren<Animator>())
        {
            if (childanimator != anim) {
                anim.avatar = childanimator.avatar;
                Destroy(childanimator);
                break;//最初のアニメーターを見つけた場合、探すのをやめる
            }
        }
    }

    void AttackHandler() {
        if (currentlyAttacking) {
            if (!stopRotating) {
                Vector3 dir = curtarget.position - transform.position;
                Quaternion targetrot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetrot, Time.deltaTime * 5); //http://gamesonytablet.blogspot.jp/2013/03/ai3.html(回転をスムーズにする）
                float angle = Vector3.Angle(transform.forward, dir);

                if (angle < 5)
                {
                    if (!attackonce) {
                        agent.Stop();
                        anim.SetBool("Attack", true);
                        StartCoroutine("CloseAttack");
                        attackonce = true;

                    }
                }

            }
        }
        attackcurve = anim.GetFloat("attackcurve");//アニメーターのパラメーター"attackcurve"の値を参照し取得する
        if (attackcurve > .05f)//このfってどんな意味だ
        {
            stopRotating = true;

            if (attackcurve > .07f)//ここの98fはチュートリアル上の都合上
            {
                damageCollider.SetActive(true);
            }else
            {
            if(damageCollider.activeInHierarchy)
            {
                    damageCollider.SetActive(false);
            }
            }
        }
    }

    IEnumerator CloseAttack() {
        yield return new WaitForSeconds(4);
        anim.SetBool("Attack", false);
        attackonce = false;
        agent.Resume();
        currentlyAttacking = false;
        stopRotating = false;
    }
}

