using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Start : MonoBehaviour
{
    private bool firstpush=false;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKey && firstpush==false)
        {
            firstpush = true;
        }

        if (firstpush==true)
        {
            SceneManager.LoadScene("Stage1");
        }
    }
}
