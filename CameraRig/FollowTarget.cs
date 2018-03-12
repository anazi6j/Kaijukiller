using UnityEngine;

public  abstract class FollowTarget : MonoBehaviour {
    [SerializeField]public Transform target;
    [SerializeField]private bool autotargetPlayer = true;


    // Use this for initialization
    virtual protected void Start() {
        if (autotargetPlayer)
        {
            FindTargetPlayer();
        }
    }



    // Update is called once per frame
    void FixedUpdate() {
      
   
      if (autotargetPlayer && (target == null || !target.gameObject.activeSelf))
        {
            FindTargetPlayer();
        }
        if (target != null)
        {
            Follow(Time.deltaTime);
        }
    }
  protected  abstract void Follow(float deltaTime);
    //なぜこうなった

    public void FindTargetPlayer()
     {

    if(target == null) {
            GameObject targetObj = GameObject.FindGameObjectWithTag("Player");
            if(targetObj) {
                SetTarget(targetObj.transform);
            }
    }

    }

    public virtual void SetTarget(Transform newTransform) {
        target = newTransform;
    }
   
}
