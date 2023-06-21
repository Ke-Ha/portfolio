using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    
    private Animator anim;
    private float horizontalKey=0;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalKey = Input.GetAxis("Horizontal");//「Horizontal」キー（今回は←、→）の入力をfloat値で取得する　←が正、→が負

        if (horizontalKey>0)
        {
            anim.SetBool("walk", true);
            transform.localScale = new Vector3(1, 1, 1);

        }
        else if (horizontalKey<0)
        {
            anim.SetBool("walk", true);
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }
}
