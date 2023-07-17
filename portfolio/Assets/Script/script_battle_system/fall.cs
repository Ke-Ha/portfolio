//����̉��̃R���C�_�[�ɂ���X�N���v�g
//2023/7/6

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fall : MonoBehaviour
{
    //1p���[�h�̏�������
    public bool playerwin = false;
    public bool enemywin = false;
    //2p���[�h�̏�������
    public bool player1win = false;
    public bool player2win = false;
    public bool win_state;//�����ꂩ�̏������肪true���ǂ���


    private string player_tag = "Player";
    private string enemy_tag = "Enemy";
    private string player1_tag = "Player1";
    private string player2_tag = "Player2";

    private bool p2_mode;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "mode2")
        {
            p2_mode = true;
        }
    }

    void Update()
    {
        if (playerwin || enemywin || player1win || player2win) win_state = true;
    }

    private void OnTriggerExit2D(Collider2D collision) //����̉��̃R���C�_�[���牽�����o����
    {
        if (p2_mode==true) //2p���[�h�̂Ƃ�
        {
            //���̃R���C�_�[����o���I�u�W�F�N�g�̃^�O�𔻒肷��
            if (collision.gameObject.tag == player1_tag) player1win = true; 

            if (collision.gameObject.tag == player2_tag) player2win = true;
        }
        else //2p���[�h�łȂ��Ƃ�
        {
            if (collision.gameObject.tag == player_tag) enemywin = true;
            
            if (collision.gameObject.tag == enemy_tag) playerwin = true;
        }


    }
}
