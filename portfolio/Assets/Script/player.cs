using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public attack_collider at_col;

    private Animator anim;
    private Rigidbody2D rb;
    private GameObject enemy_go; //敵のゲームオブジェクト
    private Rigidbody2D enemy_rb; //敵のrigidbody
    
    private bool on_at_col; //攻撃判定のコライダーに敵が入っているかどうか
    public bool player_atacked=false; //自分がやられ状態かどうか
    public bool enemy_atacked=false; //敵がやられ状態かどうか

    private float horizontalKey=0f;
    //private float enemy_atacked_time = 0f;
    private float player_atacked_time = 0f;
    private bool ZKey=false;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy_go = GameObject.Find("enemy");
        enemy_rb = enemy_go.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player_atacked == true)
        {
            player_atacked_time += Time.deltaTime;
            if (player_atacked_time >= 0.7f)
            {
                player_atacked = false;
            }
        }
        rb.velocity=playerMove();

        playerAttack();

    }

    Vector2 playerMove() //プレイヤーの移動速度をキー入力によって計算し、値を返す
    {
        horizontalKey = Input.GetAxis("Horizontal");//「Horizontal」キー（今回は←、→）の入力をfloat値で取得する　←が正、→が負
        float xSpeed = 0.0f;

        if (player_atacked!=true) 
        {
            if (horizontalKey > 0)
            {
                anim.SetBool("walk", true);
                //transform.localScale = new Vector3(1, 1, 1);
                xSpeed = speed;

            }
            else if (horizontalKey < 0)
            {
                anim.SetBool("walk", true);
                //transform.localScale = new Vector3(-1,1,1);
                xSpeed = -speed * 0.8f;
            }
            else
            {
                anim.SetBool("walk", false);
            }

            return new Vector2(xSpeed, rb.velocity.y);//移動速度の最終適用
        }
        else
        {
            return new Vector2(-7,rb.velocity.y);
        }
    }

    void playerAttack()
    {
        ZKey = Input.GetKeyDown(KeyCode.Z);

        if (ZKey == true)
        {

            on_at_col = at_col.on_attack_collider;

            //今はつけてないが溜めモーションの後攻撃する
            //anim.SetBool("charge",true);

            if (on_at_col == true)
            {
                //Zキーが押された、かつ敵が攻撃判定の中にいたら敵のやられ状態をONにする
                enemy_atacked = true;
            }
        }


    }


}
