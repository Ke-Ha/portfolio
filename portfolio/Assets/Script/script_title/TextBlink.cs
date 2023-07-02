//�^�C�g���V�[���̃e�L�X�g��_�ł����鏈��
//2023/6/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextBlink : MonoBehaviour
{
    [Header("�_�Ŏ���")] public float blink_cycle = 1.0f;

    private TextMeshProUGUI text; //���̃X�N���v�g���������Ă���Q�[���I�u�W�F�N�g�̃e�L�X�g
    private float time; //����

    void Start()
    {
        text = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.color = GetAlphaColor(text.color);
    }

    Color GetAlphaColor(Color color)//�����x�𑀍삵�ē_�ł�����
    {
        time += Time.deltaTime * 5.0f * blink_cycle;
        color.a = 1/Mathf.Sin(time);

        return color;
    }
}