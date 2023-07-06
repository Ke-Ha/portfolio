//プレイヤーのゲームオブジェクトにつけるスクリプト
//2023/06/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public attack_collider at_col;
    public enemy enemy_cl;
    public fall fall_cl;

    public float speed=4.0f; //移動速度
    public float attacked_end=0.5f; //プレイヤーのやられ状態終了時間
    public float charge_time = 0.5f; //プレイヤーのタメ時間
    public float attack_time = 0.3f; //プレイヤーの攻撃モーション時間
    public float attack_cooldown = 1.0f; //プレイヤーの攻撃クールダウン

    private Animator anim;
    private Rigidbody2D rb;
    //private GameObject enemy_go; //敵のゲームオブジェクト
    //private Rigidbody2D enemy_rb; //敵のrigidbody
    private GameObject attack_go; //攻撃判定のゲームオブジェクト
    
    public bool player_attacked=false; //自分がやられ状態かどうか
    //public bool enemy_attacked=false; //敵がやられ状態かどうか

    private float horizontalKey = 0.0f;
    private float player_attacked_time = 0.0f;
    private float player_charge_time = 0.0f; 
    private float player_attack_time = 0.0f;
    private float player_attack_cooldown = 0.0f;

    private bool on_at_col=false; //攻撃判定のコライダーに敵が入っているかどうか
    private bool attack_key=false; //攻撃キーが押されたかどうか
    public bool player_charge = false; //タメ状態かどうか
    public bool player_attack = false; //攻撃状態かどうか


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //enemy_go = GameObject.Find("enemy");

        attack_go = GameObject.Find("attack_collider");
    }

    void Update()
    {
        if(fall_cl.playerwin != true && fall_cl.enemywin != true){ //どちらかの勝利状態がfalseのとき
            if (player_attacked == true) //プレイヤーのやられ状態がONのとき
            {
                anim.Play("player_attacked");
                player_attacked_time += Time.deltaTime;
                if (player_attacked_time >= attacked_end) //やられ状態をattacked_endで解除する
                {
                    player_attacked = false; 
                    player_attacked_time = 0.0f;
                    anim.Play("player_stand");
                }
            }
            rb.velocity = playerMove();

            player_attack_cooldown += Time.deltaTime; 
            if (player_attack_cooldown >= attack_cooldown) //クールダウン時間以上経過してから攻撃できるようにする
            {
                playerAttack();
            }
        }
        else //どちらかが勝利しているとき
        {
            anim.SetBool("walk", false);
            anim.Play("player_stand");
            rb.velocity = Vector2.zero; //速度を0にする
            rb.isKinematic = true; //rigidbodyをkinematicにする
        }
    }

    Vector2 playerMove() //プレイヤーの移動速度の値を返す
    {
        horizontalKey = Input.GetAxis("Horizontal");//「Horizontal」キー（今回は←、→）の入力をfloat値で取得する　←が正、→が負
        float xSpeed = 0.0f;

        if (player_attacked==true) //プレイヤーがやられ状態の時の移動速度
        {
            return new Vector2(-7,rb.velocity.y);
        }
        else if(player_charge==true) //プレイヤーがタメ状態の時の移動速度
        {
            return new Vector2(0, rb.velocity.y);
        }
        else if (player_attack==true) //プレイヤーが攻撃状態の時の移動速度
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
        attack_key = Input.GetKeyDown(KeyCode.L); //攻撃キーの入力を検知する（現状L）

        if(player_attacked == true) //溜め、攻撃モーション中に相手から攻撃を受けるとやられ状態で上書きする
        {
            player_charge = false;
            player_charge_time = 0.0f;
            player_attack = false;
            player_attack_time = 0.0f;
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//攻撃判定の絵を表示

        }

        if (attack_key == true && player_charge!=true && player_attack != true && player_attacked != true) //キーが押されたら溜めモーション開始（既に溜め中、攻撃中は移行しない）
        {
            player_charge = true;
        }

        if(player_charge == true && player_attacked != true) //プレイヤーの溜め状態がtrueの時
        {
            if (player_attacked != true)
            {
                anim.Play("player_charge");
            }

            player_charge_time += Time.deltaTime;

            if(player_charge_time >= charge_time) //charge_timeに設定した秒数経過すると溜めモーション終了
            {
                player_charge_time = 0.0f;
                player_charge = false;
                player_attack = true;
            }
        }

        if (player_attack == true && player_attacked != true) //プレイヤーの攻撃状態がtrueの時
        {
            if (player_attacked != true)
            {
                anim.Play("player_attack");
            }

            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);//攻撃判定の絵を表示

            player_attack_time += Time.deltaTime;

            on_at_col = at_col.on_attack_collider;
            if (on_at_col == true) //プレイヤーの攻撃判定内に敵が入っていたら敵のやられ状態をtrueにする
            {
                enemy_cl.enemy_attacked = true;
            }

            if(player_attack_time >= attack_time) //attack_timeに設定した秒数経過すると攻撃モーション終了
            {
                player_attack_time = 0.0f;
                player_attack_cooldown = 0.0f;
                player_attack = false;
                anim.Play("player_stand");
                attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//攻撃判定の絵を非表示
                
            }

        }
    }

}
