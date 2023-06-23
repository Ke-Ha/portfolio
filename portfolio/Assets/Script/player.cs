using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;//移動スピード

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
        horizontalKey = Input.GetAxis("Horizontal");//「Horizontal」キー（今回は←、→）の入力をfloat値で取得する　←が正、→が負
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
