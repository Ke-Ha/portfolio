//足場の下のコライダーにつけるスクリプト
//2023/7/6

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall : MonoBehaviour
{
    public bool playerwin = false; //プレイヤーが勝利したかどうか
    public bool enemywin = false; //敵が勝利したかどうか

    private string player_tag = "Player";
    private string enemy_tag = "Enemy";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision) //足場の下のコライダーから何かが出た時
    {
        if (collision.gameObject.tag == player_tag) //このコライダーから出たものがplayerタグだった時
        {
            enemywin = true; //敵の勝利状態をtrueにする
        }
        if (collision.gameObject.tag == enemy_tag) //このコライダーから出たものがenemyタグだった時
        {
            playerwin = true; //プレイヤーの勝利状態をtrueにする
        }

    }
}
