using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upAction : MonoBehaviour {
    public Dragon MyDragon;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 按了向上的按钮
	public void bottonTest(){
        Vector3 currentPosition= MyDragon.transform.position;
        //int Hori = MyDragon.LeftRight;
        int Verti = MyDragon.UpDown;
        if (Verti != 1) {
            Verti += 1;
            MyDragon.UpDown += 1;
            float NewY = 0.015f * Verti;
            currentPosition.y = NewY;
            MyDragon.transform.position = currentPosition;
        }



        
        //currentPosition.y+=0.003f; //向上移动
       // GameObject.Find("SJ001").transform.position= currentPosition;
	}
}
