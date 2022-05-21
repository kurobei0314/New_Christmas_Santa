using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    public GameObject Present_R;
    public GameObject[] mokumoku;
    public GameObject text;
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("Result");
        InitializeActive();
        InitializeText();
        StartCoroutine ("ResultAnimation");  
        InitializeButton();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeActive(){
        Present_R.SetActive(true);
        text.SetActive(false);
        button.SetActive(false);

        for (int i=0 ; i < 3 ;i++){
            mokumoku[i].SetActive(false);
        }
    }

    void InitializeButton(){
        GameObject TitleButton = button.transform.Find("title").gameObject;
        GameObject MainButton= button.transform.Find("main").gameObject;
        GameObject RankingButton= button.transform.Find("ranking").gameObject;
        GameObject TweetButton= button.transform.Find("tweet").gameObject;
        
        TitleButton.GetComponent<Button>().onClick.AddListener (TitleClick);
        MainButton.GetComponent<Button>().onClick.AddListener (MainClick);
        RankingButton.GetComponent<Button>().onClick.AddListener (RankingClick);
        TweetButton.GetComponent<Button>().onClick.AddListener (TweetClick);
    }

    void InitializeText(){

        GameObject ScoreText = text.transform.Find("score").gameObject;
        GameObject AllScoreText = text.transform.Find("allscore").gameObject;

        int distance = ScoreManager.instance.score - 100 * ScoreManager.instance.GetPresent;
        string TextContext = "100 × "+ ScoreManager.instance.GetPresent + " + " + distance;

        ScoreText.GetComponent<Text>().text = TextContext;
        AllScoreText.GetComponent<Text>().text = ScoreManager.instance.score + "てん";

    }


    private IEnumerator ResultAnimation() {
        
        yield return new WaitForSeconds (1.3f);

        Sprite sprite = Resources.Load<Sprite>("Image/OpenPresent_R");
        Present_R.GetComponent<Image>().sprite = sprite;
        
        for (int i = 0 ; i < 3; i++){
            yield return new WaitForSeconds (0.3f);
            mokumoku[i].SetActive(true);
        }

        text.SetActive(true);
        button.SetActive(true);
    }

    public void TitleClick(){
        AudioManager.Instance.PlaySE("Button");
        AudioManager.Instance.StopBGM();
        FadeManager.Instance.LoadScene ("Title", 1.0f);
    }

    public void RankingClick(){
        AudioManager.Instance.PlaySE("Button");
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (ScoreManager.instance.score);
    }

    public void TweetClick(){

        AudioManager.Instance.PlaySE("Button");
        string text = "届けたプレゼントは"+ScoreManager.instance.GetPresent+"個、合計点は"+ScoreManager.instance.score+"てんとったよ！"; 
        naichilab.UnityRoomTweet.Tweet ("christmas_santa_run", text, "unityroom", "unity1week");

    }

    public void MainClick(){

        AudioManager.Instance.PlaySE("Button");
        FadeManager.Instance.LoadScene ("Main", 1.0f);
    }    
}
