using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_collider : MonoBehaviour
{
    public bool on_attack_collider;
    void Start()
    {

    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("コライダーの中");
        on_attack_collider = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        on_attack_collider = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        on_attack_collider = false;
    }
}
