using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	public Text txt;
	public static int score;
    private int _ScoreShow;

	// Use this for initialization
	void Start () {
		score = 0;
        _ScoreShow = 0;
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = "COIN " + ((int)score/100).ToString();
	}
}
