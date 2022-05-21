using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class TitleController : MonoBehaviour
{

    public GameObject Santa;
    public GameObject button;
    public GameObject EndPosition;
    public GameObject Text;

    public GameObject MainTutolial;
    public GameObject[] Tutolial;
    public GameObject Batsu;
    public GameObject RightArrow, LeftArrow;

    int TutorialIndex;


    // Start is called before the first frame update
    void Start()
    {
        InitializeActive();
        StartCoroutine ("TitleAnimation");
        InitializeButton();

        RightArrow.GetComponent<Button>().onClick.AddListener (RightArrow_Touch);
        LeftArrow.GetComponent<Button>().onClick.AddListener (LeftArrow_Touch);
        Batsu.GetComponent<Button>().onClick.AddListener (Batsu_Touch);

        AudioManager.Instance.PlayBGM("Title");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeActive(){

        button.SetActive(false);

        Text Text1 = Text.transform.Find("Text1").gameObject.GetComponent<Text>();
        Text Text2 = Text.transform.Find("Text2").gameObject.GetComponent<Text>();
        Text1.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        Text2.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        TutorialIndex = 0;

        MainTutolial.SetActive(false);
        Tutolial[0].SetActive(true);

        for(int i = 1; i < 4 ; i++){
            Tutolial[i].SetActive(false);
        }
    }


    void InitializeButton(){
        GameObject StartButton = button.transform.Find("GameStart").gameObject;
        GameObject TutolialButton= button.transform.Find("Tutolial").gameObject;
        
        StartButton.GetComponent<Button>().onClick.AddListener (StartClick);
        TutolialButton.GetComponent<Button>().onClick.AddListener (TutolialClick);
    }

    void StartClick(){
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlaySE("Button");
        FadeManager.Instance.LoadScene ("Main", 1.0f);
    }

    void TutolialClick(){
        MainTutolial.SetActive(true);
        AudioManager.Instance.PlaySE("Button");
    }

    private IEnumerator TitleAnimation() {

        yield return new WaitForSeconds (1.0f);

        Santa.transform.DOMove (
            EndPosition.transform.position,  //移動後の座標
            1.0f                             //時間
        );
        yield return new WaitForSeconds (1.0f);

        Text Text1 = Text.transform.Find("Text1").gameObject.GetComponent<Text>();
        Text Text2 = Text.transform.Find("Text2").gameObject.GetComponent<Text>();

        DOTween.ToAlpha(
            () => Text1.color, 
            color => Text1.color = color,
            1.0f,                                
            1.0f
        );

        DOTween.ToAlpha(
            () => Text2.color, 
            color => Text2.color = color,
            1.0f,                                
            1.0f
        );

        yield return new WaitForSeconds (1.0f);
        button.SetActive(true);
    }

    void RightArrow_Touch(){
        
        AudioManager.Instance.PlaySE("Button");
        Tutolial[TutorialIndex].SetActive(false);

        if(TutorialIndex == 3) {
            TutorialIndex = 0;
        }
        else{
            TutorialIndex += 1;
        }

        Tutolial[TutorialIndex].SetActive(true);
    }

    void LeftArrow_Touch(){

        AudioManager.Instance.PlaySE("Button");
        Tutolial[TutorialIndex].SetActive(false);

        if(TutorialIndex == 0) {
            TutorialIndex = 3;
        }
        else{
            TutorialIndex -= 1;
        }

        Tutolial[TutorialIndex].SetActive(true);
    }

    void Batsu_Touch(){
        AudioManager.Instance.PlaySE("Button");
        MainTutolial.SetActive(false);
    }

}
