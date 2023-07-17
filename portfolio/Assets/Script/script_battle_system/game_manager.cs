//�o�g���V�X�e���֘A�̊Ǘ�������X�N���v�g
//2023/7/7
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    public fall fall_cl;

    private GameObject text_win_go;
    private GameObject text_back_go;
    private TextMeshProUGUI text_win;
    private TextMeshProUGUI text_back;

    private bool win_state=false; //������Ԃ��ǂ���
    
    void Start()
    {
        text_win_go = GameObject.Find("text_win");
        text_back_go = GameObject.Find("text_back");
        text_win = text_win_go.GetComponent<TextMeshProUGUI>();
        text_back = text_back_go.GetComponent<TextMeshProUGUI>();

        text_win.color = new Color32(255, 255, 255, 0);
        text_back.color = new Color32(255, 255, 255, 0);

    }

    void Update()
    {
        if(fall_cl.win_state == true)
        {
            //���b�Z�[�W�\���A������Ԃ�true�ɂ���
            text_win.color = new Color32(255, 255, 255, 255);
            text_back.color = new Color32(255, 255, 255, 255);

            if (fall_cl.playerwin) text_win.text = "Player Win!";
            else if (fall_cl.enemywin) text_win.text = "Enemy Win!";
            else if (fall_cl.player1win) text_win.text = "Player1 Win!";
            else if (fall_cl.player2win) text_win.text = "Player2 Win!";

            if (fall_cl.win_state)
            {
                if (Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene("title");
            }



        }



        if (fall_cl.playerwin == true) //�v���C���[���������Ă����Ƃ�
        {
            //���b�Z�[�W�\���A������Ԃ�true�ɂ���
            text_win.color = new Color32(255, 255, 255, 255);
            text_back.color = new Color32(255, 255, 255, 255);
            win_state = true;
        }
        else if (fall_cl.enemywin == true) //�G���������Ă����Ƃ�
        {
            //���b�Z�[�W�\���A������Ԃ�true�ɂ���
            text_win.color = new Color32(255, 255, 255, 255);
            text_back.color = new Color32(255, 255, 255, 255);


            if (SceneManager.GetActiveScene().name == "mode1_1") //���݂̃V�[�������擾
            {
                text_win.text = "Enemy Win!";
            }
            else if(SceneManager.GetActiveScene().name == "mode2")
            {
                text_win.text = "Player2 Win!"; 
            }
            win_state = true;
        }

        if (win_state == true) //������Ԃ�true�̂Ƃ��^�C�g���ɖ߂��悤�ɂ���
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("title");
            }
        }
    }
}
