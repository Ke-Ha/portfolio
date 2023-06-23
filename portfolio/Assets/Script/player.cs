using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;//�ړ��X�s�[�h

    private Animator anim;
    private Rigidbody2D rb;

    private float horizontalKey=0;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalKey = Input.GetAxis("Horizontal");//�uHorizontal�v�L�[�i����́��A���j�̓��͂�float�l�Ŏ擾����@�������A������
        float xSpeed = 0.0f;

        if (horizontalKey>0)
        {
            anim.SetBool("walk", true);
            transform.localScale = new Vector3(1, 1, 1);
            xSpeed = speed;

        }
        else if (horizontalKey<0)
        {
            anim.SetBool("walk", true);
            transform.localScale = new Vector3(-1,1,1);
            xSpeed = -speed;
        }
        else
        {
            anim.SetBool("walk", false);
        }
        rb.velocity = new Vector2(xSpeed, rb.velocity.y);
    }
}
