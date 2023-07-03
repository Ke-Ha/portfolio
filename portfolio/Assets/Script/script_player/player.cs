//�v���C���[�̃Q�[���I�u�W�F�N�g�ɂ���X�N���v�g
//2023/06/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public attack_collider at_col;

    public float speed;
    public float attacked_end=0.5f; //�v���C���[�̂����ԏI������
    public float charge_time = 0.5f; //�v���C���[�̃^������
    public float attack_time = 0.3f; //�v���C���[�̍U�����[�V��������

    private Animator anim;
    private Rigidbody2D rb;
    //private GameObject enemy_go; //�G�̃Q�[���I�u�W�F�N�g
    //private Rigidbody2D enemy_rb; //�G��rigidbody
    private GameObject attack_go; //�U������̃Q�[���I�u�W�F�N�g
    
    public bool player_attacked=false; //�����������Ԃ��ǂ���
    public bool enemy_attacked=false; //�G�������Ԃ��ǂ���

    private float horizontalKey = 0f;
    private float player_attacked_time = 0f;
    private float player_charge_time = 0f; 
    private float player_attack_time = 0f;

    private bool on_at_col=false; //�U������̃R���C�_�[�ɓG�������Ă��邩�ǂ���
    private bool attack_key=false;
    private bool player_charge = false;
    private bool player_attack = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //enemy_go = GameObject.Find("enemy");

        attack_go = GameObject.Find("attack_collider");
    }

    void Update()
    {
        if (player_attacked == true) //�v���C���[�̂����Ԃ�ON�̂Ƃ�
        {
            player_attacked_time += Time.deltaTime;
            if (player_attacked_time >= attacked_end) //�����Ԃ�attacked_end�ŉ�������
            {
                player_attacked = false;
                player_attacked_time = 0.0f;
            }
        }
        rb.velocity=playerMove();

        playerAttack();

    }

    Vector2 playerMove() //�v���C���[�̈ړ����x�̒l��Ԃ�
    {
        horizontalKey = Input.GetAxis("Horizontal");//�uHorizontal�v�L�[�i����́��A���j�̓��͂�float�l�Ŏ擾����@�������A������
        float xSpeed = 0.0f;

        if (player_attacked==true) //�v���C���[�������Ԃ̎��̈ړ����x
        {
            return new Vector2(-7,rb.velocity.y);
        }
        else if(player_charge==true) //�v���C���[���^����Ԃ̎��̈ړ����x
        {
            return new Vector2(0, rb.velocity.y);
        }
        else if (player_attack==true) //�v���C���[���U����Ԃ̎��̈ړ����x
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
        attack_key = Input.GetKeyDown(KeyCode.L); //�U���L�[�̓��͂����m����i����L�j

        if (attack_key == true && player_charge!=true && player_attack != true) //�L�[�������ꂽ�痭�߃��[�V�����J�n�i���ɗ��ߒ��A�U�����͈ڍs���Ȃ��j
        {
            player_charge = true;
        }

        if(player_charge == true) //�v���C���[�̗��ߏ�Ԃ�true�̎�
        {
            anim.Play("player_charge");

            player_charge_time += Time.deltaTime;

            if(player_charge_time >= charge_time) //charge_time�ɐݒ肵���b���o�߂���Ɨ��߃��[�V�����I��
            {
                player_charge_time = 0.0f;
                player_charge = false;
                player_attack = true;
            }
        }

        if (player_attack == true) //�v���C���[�̍U����Ԃ�true�̎�
        {
            anim.Play("player_attack");
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);//�U������̊G��\��



            player_attack_time += Time.deltaTime;

            on_at_col = at_col.on_attack_collider;
            if (on_at_col == true) //�v���C���[�̍U��������ɓG�������Ă�����G�̂����Ԃ�true�ɂ���
            {
                enemy_attacked = true;
            }

            if(player_attack_time >= attack_time) //attack_time�ɐݒ肵���b���o�߂���ƍU�����[�V�����I��
            {
                player_attack_time = 0.0f;
                player_attack = false;
                anim.Play("player_stand");
                attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//�U������̊G���\��

            }

        }
    }

}
