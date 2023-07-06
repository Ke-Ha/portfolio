//�^�C�g���V�[���̐���X�N���v�g
//2023/6/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Title_Start : MonoBehaviour
{
    private bool mode_select = false; //���[�h�I����ʂ��ǂ���
    private bool select_mode1 = true; //�J�[�\����mode1�ɍ����Ă��邩�ǂ���
    private bool select_mode2 = false; //�J�[�\����mode2�ɍ����Ă��邩�ǂ���

    private float mode_select_time = 0.0f;

    private GameObject text_mode1_go; 
    private GameObject text_mode2_go;
    private GameObject text_anybutton_go;
    private GameObject cursor_go;
    private TextMeshProUGUI text_mode1;
    private TextMeshProUGUI text_mode2;
    private TextMeshProUGUI text_anybutton;

    private Image cursor_img;
    private RectTransform cursor_rt;

    void Start()
    {
        text_mode1_go = GameObject.Find("1P MODE");
        text_mode2_go = GameObject.Find("2P MODE");
        text_anybutton_go = GameObject.Find("PressAnyButtonText");
        cursor_go = GameObject.Find("Cursor");

        text_mode1=text_mode1_go.GetComponent<TextMeshProUGUI>();
        text_mode2=text_mode2_go.GetComponent<TextMeshProUGUI>();
        text_anybutton = text_anybutton_go.GetComponent<TextMeshProUGUI>();
        cursor_img = cursor_go.GetComponent<Image>();
        cursor_rt = cursor_go.GetComponent<RectTransform>();

        //������ԂŃ��[�h�I����ʂ̕\�����𓧖��ɂ��Ă���
        text_mode1.color = new Color32(255,255,255,0); 
        text_mode2.color = new Color32(255, 255, 255, 0);
        cursor_img.color = new Color32(255, 255, 255, 0);

    }

    void Update()
    {
        if (Input.anyKey && mode_select==false)
        {
            mode_select = true;
        }

        if (mode_select == true) //�����L�[�������ƃ��[�h�I�����o��
        {
            text_mode1.color = new Color32(255, 255, 255, 255);
            text_mode2.color = new Color32(255, 255, 255, 255);
            text_anybutton.color = new Color32(255, 255, 255, 0);
            cursor_img.color = new Color32(255, 255, 255, 255);

            mode_select_time += Time.deltaTime;
            if (mode_select_time >= 0.4f) //���[�h�I����ʂɉf���Ă��班�������L�[���͂��󂯕t���Ȃ�
            {


                if (select_mode1 == true) //�J�[�\����mode1�ɍ����Ă���Ƃ�
                {

                    cursor_rt.anchoredPosition = new Vector2(-131, 0);
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene("mode1_1");
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        select_mode2 = true;
                        select_mode1 = false;
                    }
                }
                else if (select_mode2 == true) //�J�[�\����mode2�ɍ����Ă���Ƃ�
                {
                    cursor_rt.anchoredPosition = new Vector2(-131, -149);
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene("mode2");
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        select_mode1 = true;
                        select_mode2 = false;
                    }

                }
            }


        }
    }
}
