using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextBlink : MonoBehaviour
{
    [Header("点滅周期")] public float blink_cycle = 1.0f;

    private TextMeshProUGUI text; //このスクリプトをくっつけてあるゲームオブジェクトのテキスト
    private float time; //時間

    void Start()
    {
        text = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.color = GetAlphaColor(text.color);
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * blink_cycle;
        color.a = 1/Mathf.Sin(time);

        return color;
    }
}
