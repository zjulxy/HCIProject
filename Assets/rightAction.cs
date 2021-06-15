using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightAction : MonoBehaviour {

    public Dragon MyDragon;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 按了向左的按钮
    public void bottonTest()
    {
        Vector3 currentPosition = MyDragon.transform.position;
        int Hori = MyDragon.LeftRight;
        //int Verti = MyDragon.UpDown;
        if (Hori != 1)
        {
            Hori += 1;
            MyDragon.LeftRight += 1;
			float NewX = 0.015f+ 0.015f * Hori;
            currentPosition.x = NewX;
            MyDragon.transform.position = currentPosition;
        }

    }
}
