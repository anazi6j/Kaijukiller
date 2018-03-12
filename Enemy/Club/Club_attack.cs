using UnityEngine;
using System.Collections;

public class Club_attack : MonoBehaviour {
    public GameObject tacklecollider;
    public GameObject hammmerattackcollider;
    public GameObject DenpsyL;
    public GameObject DenpsyR;
    public float attackpower=10f;
    void Start()
    {
        tacklecollider.SetActive(false);
        hammmerattackcollider.SetActive(false);
        DenpsyL.SetActive(false);
        DenpsyR.SetActive(false);

    }


   void tackleon()
    {
        tacklecollider.SetActive(true);
    }

    void takcleoff()
    {
        tacklecollider.SetActive(false);
    }

    void hammeron()
    {
        hammmerattackcollider.SetActive(true);
    }

    void hammeroff()
    {
        hammmerattackcollider.SetActive(false);
    }

    void denpsyLon()
    {
        DenpsyL.SetActive(true);
    }

    void denpsyLoff()
    {
        DenpsyL.SetActive(false);
    }
    void denpsyRon()
    {
        DenpsyR.SetActive(true);
    }
    void denpsyRoff()
    {
        DenpsyR.SetActive(false);
    }
}
