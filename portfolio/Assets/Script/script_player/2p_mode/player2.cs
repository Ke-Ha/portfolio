//2P���[�h��2P���ɂ���X�N���v�g
//2023/7/6
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : MonoBehaviour
{
    public en_attack_collider en_at_col;
    public player1 player1_cl; //player1�N���X���擾
    public fall fall_cl;


    public bool player2_attacked = false; //�������Ԃ��ǂ���

    public float speed = 4.0f;
    public float attacked_end = 0.5f; //�����ԏI������
    public float charge_time = 0.5f; //�^������
    public float attack_time = 0.3f; //�U�����[�V��������
    public float attack_cooldown = 1.0f; //�U���N�[���_�E������


    private Animator anim;
    private Rigidbody2D rb;
    private GameObject attack_go;

    private float horizontalKey = 0.0f;
    private float player2_attacked_time = 0f;
    private float player2_charge_time = 0f;
    private float player2_attack_time = 0f;
    private float player2_attack_cooldown = 0.0f;

    private bool on_at_col = false;
    private bool attack_key = false;
    private bool player2_charge = false; //���^����Ԃ��ǂ���
    private bool player2_attack = false; //���U����Ԃ��ǂ���


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attack_go = GameObject.Find("enemy_attack_collider");

    }
    void Update()
    {
        if (fall_cl.playerwin != true && fall_cl.enemywin != true)//�ǂ��炩�̏�����Ԃ�false�̂Ƃ�
        {

            if (player2_attacked == true)//�u�����̂����ԁv��true�̎��@
            {
                anim.Play("enemy_attacked");
                player2_attacked_time += Time.deltaTime;
                if (player2_attacked_time >= attacked_end)//�����̂����Ԃ�player2_attacked_end�ŉ�������
                {
                    player2_attacked = false;
                    player2_attacked_time = 0f;
                    anim.Play("enemy_stand");
                }
            }
            rb.velocity = player2Move();

            player2_attack_cooldown += Time.deltaTime;
            if (player2_attack_cooldown >= attack_cooldown)
            {
                player2Attack();
            }
        }
        else //�ǂ��炩���������Ă���Ƃ�
        {
            anim.SetBool("walk", false);
            anim.Play("enemy_stand");
            rb.velocity = Vector2.zero; //���x��0�ɂ���
            rb.isKinematic = true; //rigidbody��kinematic�ɂ���
        }
    }

    Vector2 player2Move() //�v���C���[�̈ړ����x�̒l��Ԃ�
    {
        horizontalKey = Input.GetAxis("P2 Horizontal");//�uP2 Horizontal�v�L�[�i����́��A���j�̓��͂�float�l�Ŏ擾����@�������A������
        float xSpeed = 0.0f;

        if (player2_attacked == true) //�v���C���[�������Ԃ̎��̈ړ����x
        {
            return new Vector2(7, rb.velocity.y);
        }
        else if (player2_charge == true) //�v���C���[���^����Ԃ̎��̈ړ����x
        {
            return new Vector2(0, rb.velocity.y);
        }
        else if (player2_attack == true) //�v���C���[���U����Ԃ̎��̈ړ����x
        {
            return new Vector2(0, rb.velocity.y);
        }
        else //����ȏ�ԈȊO�̎��̃v���C���[�ړ��i�L�[���͂ɂ���Ĉړ��ł��鎞�j
        {
            if (horizontalKey > 0)
            {
                anim.SetBool("walk", true);
                xSpeed = speed * 0.8f;

            }
            else if (horizontalKey < 0)
            {
                anim.SetBool("walk", true);
                xSpeed = -speed;
            }
            else
            {
                anim.SetBool("walk", false);
            }

            return new Vector2(xSpeed, rb.velocity.y);//�ړ����x�̍ŏI�K�p
        }
    }

    void player2Attack()
    {
        attack_key = Input.GetKeyDown(KeyCode.Space);

        if (player2_attacked == true)
        {
            player2_charge = false;
            player2_charge_time = 0.0f;
            player2_attack = false;
            player2_attack_time = 0.0f;
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//�U������̊G���\��

        }

        if (attack_key == true && player2_charge != true && player2_attack != true)
        {
            player2_charge = true;
        }

        if (player2_charge == true)
        {
            if (player2_attacked != true)
            {
                anim.Play("enemy_charge");
            }
            player2_charge_time += Time.deltaTime;

            if (player2_charge_time >= charge_time) //charge_time�ɐݒ肵���b���o�߂���Ɨ��߃��[�V�����I��
            {
                player2_charge_time = 0.0f;
                player2_charge = false;
                player2_attack = true;
            }
        }

        if (player2_attack == true) //�G�̍U����Ԃ�true�̎�
        {
            if (player2_attacked != true)
            {
                anim.Play("enemy_attack");
            }
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);//�U������̊G��\��



            player2_attack_time += Time.deltaTime;

            on_at_col = en_at_col.on_attack_collider;
            if (on_at_col == true) //�G�̍U��������Ƀv���C���[�������Ă�����v���C���[�̂����Ԃ�true�ɂ���
            {
                player1_cl.player1_attacked = true;
            }

            if (player2_attack_time >= attack_time) //attack_time�ɐݒ肵���b���o�߂���ƍU�����[�V�����I��
            {
                player2_attack_time = 0.0f;
                player2_attack_cooldown = 0.0f;
                player2_attack = false;
                anim.Play("enemy_stand");
                attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//�U������̊G���\��

            }

        }
    }
}

