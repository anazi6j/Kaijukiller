using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot { 
public class AnimationCopy_Test : MonoBehaviour
{
    public EnemyParamTable epm;

    public List<CloseAttacks> C;
    // Start is called before the first frame update
    void Start()
    {


            C = new List<CloseAttacks>();

            for (int i = 0; i < epm.closeAttacks.Count; i++)
            {
                C.Add(new CloseAttacks(epm.closeAttacks[i].attackanim, epm.closeAttacks[i].attackpower, epm.closeAttacks[i].attackstrength,
              epm.closeAttacks[i].intervaltime, epm.closeAttacks[i].canbeParried, epm.closeAttacks[0].colliders));
            }





        }
        

        


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
}
