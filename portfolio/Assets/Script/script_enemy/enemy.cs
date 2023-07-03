//敵のゲームオブジェクトにくっつけるスクリプト
//2023/06/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public en_attack_collider en_at_col;
    public player player_cl; //playerクラスを取得

    public float speed = 3.0f;
    public float attacked_end = 0.5f;
    public float charge_time=0.5f;
    public float attack_time=0.2f;


    private Animator anim;
    private Rigidbody2D rb;
    private GameObject attack_go;
    
    private float enemy_attacked_time = 0f; //敵のやられ状態持続時間
    private float enemy_charge_time = 0f;
    private float enemy_attack_time = 0f;

    private bool pl_enemy_attacked=false; //playerクラスからもってきた「敵のやられ状態」
    private bool on_at_col = false;
    private bool enemy_charge = false;
    private bool enemy_attack = false;
    private bool attack_trigger=true;

    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attack_go = GameObject.Find("enemy_attack_collider");

    }
    void Update()
    {
        pl_enemy_attacked = player_cl.enemy_attacked; //player側から「敵のやられ状態」をもってくる

        if (pl_enemy_attacked == true)//「敵のやられ状態」がtrueの時　
        {
            
            enemy_attacked_time += Time.deltaTime;
            if (enemy_attacked_time >= attacked_end)//敵のやられ状態をenemy_attacked_endで解除する
            {
                player_cl.enemy_attacked=false;
                enemy_attacked_time = 0f;
            }
        }
        rb.velocity = enemyMove();

        enemyAttack();
    }

    private Vector2 enemyMove()
    {
         pl_enemy_attacked = player_cl.enemy_attacked; //player側から「敵のやられ状態」をもってくる

        if (pl_enemy_attacked == true)//「敵のやられ状態」がtrueの時の処理
        {
            return new Vector2(7, rb.velocity.y);
        }
        else if (enemy_charge==true)
        {
            return new Vector2(0, rb.velocity.y);
        }
        else if (enemy_attack == true)
        {
            return new Vector2(0, rb.velocity.y);
        }
        else //敵が特殊な状態でない時の処理
        {
            return new Vector2(-2, rb.velocity.y);
        }
    }

    void enemyAttack()
    {
        if (attack_trigger == true && enemy_charge != true && enemy_attack != true)
        {
            enemy_charge = true;
        }

        if (enemy_charge == true)
        {
            anim.Play("enemy_charge");

            enemy_charge_time += Time.deltaTime;

            if (enemy_charge_time >= charge_time) //charge_timeに設定した秒数経過すると溜めモーション終了
            {
                enemy_charge_time = 0.0f;
                enemy_charge = false;
                enemy_attack = true;
            }
        }

        if (enemy_attack == true) //敵の攻撃状態がtrueの時
        {
            anim.Play("enemy_attack");
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);//攻撃判定の絵を表示



            enemy_attack_time += Time.deltaTime;

            on_at_col = en_at_col.on_attack_collider;
            if (on_at_col == true) //敵の攻撃判定内にプレイヤーが入っていたらプレイヤーのやられ状態をtrueにする
            {
                player_cl.player_attacked = true;
            }

            if (enemy_attack_time >= attack_time) //attack_timeに設定した秒数経過すると攻撃モーション終了
            {
                enemy_attack_time = 0.0f;
                enemy_attack = false;
                anim.Play("enemy_stand");
                attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//攻撃判定の絵を非表示

            }

        }
    }
}
