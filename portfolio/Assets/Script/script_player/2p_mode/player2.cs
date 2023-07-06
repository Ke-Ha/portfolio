//2Pモードで2P側につけるスクリプト
//2023/7/6
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : MonoBehaviour
{
    public en_attack_collider en_at_col;
    public player1 player1_cl; //player1クラスを取得
    public fall fall_cl;


    public bool player2_attacked = false; //今やられ状態かどうか

    public float speed = 4.0f;
    public float attacked_end = 0.5f; //やられ状態終了時間
    public float charge_time = 0.5f; //タメ時間
    public float attack_time = 0.3f; //攻撃モーション時間
    public float attack_cooldown = 1.0f; //攻撃クールダウン時間


    private Animator anim;
    private Rigidbody2D rb;
    private GameObject attack_go;

    private float horizontalKey = 0.0f;
    private float player2_attacked_time = 0f;
    private float player2_charge_time = 0f;
    private float player2_attack_time = 0f;
    private float player2_attack_cooldown = 0.0f;

    private bool on_at_col = false;
    private bool attack_key = false;
    private bool player2_charge = false; //今タメ状態かどうか
    private bool player2_attack = false; //今攻撃状態かどうか


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attack_go = GameObject.Find("enemy_attack_collider");

    }
    void Update()
    {
        if (fall_cl.playerwin != true && fall_cl.enemywin != true)//どちらかの勝利状態がfalseのとき
        {

            if (player2_attacked == true)//「自分のやられ状態」がtrueの時　
            {
                anim.Play("enemy_attacked");
                player2_attacked_time += Time.deltaTime;
                if (player2_attacked_time >= attacked_end)//自分のやられ状態をplayer2_attacked_endで解除する
                {
                    player2_attacked = false;
                    player2_attacked_time = 0f;
                    anim.Play("enemy_stand");
                }
            }
            rb.velocity = player2Move();

            player2_attack_cooldown += Time.deltaTime;
            if (player2_attack_cooldown >= attack_cooldown)
            {
                player2Attack();
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

    Vector2 player2Move() //プレイヤーの移動速度の値を返す
    {
        horizontalKey = Input.GetAxis("P2 Horizontal");//「P2 Horizontal」キー（今回は←、→）の入力をfloat値で取得する　←が正、→が負
        float xSpeed = 0.0f;

        if (player2_attacked == true) //プレイヤーがやられ状態の時の移動速度
        {
            return new Vector2(7, rb.velocity.y);
        }
        else if (player2_charge == true) //プレイヤーがタメ状態の時の移動速度
        {
            return new Vector2(0, rb.velocity.y);
        }
        else if (player2_attack == true) //プレイヤーが攻撃状態の時の移動速度
        {
            return new Vector2(0, rb.velocity.y);
        }
        else //特殊な状態以外の時のプレイヤー移動（キー入力によって移動できる時）
        {
            if (horizontalKey > 0)
            {
                anim.SetBool("walk", true);
                xSpeed = speed * 0.8f;

            }
            else if (horizontalKey < 0)
            {
                anim.SetBool("walk", true);
                xSpeed = -speed;
            }
            else
            {
                anim.SetBool("walk", false);
            }

            return new Vector2(xSpeed, rb.velocity.y);//移動速度の最終適用
        }
    }

    void player2Attack()
    {
        attack_key = Input.GetKeyDown(KeyCode.Space);

        if (player2_attacked == true)
        {
            player2_charge = false;
            player2_charge_time = 0.0f;
            player2_attack = false;
            player2_attack_time = 0.0f;
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//攻撃判定の絵を非表示

        }

        if (attack_key == true && player2_charge != true && player2_attack != true)
        {
            player2_charge = true;
        }

        if (player2_charge == true)
        {
            if (player2_attacked != true)
            {
                anim.Play("enemy_charge");
            }
            player2_charge_time += Time.deltaTime;

            if (player2_charge_time >= charge_time) //charge_timeに設定した秒数経過すると溜めモーション終了
            {
                player2_charge_time = 0.0f;
                player2_charge = false;
                player2_attack = true;
            }
        }

        if (player2_attack == true) //敵の攻撃状態がtrueの時
        {
            if (player2_attacked != true)
            {
                anim.Play("enemy_attack");
            }
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);//攻撃判定の絵を表示



            player2_attack_time += Time.deltaTime;

            on_at_col = en_at_col.on_attack_collider;
            if (on_at_col == true) //敵の攻撃判定内にプレイヤーが入っていたらプレイヤーのやられ状態をtrueにする
            {
                player1_cl.player1_attacked = true;
            }

            if (player2_attack_time >= attack_time) //attack_timeに設定した秒数経過すると攻撃モーション終了
            {
                player2_attack_time = 0.0f;
                player2_attack_cooldown = 0.0f;
                player2_attack = false;
                anim.Play("enemy_stand");
                attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//攻撃判定の絵を非表示

            }

        }
    }
}

