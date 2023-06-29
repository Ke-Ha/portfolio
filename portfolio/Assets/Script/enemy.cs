using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public player player_cl;

    private bool pl_enemy_atacked=false;
    private float enemy_atacked_time = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        pl_enemy_atacked = player_cl.enemy_atacked;

        if (pl_enemy_atacked == true)
        {
            enemy_atacked_time += Time.deltaTime;
            if (enemy_atacked_time >= 0.5f)
            {
                player_cl.enemy_atacked=false;
                enemy_atacked_time = 0f;
            }
        }
        rb.velocity = enemyMove();
    }

    private Vector2 enemyMove()
    {
         pl_enemy_atacked = player_cl.enemy_atacked;

        if (pl_enemy_atacked != true)
        {
            return new Vector2(-3, rb.velocity.y);
        }
        else
        {
            return new Vector2(7, rb.velocity.y);
        }
    }
}
