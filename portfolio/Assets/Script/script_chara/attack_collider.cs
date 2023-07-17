//playerの攻撃判定にくっつけるスクリプト
//2023/6/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class attack_collider : MonoBehaviour
{


    public bool on_attack_collider; //攻撃判定のコライダーに何か入っていたらtrueになる

    private string player_tag = "Player";
    private string enemy_tag = "Enemy";
    private string player1_tag = "Player1";
    private string player2_tag = "Player2";
    private string now_scene;

    void Start()
    {
        now_scene=SceneManager.GetActiveScene().name; //今のシーン名を取得
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //今のモードが2pモードだったら
        if (now_scene == "mode2")
        {
            //対象のタグがplayer1かplayer2だったらtrue
            if (collision.gameObject.tag == player2_tag || collision.gameObject.tag == player1_tag) on_attack_collider = true;
        }
        else
        {
            //対象のタグがPlayerかEnemyだったらtrue
            if (collision.gameObject.tag==player_tag || collision.gameObject.tag == enemy_tag) on_attack_collider = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (now_scene == "mode2")
        {
            if (collision.gameObject.tag == player2_tag || collision.gameObject.tag == player1_tag) on_attack_collider = true;
        }
        else
        {
            if (collision.gameObject.tag == player_tag || collision.gameObject.tag == enemy_tag) on_attack_collider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (now_scene == "mode2")
        {
            if (collision.gameObject.tag == player2_tag || collision.gameObject.tag == player1_tag) on_attack_collider = false;
        }
        else
        {
            if (collision.gameObject.tag == player_tag || collision.gameObject.tag == enemy_tag) on_attack_collider = false;
        }
    }
}
