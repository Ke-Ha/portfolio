//�G�̃Q�[���I�u�W�F�N�g�ɂ�������X�N���v�g
//2023/06/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public en_attack_collider en_at_col;
    public player player_cl; //player�N���X���擾

    public float speed = 3.0f;
    public float attacked_end = 0.5f;
    public float charge_time=0.5f;
    public float attack_time=0.2f;


    private Animator anim;
    private Rigidbody2D rb;
    private GameObject attack_go;
    
    private float enemy_attacked_time = 0f; //�G�̂����Ԏ�������
    private float enemy_charge_time = 0f;
    private float enemy_attack_time = 0f;

    private bool pl_enemy_attacked=false; //player�N���X��������Ă����u�G�̂����ԁv
    private bool on_at_col = false;
    private bool enemy_charge = false;
    private bool enemy_attack = false;
    private bool attack_trigger=true;

    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attack_go = GameObject.Find("enemy_attack_collider");

    }
    void Update()
    {
        pl_enemy_attacked = player_cl.enemy_attacked; //player������u�G�̂����ԁv�������Ă���

        if (pl_enemy_attacked == true)//�u�G�̂����ԁv��true�̎��@
        {
            
            enemy_attacked_time += Time.deltaTime;
            if (enemy_attacked_time >= attacked_end)//�G�̂����Ԃ�enemy_attacked_end�ŉ�������
            {
                player_cl.enemy_attacked=false;
                enemy_attacked_time = 0f;
            }
        }
        rb.velocity = enemyMove();

        enemyAttack();
    }

    private Vector2 enemyMove()
    {
         pl_enemy_attacked = player_cl.enemy_attacked; //player������u�G�̂����ԁv�������Ă���

        if (pl_enemy_attacked == true)//�u�G�̂����ԁv��true�̎��̏���
        {
            return new Vector2(7, rb.velocity.y);
        }
        else if (enemy_charge==true)
        {
            return new Vector2(0, rb.velocity.y);
        }
        else if (enemy_attack == true)
        {
            return new Vector2(0, rb.velocity.y);
        }
        else //�G������ȏ�ԂłȂ����̏���
        {
            return new Vector2(-2, rb.velocity.y);
        }
    }

    void enemyAttack()
    {
        if (attack_trigger == true && enemy_charge != true && enemy_attack != true)
        {
            enemy_charge = true;
        }

        if (enemy_charge == true)
        {
            anim.Play("enemy_charge");

            enemy_charge_time += Time.deltaTime;

            if (enemy_charge_time >= charge_time) //charge_time�ɐݒ肵���b���o�߂���Ɨ��߃��[�V�����I��
            {
                enemy_charge_time = 0.0f;
                enemy_charge = false;
                enemy_attack = true;
            }
        }

        if (enemy_attack == true) //�G�̍U����Ԃ�true�̎�
        {
            anim.Play("enemy_attack");
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);//�U������̊G��\��



            enemy_attack_time += Time.deltaTime;

            on_at_col = en_at_col.on_attack_collider;
            if (on_at_col == true) //�G�̍U��������Ƀv���C���[�������Ă�����v���C���[�̂����Ԃ�true�ɂ���
            {
                player_cl.player_attacked = true;
            }

            if (enemy_attack_time >= attack_time) //attack_time�ɐݒ肵���b���o�߂���ƍU�����[�V�����I��
            {
                enemy_attack_time = 0.0f;
                enemy_attack = false;
                anim.Play("enemy_stand");
                attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//�U������̊G���\��

            }

        }
    }
}
