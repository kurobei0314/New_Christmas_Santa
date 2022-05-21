using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject touchpanel;
    [SerializeField] Camera camera;

    Rigidbody2D rigidbody;
    GameObject[] HavePresent;

    [SerializeField] GameObject[] PropertyPresent;   

    //ゲームの状態を管理する
    public enum GameState{
        COUNTDOWN,
        MAIN,
        GAMEOVER
    }

    public GameState currentGameState;

    //時間を表示するText型の変数
    public Text timeText;
    private float GameTimes = GameInfo.GAME_TIME;
    // 残り時間3秒のflag
    private int count3 = 3;

    // 最初のポジションを保管しておく
    private Vector3 InitialPosition;

    // 1回だけスコアを加算する
    bool ScoreFlg=true;

    // Start is called before the first frame update
    void Start()
    {
        touchpanel.GetComponent<Button>().onClick.AddListener (Click_touchpanel);
        SetCurrentGameState(GameState.COUNTDOWN);
        ScoreManager.instance.score = 0;
        ScoreManager.instance.GetPresent = 0;
        InitialPosition = player.transform.position;

        AudioManager.Instance.PlayBGM("Main");
    }

    // Update is called once per frame
    void Update()
    {
        if     (currentGameState == GameState.COUNTDOWN){
            StartCoroutine ("MainAnimation");
        }
        else if(currentGameState == GameState.MAIN){
            
            PlayerControll();
            GameTimeCounter();
        }

        else if(currentGameState == GameState.GAMEOVER){

            if(ScoreFlg){
                ScoreManager.instance.score += (int)(player.transform.position.x - InitialPosition.x);
                ScoreFlg = false;
            }
            AudioManager.Instance.StopBGM();
            FadeManager.Instance.LoadScene ("Result", 1.0f);

        }
        
    
    }

    //持っているプレゼントの初期化
    void HavePresentInitialize(){

        HavePresent = new GameObject[3];

        for(int i=0; i<3; i++){
            HavePresent[i] =  PropertyPresent[i];
        }
        
    }

    // プレイヤーの移動
    public void PlayerControll(){

        //Debug.Log(player.GetComponent<santa>().JudgeTouchEnemy());
        EnemyInfo.Types AttackEnemyType = player.GetComponent<santa>().GetEnemyAttack();
        rigidbody = player.GetComponent<Rigidbody2D>();

        //敵と触れているかどうか
        if(     AttackEnemyType == EnemyInfo.Types.NONE){
            float PlayerSpeed = player.GetComponent<santa>().Get_PlayerSpeed();
            
            rigidbody.MovePosition( player.transform.position + new Vector3(PlayerSpeed, GameInfo.GRAVITY,0.0f) );
        }
        else if(AttackEnemyType == EnemyInfo.Types.BIRD){
            rigidbody.MovePosition( player.transform.position + new Vector3(0.0f, GameInfo.GRAVITY,0.0f) );
        }
        else if(AttackEnemyType == EnemyInfo.Types.CAT){
            rigidbody.MovePosition( player.transform.position + new Vector3(0.0f, 0.0f,0.0f) );
        }

         // プレイヤーが落ちているかどうかを確認する
        /*
        Vector3 PlayerUpLeftPosition = player.GetComponent<santa>().Get_UpLeftPosition();
        
        if(camera.transform.position.y - 7.5f > PlayerUpLeftPosition.y){
            // SetCurrentGameState(GameState.GAMEOVER);
        }
        */
    }
    

    // ゲームの状態をセットする
    public void SetCurrentGameState (GameState state) {
        currentGameState = state;
    }

    //画面をクリックした時の処理
    void Click_touchpanel(){

        if( player.GetComponent<santa>().GetEnemyAttack() == EnemyInfo.Types.NONE && 
            currentGameState == GameState.MAIN){

            if(player.GetComponent<santa>().LandRoof()){
                rigidbody = player.GetComponent<Rigidbody2D>();
                rigidbody.AddForce(Vector2.up * GameInfo.PLAYER_JUMP);

                AudioManager.Instance.PlaySE("Jump");
            }
        }
    }

    void GameTimeCounter(){

        //時間をカウントする
        GameTimes = TimeCounter(GameTimes);

        //時間を表示する
        timeText.text = ((int)GameTimes).ToString();

        //3秒前の音
        if( 0 < GameTimes && GameTimes < 4){
            if ((int)GameTimes <= count3 && count3 < (int)GameTimes+1){
                count3--;
                AudioManager.Instance.PlaySE("Count");
            }
        }

        if(GameTimes < 0) SetCurrentGameState(GameState.GAMEOVER);
    }

    float TimeCounter(float time){

        time -= Time.deltaTime;
        return time;
    }

    private IEnumerator MainAnimation() {

        yield return new WaitForSeconds (1.0f);
        SetCurrentGameState(GameState.MAIN);
    }

}
