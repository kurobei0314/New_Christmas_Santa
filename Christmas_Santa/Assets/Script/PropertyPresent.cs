using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyPresent : MonoBehaviour
{
    PresentInfo.Type currentPresentType;
    Image ImageColor;

    [SerializeField] private Sprite[] PresentImageSprites;

    void Awake(){

        ImageColor = this.gameObject.GetComponent<Image>();
        SetPresentType(PresentInfo.Type.NONE);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPresentType(PresentInfo.Type type){

        switch(type){

            case PresentInfo.Type.NONE:
                currentPresentType = PresentInfo.Type.NONE;
                ImageColor.sprite = null;
                ImageColor.color = Color.gray;
                break;
            case PresentInfo.Type.RED:
                currentPresentType = PresentInfo.Type.RED;
                ImageColor.color = new Color(1,1,1,1);
                ImageColor.sprite = PresentImageSprites[0];
                break;
            case PresentInfo.Type.BLUE:
                currentPresentType = PresentInfo.Type.BLUE;
                ImageColor.color = new Color(1,1,1,1);
                ImageColor.sprite = PresentImageSprites[1];
                break;
            case PresentInfo.Type.YELLOW:
                currentPresentType = PresentInfo.Type.YELLOW;
                ImageColor.color = new Color(1,1,1,1);
                ImageColor.sprite = PresentImageSprites[2];
                break;
        }

    }

    public PresentInfo.Type GetPresentType(){
        return currentPresentType;
    }
}
