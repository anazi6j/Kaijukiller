using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCopy_Test : MonoBehaviour
{
    public EnemyParamTable epm;
    
    public List<CloseAttacks> C= new List<CloseAttacks>();
    // Start is called before the first frame update
    void Start()
    {
        
       
            List<CloseAttacks> closes = new List<CloseAttacks>();

            closes[0].attackanim = epm.closeAttacks[0].attackanim;
            closes[0].attackpower = epm.closeAttacks[0].attackpower;
            closes[0].attackstrength = epm.closeAttacks[0].attackstrength;
            closes[0].intervaltime = epm.closeAttacks[0].intervaltime;
            C.Add(closes[0]);
            Debug.Log("テストコピー開始");

        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
