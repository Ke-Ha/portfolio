//�G�̍U������ɂ�������X�N���v�g
//2023/7/3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_attack_collider : MonoBehaviour
{
    public bool on_attack_collider; //�U������̃R���C�_�[�ɉ��������Ă�����true�ɂȂ�

    private string player_tag = "Player";

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == player_tag) //�Ώۂ̃^�O��player��������true
        {
            on_attack_collider = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == player_tag)
        {
            on_attack_collider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == player_tag)
        {
            on_attack_collider = false;
        }
    }
}
