using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        sr.color = new Color32(185, 150, 150, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
