using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsBump : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PinHead")
        {//调用GameManager物体中的GameManager脚本组件中的GameOver方法
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }
}

