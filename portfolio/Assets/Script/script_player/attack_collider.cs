//playerの攻撃判定にくっつけるスクリプト
//2023/6/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_collider : MonoBehaviour
{


    public bool on_attack_collider; //攻撃判定のコライダーに何か入っていたらtrueになる

    private string enemy_tag = "Enemy";

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == enemy_tag) //衝突した対象のタグがEnemyだったらtrue
        {
            on_attack_collider = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag==enemy_tag){ //衝突した対象のタグがEnemyだったらtrue
            on_attack_collider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        on_attack_collider = false;
    }
}
