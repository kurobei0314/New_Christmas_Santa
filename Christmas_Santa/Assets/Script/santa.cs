using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using DG.Tweening;

public class santa : MonoBehaviour
{
    // プレイヤーの横幅、縦幅
    float width, height;
    float speed=GameInfo.INITIAL_SPEED;

    //プレイヤーの状態を管理する
    public enum PlayerState{
        NORMAL,
        JUMP,
        FALL,
    }

    public PlayerState currentPlayerState;
    public Property HavePresent;
    // [SerializeField] private Sprite[] PlayerSprites;
    [SerializeField] private GameObject[] SantaImage; 
    int PlayerSpriteFlg = 1;
    EnemyInfo.Types EnemyAttack = EnemyInfo.Types.NONE;

    public GameController Controller;

    // Start is called before the first frame update
    void Awake()
    {
       width  = GetComponent<Renderer>().bounds.size.x;
       height = GetComponent<Renderer>().bounds.size.y; 
       SetCurrentPlayerState (PlayerState.NORMAL);

       Observable.Interval(TimeSpan.FromSeconds(0.4f)).Subscribe(_ =>
        {
            ChangePlayerImage();    
        }).AddTo(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentPlayerState);
    }

    //プレイヤーの右下の座標を取得する
    public Vector3 Get_UnderRightPosition(){

        Vector3 UnderRightPosition = transform.position; 
        UnderRightPosition -= new Vector3 (width/2, height/2,0);
        return UnderRightPosition; 
    }

    //プレイヤーの左上の座標を取得する
    public Vector3 Get_UpLeftPosition(){

        Vector3 UpLeftPosition = transform.position; 
        UpLeftPosition += new Vector3 (width/2, height/2,0);
        return UpLeftPosition; 
    }

    // プレイヤーのスピードを取得する
    public float Get_PlayerSpeed(){
        return speed;
    }

    // プレイヤーの画像を変える
    void ChangePlayerImage(){

        if(EnemyAttack == EnemyInfo.Types.NONE  && currentPlayerState == PlayerState.NORMAL){
            PlayerSpriteFlg = 1 - PlayerSpriteFlg;
            SwitchSantaImage(PlayerSpriteFlg);
        }
    }

    // 衝突判定まわり
    void OnCollisionEnter2D(Collision2D col)
    {
            //欲しがってる人と触れた時の処理
            if(col.gameObject.tag == "chimney"){

                //煙突に１度も入っていなかった時
                if(col.gameObject.GetComponent<chimney>().JudgePresentAccept()){

                    HavePresent.FindHavePresent(col.gameObject.GetComponent<chimney>().GetPresentType());
                    col.gameObject.GetComponent<chimney>().WantPresentActive();
                }

            }
            //鳥によって落ちてるときに地面に触れたとき
            if(EnemyAttack == EnemyInfo.Types.BIRD) SetEnemyAttack(EnemyInfo.Types.NONE);

    }
    

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "roof"   ||
            col.gameObject.tag == "chimney" )
            {
                SetCurrentPlayerState(PlayerState.NORMAL);
            }
    }
    
    void OnCollisionExit2D(Collision2D col)
    {
        //Debug.Log("Exit");
            if (col.gameObject.tag == "roof")
            {
                SetCurrentPlayerState(PlayerState.JUMP);
            }
    }
    
    void OnTriggerEnter2D(Collider2D col){

        //プレゼントと触れた時の処理 
        if(col.gameObject.tag == "P_red"    ||
           col.gameObject.tag == "P_yellow" ||
           col.gameObject.tag == "P_blue"   ){
            
            col.gameObject.SetActive(false);
            HavePresent.SetHavePresent(col.gameObject.GetComponent<present>().GetPresentType());
            AudioManager.Instance.PlaySE("GetPresent");
            //StartCoroutine ("PresentMoveAnimation");
        }

        //スピード変換アイテムと触れた時の処理
        if(col.gameObject.tag == "Item"){

            col.gameObject.SetActive(false);
            if (col.gameObject.GetComponent<item>().JudgeNonena()){
                StartCoroutine ("ChangeNonenaSpeed");
            }
            else{
                ChangePlayerSpeed(col.gameObject.GetComponent<item>().ChangeSantaSpeedAmount());
            }
        }

        //敵と触れた時の処理
        if(col.gameObject.tag == "Enemy"){

            col.gameObject.SetActive(false);
            AudioManager.Instance.PlaySE("Hit06-1");
            TouchEnemy(col.gameObject.GetComponent<enemy>().GetEnemyType());
        }

        //落ちた時
        if(col.gameObject.tag == "underCollider"){

            AudioManager.Instance.PlaySE("Fall");
            //落ちた時のSE
            Controller.SetCurrentGameState(GameController.GameState.GAMEOVER);

        }

        //コースを完走した時
        if(col.gameObject.tag == "Goal"){

            //落ちた時のSE
            Controller.SetCurrentGameState(GameController.GameState.GAMEOVER);

        }
    }

    IEnumerator ChangeNonenaSpeed(){

        speed = GameInfo.MAX_SPEED;
        yield return new WaitForSeconds (GameInfo.NonenaTime);
        AudioManager.Instance.PlaySE("PowerDown");
        speed = GameInfo.MIN_SPEED;
    }

    // プレイヤーの状態を変える
    public void ChangeCurrentPlayerState () {
        switch (currentPlayerState) {
            case PlayerState.NORMAL:
                currentPlayerState = PlayerState.JUMP;
                break;
            case PlayerState.JUMP:
                break;
        }
    }

    // ゲームの状態をセットする
    public void SetCurrentPlayerState (PlayerState state) {
        currentPlayerState = state;
    }

    // ゲームの状態をセットする
    public void SetEnemyAttack (EnemyInfo.Types state) {
        EnemyAttack = state;
    }

    //playerが地面に着いているかどうか判定
    public bool LandRoof(){
        if(currentPlayerState == PlayerState.NORMAL){
            return true;
        }
        else{
            return false;
        }
    }

    public EnemyInfo.Types GetEnemyAttack(){

        return EnemyAttack;
    }

    void ChangePlayerSpeed(float ChangeSpeed){

        if( !(speed + ChangeSpeed < GameInfo.MIN_SPEED || speed + ChangeSpeed > GameInfo.MAX_SPEED) ){
            speed += ChangeSpeed;
        }
        else if (speed + ChangeSpeed < GameInfo.MIN_SPEED ){
            speed = GameInfo.MIN_SPEED;
        }
        else if (speed + ChangeSpeed > GameInfo.MAX_SPEED){
            speed = GameInfo.MAX_SPEED;
        }
    }
    
    void TouchEnemy(EnemyInfo.Types type){

        switch(type){

            case EnemyInfo.Types.BIRD:
                SetEnemyAttack(EnemyInfo.Types.BIRD);
                SwitchSantaImage(2);
                // GameControllerにて移動を制御してる
                break;

            case EnemyInfo.Types.CAT:
                SetEnemyAttack(EnemyInfo.Types.CAT);
                StartCoroutine ("TouchCat");
                break;
        }
    }

    IEnumerator TouchCat(){
        SwitchSantaImage(2);

        transform.DOJump(
            transform.position + new Vector3(2.0f,0.0f,0.0f),      // 移動終了地点
            10,               // ジャンプする力
            1,               // ジャンプする回数
            1.0f              // アニメーション時間
        );

        yield return new WaitForSeconds (1.0f);

        SwitchSantaImage(PlayerSpriteFlg);
        SetEnemyAttack(EnemyInfo.Types.NONE);        

    }

    // サンタの画像を切り替える。引数に表示したいサンタの画像のナンバーを入れる
    void SwitchSantaImage(int num){

        for(int i=0; i<3;i++){
            SantaImage[i].SetActive (false);
        }
        SantaImage[num].SetActive(true);
    }
}
