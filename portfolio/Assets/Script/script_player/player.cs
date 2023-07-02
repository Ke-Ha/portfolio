//プレイヤーのゲームオブジェクトにつけるスクリプト
//2023/06/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public attack_collider at_col;
    public float atacked_end=0.5f; //プレイヤーのやられ状態終了時間
    public float charge_time = 0.5f;

    private Animator anim;
    private Rigidbody2D rb;
    private GameObject enemy_go; //敵のゲームオブジェクト
    private Rigidbody2D enemy_rb; //敵のrigidbody
    
    private bool on_at_col; //攻撃判定のコライダーに敵が入っているかどうか
    public bool player_atacked=false; //自分がやられ状態かどうか
    public bool enemy_atacked=false; //敵がやられ状態かどうか

    private float horizontalKey=0f;
    //private float enemy_atacked_time = 0f;
    private float player_atacked_time=0f; //プレイヤーのやられ状態時間
    private float player_charge_time = 0f;
    private bool ZKey=false;
    private bool player_charge = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy_go = GameObject.Find("enemy");
        enemy_rb = enemy_go.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player_atacked == true) //プレイヤーのやられ状態がONのとき
        {
            player_atacked_time += Time.deltaTime;
            if (player_atacked_time >= atacked_end) //やられ状態をatacked_endで解除する
            {
                player_atacked = false;
                player_atacked_time = 0.0f;
            }
        }
        rb.velocity=playerMove();

        playerAttack();

    }

    Vector2 playerMove() //プレイヤーの移動速度の値を返す
    {
        horizontalKey = Input.GetAxis("Horizontal");//「Horizontal」キー（今回は←、→）の入力をfloat値で取得する　←が正、→が負
        float xSpeed = 0.0f;

        if (player_atacked==true) //プレイヤーがやられ状態の時の移動速度
        {
            return new Vector2(-7,rb.velocity.y);
        }
        else if(player_charge==true)
        {
            return new Vector2(0, rb.velocity.y);
        }
        else //特殊な状態以外の時のプレイヤー移動（キー入力によって移動できる時）
        {
            if (horizontalKey > 0)
            {
                anim.SetBool("walk", true);
                xSpeed = speed;

            }
            else if (horizontalKey < 0)
            {
                anim.SetBool("walk", true);
                xSpeed = -speed * 0.8f;
            }
            else
            {
                anim.SetBool("walk", false);
            }

            return new Vector2(xSpeed, rb.velocity.y);//移動速度の最終適用
        }
    }

    void playerAttack() //プレイヤーの攻撃
    {
        ZKey = Input.GetKeyDown(KeyCode.Z);

        if (ZKey == true || player_charge==true)//Zキーを押すと溜めスタート
        {
            anim.Play("player_charge");

            player_charge = true;
            player_charge_time += Time.deltaTime;

            if (player_charge_time >= charge_time)
            {
                on_at_col = at_col.on_attack_collider;//攻撃判定の中に敵が入っているか取得する
                if (on_at_col == true)
                {
                    //Zキーが押された、かつ敵が攻撃判定の中にいたら敵のやられ状態をONにする
                    enemy_atacked = true;
                }
                player_charge_time = 0.0f;
                player_charge = false;
                anim.Play("player_stand");

            }

        }


    }

}
