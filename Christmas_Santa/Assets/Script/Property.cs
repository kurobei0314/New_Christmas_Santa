using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
{
    //[SerializeField] SpriteRenderer[] HavePresent;
    public PropertyPresent[] HavePresent;

    // Start is called before the first frame update
    void Start()
    {
        HavePresentInitialize(); 
    }

    // Update is called once per frame
    void Update()
    {
    
    }


    //持っているプレゼントの初期化
    /*
    void HavePresentInitialize(){

        HavePresent = new GameObject[3];

        for(int i=0; i<3; i++){
            HavePresent[i] =  PropertyPresent[i];
        }
        
    }
    */
    void HavePresentInitialize(){

        for(int i=0; i < GameInfo.MAX_HAVEPRESENT; i++){

            HavePresent[i].SetPresentType(PresentInfo.Type.NONE);
            //HavePresent[i].GetComponent<PropertyPresent>().SetPresentType(PresentInfo.Type.NONE);
        }
    } 

    public void SetHavePresent(PresentInfo.Type type){

        for(int i=0; i < GameInfo.MAX_HAVEPRESENT; i++){

            //所持できるプレゼントに空きがあったら
            if(HavePresent[i].GetPresentType() == PresentInfo.Type.NONE){
                HavePresent[i].SetPresentType(type);
                return;
            }
        }
    }

    // 欲しい人にプレゼントを持って行ったときに欲しい色があるかどうか見つける処理
    public void FindHavePresent(PresentInfo.Type type){

        for(int i=0; i < GameInfo.MAX_HAVEPRESENT; i++){

            //もしもみつけたら
            if(HavePresent[i].GetPresentType() == type){
                HavePresent[i].SetPresentType(PresentInfo.Type.NONE);
                //スコアをプラスする
                ScoreManager.instance.score += 100;
                ScoreManager.instance.GetPresent += 1;
                //音をつける
                AudioManager.Instance.PlaySE("SuccessPresent");
                return;
            }
        }
        //みつかんなかったらなにもしない

        //音をつける
        AudioManager.Instance.PlaySE("FailPresent");
    }

}
