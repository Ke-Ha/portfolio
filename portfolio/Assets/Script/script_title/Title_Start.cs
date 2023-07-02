//タイトルシーンの制御スクリプト
//2023/6/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Start : MonoBehaviour
{
    private bool firstpush=false; //シーンチェンジを必ず1回だけ行うための変数


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKey && firstpush==false) //シーンチェンジを必ず一回だけ行う処理
        {
            firstpush = true;
        }

        if (firstpush==true)
        {
            SceneManager.LoadScene("Stage1");
        }
    }
}
