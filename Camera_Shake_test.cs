using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake_test : MonoBehaviour {
    
    public IEnumerator Shake(float duration,float magintude)
    {


        Vector3 originalPos = new Vector3(0, 2.84f, -10.78f);

        transform.localPosition = originalPos;
        float elapesd = 0.0f;

        while(elapesd <duration)
        {
            float x = Random.Range(-1f, 1f) * magintude;
            float y = Random.Range(-1, 1f) * magintude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapesd += Time.deltaTime;

            yield return null;
        }


        transform.localPosition = originalPos;
    }
	
}
