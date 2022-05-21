using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameInfo{

    //プレイヤーのジャンプ力
    public static readonly float PLAYER_JUMP = 2.7f;

    //プレイヤーの重力
    public static readonly float GRAVITY = -0.35f;   

    //プレイヤーの初期速度
    public static readonly float INITIAL_SPEED = 0.15f;

    //プレイヤーの最小速度
    public static readonly float MIN_SPEED = 0.1f;

    //プレイヤーの最大速度
    public static readonly float MAX_SPEED = 0.3f;
    
    //プレイヤーの最大プレゼント所持数
    public static readonly int MAX_HAVEPRESENT = 2;

    //コーラによる上昇スピード
    public static readonly float COLA_SPEED = 0.05f;

    //お茶による減少スピード
    public static readonly float GREENTEA_SPEED = -0.05f;

    //ゲームのプレイ時間
    public static readonly int GAME_TIME = 70;

    public static readonly float NonenaTime = 8.0f;

}

public static class PresentInfo{
    public enum Type{
        RED,
        BLUE,
        YELLOW,
        NONE
    }
    
}
public static class EnemyInfo{
    public enum Types{
        BIRD,
        CAT,
        NONE
    }
}
