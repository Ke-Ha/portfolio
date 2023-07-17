//敵のゲームオブジェクトにくっつけるスクリプト
//2023/06/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public attack_collider at_col;
    public player player_cl; //playerクラスを取得
    public fall fall_cl;

    public bool enemy_attacked=false; //今やられ状態かどうか

    public float speed = 3.0f;
    public float attacked_end = 0.5f; //やられ状態終了時間
    public float charge_time=0.5f; //タメ時間
    public float attack_time=0.2f; //攻撃モーション時間
    public float attack_cooldown = 1.0f; //攻撃クールダウン時間


    private Animator anim;
    private Rigidbody2D rb;
    private GameObject attack_go; //攻撃判定のゲームオブジェクト
    private GameObject player_go;

    private Vector3 player_pos;
    private Vector3 enemy_pos;
    
    private float enemy_attacked_time = 0f; 
    private float enemy_charge_time = 0f;
    private float enemy_attack_time = 0f;
    private float enemy_attack_cooldown = 0.0f;
    private float move_switch_time = 0.0f; //移動切り替えのクールタイム
    private float p_to_e; //プレイヤーと敵の距離
    private float move_dir = 0f; //敵の移動方向（1で→、0で停止、-1で←）
    private int rand; //乱数を入れる用

    private bool on_at_col = false; //攻撃判定に敵が入っているかどうか
    private bool enemy_charge = false; //今タメ状態かどうか
    private bool enemy_attack = false; //今攻撃状態かどうか
    private bool attack_trigger; //このトリガーがtrueになったら攻撃する


    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attack_go = transform.GetChild(0).gameObject;
        player_go = GameObject.Find("player");

    }
    void Update()
    {
        if(fall_cl.playerwin!=true && fall_cl.enemywin!=true){
            if (enemy_attacked == true)//「敵のやられ状態」がtrueの時　
            {
                anim.Play("enemy_attacked");
                enemy_attacked_time += Time.deltaTime;
                if (enemy_attacked_time >= attacked_end)//敵のやられ状態を一定時間経過で解除する
                {
                    enemy_attacked = false;
                    enemy_attacked_time = 0f;
                    anim.Play("enemy_stand");
                }
            }
            rb.velocity = enemyMove(); 

            enemy_attack_cooldown += Time.deltaTime;
            if (enemy_attack_cooldown >= attack_cooldown)
            {
                enemyAttack();
            }
        }
        else //どちらかが勝利しているとき
        {
            anim.SetBool("walk", false);
            anim.Play("enemy_stand");
            rb.velocity = Vector2.zero; //速度を0にする
            rb.isKinematic = true; //rigidbodyをkinematicにする
        }
    }

    private Vector2 enemyMove() //移動速度を返す
    {

        if (enemy_attacked == true)//敵がやられ状態のとき
        {
            return new Vector2(7, rb.velocity.y);
        }
        else if (enemy_charge==true)//敵がタメ状態のとき
        {
            return new Vector2(0, rb.velocity.y);
        }
        else if (enemy_attack == true)//敵が攻撃状態のとき
        {
            return new Vector2(0, rb.velocity.y);
        }
        else //敵が特殊な状態でない時の処理
        {
            player_pos = player_go.transform.position;
            enemy_pos = transform.position;
            p_to_e = enemy_pos.x - player_pos.x;

            move_switch_time += Time.deltaTime; 

            if ( p_to_e > 3.5 && move_switch_time>=0.4) //プレイヤーと敵の距離が一定以上のとき
            {
                move_switch_time = 0f;
                //プレイヤーが攻撃状態なら停止、そうでなければ前進
                if (player_cl.player_attack == true || player_cl.player_charge == true)
                {
                    move_dir = 0;
                }
                else
                {
                    move_dir = -1;
                }
            }
            else if(p_to_e < 3.5 && move_switch_time >= 0.4) //プレイヤーと敵の距離が一定以下のとき
            {
                move_switch_time = 0f;
                //プレイヤーが攻撃状態なら後退、そうでなければ何でもいいのでランダム行動
                if (player_cl.player_attack == true || player_cl.player_charge == true)
                {
                    move_dir = 1;
                }
                else
                {
                    //敵が攻撃クールダウン中なら後退
                    if (enemy_attack_cooldown < attack_cooldown)
                    {
                        move_dir = 1;
                    }
                    else
                    {
                        rand = Random.Range(-1, 1);
                        //乱数によって移動方向を決定
                        if (rand == -1)
                        {
                            move_dir = -1;
                        }
                        else
                        {
                            move_dir = 0;
                        }
                    }
                }
            }
            else
            {
            }

            if (move_dir < 0) //move_dirの値によって移動速度を返す
            {
                anim.SetBool("walk", true);
                return new Vector2(-speed, rb.velocity.y);
            }
            else if (move_dir > 0)
            {
                anim.SetBool("walk", true);
                return new Vector2(speed * 0.8f, rb.velocity.y);
            }
            else
            {
                anim.SetBool("walk", false);
                return new Vector2(0, rb.velocity.y);
            }
        }
    }

    void enemyAttack()
    {

        if (enemy_attacked == true) //攻撃されたら敵の状態をリセットする
        {
            attack_trigger = false;
            enemy_charge = false;
            enemy_charge_time = 0.0f;
            enemy_attack = false;
            enemy_attack_time = 0.0f;
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//攻撃判定の絵を非表示

        }

        if(p_to_e < 3) //プレイヤーと敵の距離が一定以下なら攻撃トリガーをtrueにする
        {
            attack_trigger = true;
        }
        else
        {
            attack_trigger = false;
        }

        if (attack_trigger == true && enemy_charge != true && enemy_attack != true) //敵の攻撃トリガーがtrueの時
        {
            attack_trigger = false;
            enemy_charge = true;
        }

        if (enemy_charge == true) //敵の溜め状態がtrueの時
        {
            if (enemy_attacked != true)
            {
                anim.Play("enemy_charge");
            }
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
            if (enemy_attacked != true) 
            { 
                anim.Play("enemy_attack");
            }
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);//攻撃判定の絵を表示

            enemy_attack_time += Time.deltaTime;

            on_at_col = at_col.on_attack_collider;
            if (on_at_col == true) //敵の攻撃判定内にプレイヤーが入っていたらプレイヤーのやられ状態をtrueにする
            {
                player_cl.player_attacked = true;
            }

            if (enemy_attack_time >= attack_time) //attack_timeに設定した秒数経過すると攻撃モーション終了
            {
                enemy_attack_time = 0.0f;
                enemy_attack_cooldown = 0.0f;
                enemy_attack = false;
                anim.Play("enemy_stand");
                attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//攻撃判定の絵を非表示

            }

        }
    }
}
