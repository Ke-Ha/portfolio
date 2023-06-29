using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public attack_collider at_col;

    private Animator anim;
    private Rigidbody2D rb;
    private GameObject enemy_go; //�G�̃Q�[���I�u�W�F�N�g
    private Rigidbody2D enemy_rb; //�G��rigidbody
    
    private bool on_at_col; //�U������̃R���C�_�[�ɓG�������Ă��邩�ǂ���
    public bool player_atacked=false; //�����������Ԃ��ǂ���
    public bool enemy_atacked=false; //�G�������Ԃ��ǂ���

    private float horizontalKey=0f;
    //private float enemy_atacked_time = 0f;
    private float player_atacked_time = 0f;
    private bool ZKey=false;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy_go = GameObject.Find("enemy");
        enemy_rb = enemy_go.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player_atacked == true)
        {
            player_atacked_time += Time.deltaTime;
            if (player_atacked_time >= 0.7f)
            {
                player_atacked = false;
            }
        }
        rb.velocity=playerMove();

        playerAttack();

    }

    Vector2 playerMove() //�v���C���[�̈ړ����x���L�[���͂ɂ���Čv�Z���A�l��Ԃ�
    {
        horizontalKey = Input.GetAxis("Horizontal");//�uHorizontal�v�L�[�i����́��A���j�̓��͂�float�l�Ŏ擾����@�������A������
        float xSpeed = 0.0f;

        if (player_atacked!=true) 
        {
            if (horizontalKey > 0)
            {
                anim.SetBool("walk", true);
                //transform.localScale = new Vector3(1, 1, 1);
                xSpeed = speed;

            }
            else if (horizontalKey < 0)
            {
                anim.SetBool("walk", true);
                //transform.localScale = new Vector3(-1,1,1);
                xSpeed = -speed * 0.8f;
            }
            else
            {
                anim.SetBool("walk", false);
            }

            return new Vector2(xSpeed, rb.velocity.y);//�ړ����x�̍ŏI�K�p
        }
        else
        {
            return new Vector2(-7,rb.velocity.y);
        }
    }

    void playerAttack()
    {
        ZKey = Input.GetKeyDown(KeyCode.Z);

        if (ZKey == true)
        {

            on_at_col = at_col.on_attack_collider;

            //���͂��ĂȂ������߃��[�V�����̌�U������
            //anim.SetBool("charge",true);

            if (on_at_col == true)
            {
                //Z�L�[�������ꂽ�A���G���U������̒��ɂ�����G�̂����Ԃ�ON�ɂ���
                enemy_atacked = true;
            }
        }


    }


}
