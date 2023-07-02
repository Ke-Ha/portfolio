//�G�̃Q�[���I�u�W�F�N�g�ɂ�������X�N���v�g
//2023/06/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float enemy_atacked_end = 0.5f;

    private Rigidbody2D rb;
    public player player_cl; //player�N���X���擾

    private bool pl_enemy_atacked=false; //player�N���X��������Ă����u�G�̂����ԁv
    private float enemy_atacked_time = 0f; //�G�̂����Ԏ�������
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        pl_enemy_atacked = player_cl.enemy_atacked; //player������u�G�̂����ԁv�������Ă���

        if (pl_enemy_atacked == true)//�u�G�̂����ԁv��true�̎��@
        {
            enemy_atacked_time += Time.deltaTime;
            if (enemy_atacked_time >= enemy_atacked_end)//�G�̂����Ԃ�enemy_atacked_end�ŉ�������
            {
                player_cl.enemy_atacked=false;
                enemy_atacked_time = 0f;
            }
        }
        rb.velocity = enemyMove();
    }

    private Vector2 enemyMove()
    {
         pl_enemy_atacked = player_cl.enemy_atacked; //player������u�G�̂����ԁv�������Ă���

        if (pl_enemy_atacked == true)//�u�G�̂����ԁv��true�̎��̏���
        {
            return new Vector2(7, rb.velocity.y);
        }
        else //�G������ȏ�ԂłȂ����̏���
        {
            return new Vector2(0, rb.velocity.y);
        }
    }
}
