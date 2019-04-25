using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinsMove : MonoBehaviour {

    public float pinSpeed = 20;

    private bool isIn = false;
    //判断是否飞入
    private bool isReady = false;
    //判断是否到达start point
    private Transform startPt;
    private Transform circle0pt;

	// Use this for initialization
	void Start () {
        startPt = GameObject.Find("StartPoint").transform;
        circle0pt = GameObject.Find("Circle").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isIn)
        {
            if (!isReady)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPt.position, pinSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position,startPt.position)<0.01f)
                {
                    isReady = true;
                }
            }
        }
        else if (Vector3.Distance(transform.position, circle0pt.position) >= 1.55f) 
        {//如果针的距离与圆心的距离大于1.55(大圆直径减去小圆直径)则针还未到达圆的边缘
            transform.position = Vector3.MoveTowards(transform.position, circle0pt.position, pinSpeed * Time.deltaTime);

        }
        else
        {
            transform.parent = circle0pt;
            isIn = false;
        }
		
	}

    public void InfixPins()
    {
        isIn = true;
        isReady = true;
    }
}
