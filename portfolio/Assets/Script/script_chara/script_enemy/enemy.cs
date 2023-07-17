//�G�̃Q�[���I�u�W�F�N�g�ɂ�������X�N���v�g
//2023/06/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public attack_collider at_col;
    public player player_cl; //player�N���X���擾
    public fall fall_cl;

    public bool enemy_attacked=false; //�������Ԃ��ǂ���

    public float speed = 3.0f;
    public float attacked_end = 0.5f; //�����ԏI������
    public float charge_time=0.5f; //�^������
    public float attack_time=0.2f; //�U�����[�V��������
    public float attack_cooldown = 1.0f; //�U���N�[���_�E������


    private Animator anim;
    private Rigidbody2D rb;
    private GameObject attack_go; //�U������̃Q�[���I�u�W�F�N�g
    private GameObject player_go;

    private Vector3 player_pos;
    private Vector3 enemy_pos;
    
    private float enemy_attacked_time = 0f; 
    private float enemy_charge_time = 0f;
    private float enemy_attack_time = 0f;
    private float enemy_attack_cooldown = 0.0f;
    private float move_switch_time = 0.0f; //�ړ��؂�ւ��̃N�[���^�C��
    private float p_to_e; //�v���C���[�ƓG�̋���
    private float move_dir = 0f; //�G�̈ړ������i1�Ł��A0�Œ�~�A-1�Ł��j
    private int rand; //����������p

    private bool on_at_col = false; //�U������ɓG�������Ă��邩�ǂ���
    private bool enemy_charge = false; //���^����Ԃ��ǂ���
    private bool enemy_attack = false; //���U����Ԃ��ǂ���
    private bool attack_trigger; //���̃g���K�[��true�ɂȂ�����U������


    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attack_go = transform.GetChild(0).gameObject;
        player_go = GameObject.Find("player");

    }
    void Update()
    {
        if(fall_cl.playerwin!=true && fall_cl.enemywin!=true){
            if (enemy_attacked == true)//�u�G�̂����ԁv��true�̎��@
            {
                anim.Play("enemy_attacked");
                enemy_attacked_time += Time.deltaTime;
                if (enemy_attacked_time >= attacked_end)//�G�̂����Ԃ���莞�Ԍo�߂ŉ�������
                {
                    enemy_attacked = false;
                    enemy_attacked_time = 0f;
                    anim.Play("enemy_stand");
                }
            }
            rb.velocity = enemyMove(); 

            enemy_attack_cooldown += Time.deltaTime;
            if (enemy_attack_cooldown >= attack_cooldown)
            {
                enemyAttack();
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

    private Vector2 enemyMove() //�ړ����x��Ԃ�
    {

        if (enemy_attacked == true)//�G�������Ԃ̂Ƃ�
        {
            return new Vector2(7, rb.velocity.y);
        }
        else if (enemy_charge==true)//�G���^����Ԃ̂Ƃ�
        {
            return new Vector2(0, rb.velocity.y);
        }
        else if (enemy_attack == true)//�G���U����Ԃ̂Ƃ�
        {
            return new Vector2(0, rb.velocity.y);
        }
        else //�G������ȏ�ԂłȂ����̏���
        {
            player_pos = player_go.transform.position;
            enemy_pos = transform.position;
            p_to_e = enemy_pos.x - player_pos.x;

            move_switch_time += Time.deltaTime; 

            if ( p_to_e > 3.5 && move_switch_time>=0.4) //�v���C���[�ƓG�̋��������ȏ�̂Ƃ�
            {
                move_switch_time = 0f;
                //�v���C���[���U����ԂȂ��~�A�����łȂ���ΑO�i
                if (player_cl.player_attack == true || player_cl.player_charge == true)
                {
                    move_dir = 0;
                }
                else
                {
                    move_dir = -1;
                }
            }
            else if(p_to_e < 3.5 && move_switch_time >= 0.4) //�v���C���[�ƓG�̋��������ȉ��̂Ƃ�
            {
                move_switch_time = 0f;
                //�v���C���[���U����ԂȂ��ށA�����łȂ���Ή��ł������̂Ń����_���s��
                if (player_cl.player_attack == true || player_cl.player_charge == true)
                {
                    move_dir = 1;
                }
                else
                {
                    //�G���U���N�[���_�E�����Ȃ���
                    if (enemy_attack_cooldown < attack_cooldown)
                    {
                        move_dir = 1;
                    }
                    else
                    {
                        rand = Random.Range(-1, 1);
                        //�����ɂ���Ĉړ�����������
                        if (rand == -1)
                        {
                            move_dir = -1;
                        }
                        else
                        {
                            move_dir = 0;
                        }
                    }
                }
            }
            else
            {
            }

            if (move_dir < 0) //move_dir�̒l�ɂ���Ĉړ����x��Ԃ�
            {
                anim.SetBool("walk", true);
                return new Vector2(-speed, rb.velocity.y);
            }
            else if (move_dir > 0)
            {
                anim.SetBool("walk", true);
                return new Vector2(speed * 0.8f, rb.velocity.y);
            }
            else
            {
                anim.SetBool("walk", false);
                return new Vector2(0, rb.velocity.y);
            }
        }
    }

    void enemyAttack()
    {

        if (enemy_attacked == true) //�U�����ꂽ��G�̏�Ԃ����Z�b�g����
        {
            attack_trigger = false;
            enemy_charge = false;
            enemy_charge_time = 0.0f;
            enemy_attack = false;
            enemy_attack_time = 0.0f;
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//�U������̊G���\��

        }

        if(p_to_e < 3) //�v���C���[�ƓG�̋��������ȉ��Ȃ�U���g���K�[��true�ɂ���
        {
            attack_trigger = true;
        }
        else
        {
            attack_trigger = false;
        }

        if (attack_trigger == true && enemy_charge != true && enemy_attack != true) //�G�̍U���g���K�[��true�̎�
        {
            attack_trigger = false;
            enemy_charge = true;
        }

        if (enemy_charge == true) //�G�̗��ߏ�Ԃ�true�̎�
        {
            if (enemy_attacked != true)
            {
                anim.Play("enemy_charge");
            }
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
            if (enemy_attacked != true) 
            { 
                anim.Play("enemy_attack");
            }
            attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);//�U������̊G��\��

            enemy_attack_time += Time.deltaTime;

            on_at_col = at_col.on_attack_collider;
            if (on_at_col == true) //�G�̍U��������Ƀv���C���[�������Ă�����v���C���[�̂����Ԃ�true�ɂ���
            {
                player_cl.player_attacked = true;
            }

            if (enemy_attack_time >= attack_time) //attack_time�ɐݒ肵���b���o�߂���ƍU�����[�V�����I��
            {
                enemy_attack_time = 0.0f;
                enemy_attack_cooldown = 0.0f;
                enemy_attack = false;
                anim.Play("enemy_stand");
                attack_go.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);//�U������̊G���\��

            }

        }
    }
}
