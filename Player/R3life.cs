using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class R3life : MonoBehaviour {
    /*参考資料:Unity5 ゲーム開発レシピ P179　夢見がちゲーミング[http://gameyoshiki.blog.fc2.com/blog-entry-28.html]*/
   

    public int armorpoint=100;
    public int armorpointmax=100;
    void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
       
        armorpoint = armorpointmax;
	}

   

   void OntriggerEnter(CapsuleCollider collider) {
   if(collider.gameObject.tag =="Enemy") {

            armorpoint -= 10;

   }


   }
}
