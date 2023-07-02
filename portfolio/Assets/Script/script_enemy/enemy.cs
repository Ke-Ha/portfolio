//敵のゲームオブジェクトにくっつけるスクリプト
//2023/06/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float enemy_atacked_end = 0.5f;

    private Rigidbody2D rb;
    public player player_cl; //playerクラスを取得

    private bool pl_enemy_atacked=false; //playerクラスからもってきた「敵のやられ状態」
    private float enemy_atacked_time = 0f; //敵のやられ状態持続時間
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        pl_enemy_atacked = player_cl.enemy_atacked; //player側から「敵のやられ状態」をもってくる

        if (pl_enemy_atacked == true)//「敵のやられ状態」がtrueの時　
        {
            enemy_atacked_time += Time.deltaTime;
            if (enemy_atacked_time >= enemy_atacked_end)//敵のやられ状態をenemy_atacked_endで解除する
            {
                player_cl.enemy_atacked=false;
                enemy_atacked_time = 0f;
            }
        }
        rb.velocity = enemyMove();
    }

    private Vector2 enemyMove()
    {
         pl_enemy_atacked = player_cl.enemy_atacked; //player側から「敵のやられ状態」をもってくる

        if (pl_enemy_atacked == true)//「敵のやられ状態」がtrueの時の処理
        {
            return new Vector2(7, rb.velocity.y);
        }
        else //敵が特殊な状態でない時の処理
        {
            return new Vector2(0, rb.velocity.y);
        }
    }
}
