using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    //Enemyの種類を管理する
    [SerializeField] EnemyInfo.Types EnemyType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public EnemyInfo.Types GetEnemyType(){

        return EnemyType;
    }



    


}
