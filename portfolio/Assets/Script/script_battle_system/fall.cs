//����̉��̃R���C�_�[�ɂ���X�N���v�g
//2023/7/6

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall : MonoBehaviour
{
    public bool playerwin = false; //�v���C���[�������������ǂ���
    public bool enemywin = false; //�G�������������ǂ���

    private string player_tag = "Player";
    private string enemy_tag = "Enemy";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision) //����̉��̃R���C�_�[���牽�����o����
    {
        if (collision.gameObject.tag == player_tag) //���̃R���C�_�[����o�����̂�player�^�O��������
        {
            enemywin = true; //�G�̏�����Ԃ�true�ɂ���
        }
        if (collision.gameObject.tag == enemy_tag) //���̃R���C�_�[����o�����̂�enemy�^�O��������
        {
            playerwin = true; //�v���C���[�̏�����Ԃ�true�ɂ���
        }

    }
}
