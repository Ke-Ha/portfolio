//�^�C�g���V�[���̐���X�N���v�g
//2023/6/30
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Start : MonoBehaviour
{
    private bool firstpush=false; //�V�[���`�F���W��K��1�񂾂��s�����߂̕ϐ�


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKey && firstpush==false) //�V�[���`�F���W��K����񂾂��s������
        {
            firstpush = true;
        }

        if (firstpush==true)
        {
            SceneManager.LoadScene("Stage1");
        }
    }
}
