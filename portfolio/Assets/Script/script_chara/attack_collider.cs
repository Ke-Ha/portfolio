//player�̍U������ɂ�������X�N���v�g
//2023/6/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class attack_collider : MonoBehaviour
{


    public bool on_attack_collider; //�U������̃R���C�_�[�ɉ��������Ă�����true�ɂȂ�

    private string player_tag = "Player";
    private string enemy_tag = "Enemy";
    private string player1_tag = "Player1";
    private string player2_tag = "Player2";
    private string now_scene;

    void Start()
    {
        now_scene=SceneManager.GetActiveScene().name; //���̃V�[�������擾
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���̃��[�h��2p���[�h��������
        if (now_scene == "mode2")
        {
            //�Ώۂ̃^�O��player1��player2��������true
            if (collision.gameObject.tag == player2_tag || collision.gameObject.tag == player1_tag) on_attack_collider = true;
        }
        else
        {
            //�Ώۂ̃^�O��Player��Enemy��������true
            if (collision.gameObject.tag==player_tag || collision.gameObject.tag == enemy_tag) on_attack_collider = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (now_scene == "mode2")
        {
            if (collision.gameObject.tag == player2_tag || collision.gameObject.tag == player1_tag) on_attack_collider = true;
        }
        else
        {
            if (collision.gameObject.tag == player_tag || collision.gameObject.tag == enemy_tag) on_attack_collider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (now_scene == "mode2")
        {
            if (collision.gameObject.tag == player2_tag || collision.gameObject.tag == player1_tag) on_attack_collider = false;
        }
        else
        {
            if (collision.gameObject.tag == player_tag || collision.gameObject.tag == enemy_tag) on_attack_collider = false;
        }
    }
}
