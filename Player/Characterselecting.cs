using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Characterselecting : MonoBehaviour {

    private GameObject[] characterlist;
    public int index;
   static string setname;
    void Start() {
        index = PlayerPrefs.GetInt("CharacterSelected");//値を取得する。存在しない場合は0を返す。
        characterlist = new GameObject[transform.childCount];//characterlistはtransformの持つ子の数だけ生成する

        for(int i =0; i<transform.childCount; i++) {
            characterlist[i] = transform.GetChild(i).gameObject;//transform内の子オブジェクトを取得
        }
        foreach (GameObject go in characterlist)
            go.SetActive(false);//characterlist内のゲームオブジェクトgoを探す

        if (characterlist[index])
            characterlist[index].SetActive(true);
            }
          
  

  public void ToggleLeft() 
  {
        
  //現在のモデルから別のモデルに切り替える
        characterlist[index].SetActive(false);//index番目のcharacterを非表示にする
        index--;
        if (index < 0)
            index = characterlist.Length - 1;
            //別のモデルにする
        characterlist[index].SetActive(true);//index番目のcharacterを表示する
  }

    public void ToggleRight()
    {

        //現在のモデルから別のモデルに切り替える
        characterlist[index].SetActive(false);
        index++;
        if (index == characterlist.Length)
            index = 0;
            characterlist[index].SetActive(true);
    }
    
    public void Confirm(string loadname)
    {
        setname = loadname;
        PlayerPrefs.SetInt("CharacterSelected",index);//index番目のキャラクターに設定する。
        SceneManager.LoadScene("mainmenu");
    }
}
