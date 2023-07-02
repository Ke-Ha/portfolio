//�v���C���[�̃Q�[���I�u�W�F�N�g�ɂ���X�N���v�g
//2023/06/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public attack_collider at_col;
    public float atacked_end=0.5f; //�v���C���[�̂����ԏI������
    public float charge_time = 0.5f;

    private Animator anim;
    private Rigidbody2D rb;
    private GameObject enemy_go; //�G�̃Q�[���I�u�W�F�N�g
    private Rigidbody2D enemy_rb; //�G��rigidbody
    
    private bool on_at_col; //�U������̃R���C�_�[�ɓG�������Ă��邩�ǂ���
    public bool player_atacked=false; //�����������Ԃ��ǂ���
    public bool enemy_atacked=false; //�G�������Ԃ��ǂ���

    private float horizontalKey=0f;
    //private float enemy_atacked_time = 0f;
    private float player_atacked_time=0f; //�v���C���[�̂����Ԏ���
    private float player_charge_time = 0f;
    private bool ZKey=false;
    private bool player_charge = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy_go = GameObject.Find("enemy");
        enemy_rb = enemy_go.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player_atacked == true) //�v���C���[�̂����Ԃ�ON�̂Ƃ�
        {
            player_atacked_time += Time.deltaTime;
            if (player_atacked_time >= atacked_end) //�����Ԃ�atacked_end�ŉ�������
            {
                player_atacked = false;
                player_atacked_time = 0.0f;
            }
        }
        rb.velocity=playerMove();

        playerAttack();

    }

    Vector2 playerMove() //�v���C���[�̈ړ����x�̒l��Ԃ�
    {
        horizontalKey = Input.GetAxis("Horizontal");//�uHorizontal�v�L�[�i����́��A���j�̓��͂�float�l�Ŏ擾����@�������A������
        float xSpeed = 0.0f;

        if (player_atacked==true) //�v���C���[�������Ԃ̎��̈ړ����x
        {
            return new Vector2(-7,rb.velocity.y);
        }
        else if(player_charge==true)
        {
            return new Vector2(0, rb.velocity.y);
        }
        else //����ȏ�ԈȊO�̎��̃v���C���[�ړ��i�L�[���͂ɂ���Ĉړ��ł��鎞�j
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

            return new Vector2(xSpeed, rb.velocity.y);//�ړ����x�̍ŏI�K�p
        }
    }

    void playerAttack() //�v���C���[�̍U��
    {
        ZKey = Input.GetKeyDown(KeyCode.Z);

        if (ZKey == true || player_charge==true)//Z�L�[�������Ɨ��߃X�^�[�g
        {
            anim.Play("player_charge");

            player_charge = true;
            player_charge_time += Time.deltaTime;

            if (player_charge_time >= charge_time)
            {
                on_at_col = at_col.on_attack_collider;//�U������̒��ɓG�������Ă��邩�擾����
                if (on_at_col == true)
                {
                    //Z�L�[�������ꂽ�A���G���U������̒��ɂ�����G�̂����Ԃ�ON�ɂ���
                    enemy_atacked = true;
                }
                player_charge_time = 0.0f;
                player_charge = false;
                anim.Play("player_stand");

            }

        }


    }

}
