//敵の攻撃判定にくっつけるスクリプト
//2023/7/3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_attack_collider : MonoBehaviour
{
    public bool on_attack_collider; //攻撃判定のコライダーに何か入っていたらtrueになる

    private string player_tag = "Player";

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == player_tag) //対象のタグがplayerだったらtrue
        {
            on_attack_collider = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == player_tag)
        {
            on_attack_collider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == player_tag)
        {
            on_attack_collider = false;
        }
    }
}
