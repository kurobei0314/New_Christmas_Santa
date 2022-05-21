using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class present : MonoBehaviour
{

    PresentInfo.Type currentPresentType;
    
    // 移動する時の移動場所
    [SerializeField] GameObject moveposition;

    //動くだけのプレセント
    [SerializeField] GameObject movepresent;
    
    // Start is called before the first frame update
    void Start()
    {
        //movepresent.SetActive(false);
        InitializeType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    void OnTriggerEnter2D(Collider2D col){

        if(col.gameObject.tag == "Player"){
            this.gameObject.SetActive(false);
            //StartCoroutine ("PresentMoveAnimation");
        }

    }
    */

    void InitializeType(){

        switch(this.gameObject.tag){
            case "P_red":
                currentPresentType = PresentInfo.Type.RED;
                break;
            case "P_yellow":
                currentPresentType = PresentInfo.Type.YELLOW;
                break;
            case "P_blue":
                currentPresentType = PresentInfo.Type.BLUE;
                break;
        }
    }

/*
    IEnumerator PresentMoveAnimation () {

        
        movepresent.transform.position = this.gameObject.transform.position;
        //movepresent.SetActive(true);
        this.gameObject.SetActive(false);
    
        RectTransform PresentTrans = movepresent.GetComponent<RectTransform>();
        
        PresentTrans.DOMove (
            moveposition.transform.position + new Vector3(1.0f*),   //終了時点のScale
            1.0f                                                    //時間
        );

        yield return new WaitForSeconds (1.0f);

        //movepresent.SetActive(false);
        
    }
    */


    public PresentInfo.Type GetPresentType(){

        return currentPresentType;
    }
   
}
