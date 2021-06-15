using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour {

	private const float ydis = 0.5f;//y间隔    
	private float[] ypos;//y位置   
	private const float zdis = 2;//z间隔    
	private float[] zpos;//z位置
	private const float xdis = 0.5f;//z间隔    
	private float[] xpos;//z位置
	private float delta;//移动距离
	private float movespeed;
	private float lastscore;
    public bool IfPause = false;
    public Dragon MyDragon;
    private List<Vector3> allGoldPos;
    // private Queue<Vector3> allGoldPos; // 存所有的gold 
    //private PauseButtons PauseButton = new PauseButtons();

	private List<GameObject> coins;//所有金币

	// Use this for initialization
	void Start () {
		//初始化队列 
		coins = new List<GameObject>();
        allGoldPos = new List<Vector3>();

		//初始化y位置
		ypos = new float[3];
		for (int i = 0; i < 3; i++)
		{
			ypos[i] = ydis * i; //0.2是偏移
		}

		//初始化z位置
		zpos = new float[5];
		for (int i = 0; i < 5; i++)
		{            
			zpos[i] = zdis * (i + 1f) + zdis/2; //保证障碍物全在视野外
		}

		xpos = new float[5];
		for (int i = 0; i < 5; i++)
		{            
			xpos[i] = xdis * i; //0.3是偏移
		}

		//移入视野前产生5列障碍物，确保移动过程中能无缝衔接
		for (int i = 0; i < 5; i++)
		{
			int count = 0; //每列的障碍物数量

			//每列的三个位置
			bool[] state = { false, false, false, false, false, false,false, false, false };
			for (int j = 0; j < 9; j++)
			{
				count = 0;
				//随机一个位置
				int rpos = (int)Random.Range(0, 9);
				//该位置必须没有放置过
				while (state[rpos])
				{
					rpos = (int)Random.Range(0, 9);
				}
				state[rpos] = true;

				/*
				//在该位置随机产生或不产生障碍物
				float rand = Random.Range(0, 1);
				if(rand < 0.3)
				{
					//增加金币
					GameObject coin = (GameObject)Instantiate(Resources.Load("GoldCoin", typeof(GameObject)), 
						new Vector3(xpos[rpos%3],  ypos [rpos/3], zpos [i]), Quaternion.identity, null);                    
					coin.transform.parent = this.transform;
					coin.transform.position *= 0.03f;
					coin.transform.localScale *= 0.03f;
					//coin.GetComponent<BoxCollider>().center = coin.transform.position;
					coins.Enqueue(coin);
					count++;
				}
				*/

				if (count >= 2) break;

			}
		}

		//初始化距离
		delta = 0;
		movespeed = 0.0006f;
		lastscore = 0;
	}


	// Update is called once per frame
	void Update () {
        // 如果不按暂停
        if (!Globle.IfPause) {
		    if (lastscore < ScoreManager.score) {
			    lastscore = ScoreManager.score;
			    if (ScoreManager.score/2000 < 4 && ScoreManager.score % 2000 == 0) {
					movespeed += ScoreManager.score / 2000 * 0.0001f;
			    };
		    }
		    //移动

		    //遍历队列
		    int len0 = coins.Count;
		    while (len0 > 0)
		    {
			    //取出第一个
			    GameObject front = coins[0];
                //同步维护位置queue
                Vector3 frontPos = allGoldPos[0];
			    front.transform.position -= new Vector3(0, 0, movespeed);      
                frontPos-= new Vector3(0, 0, movespeed);
                coins.RemoveAt(0);
                allGoldPos.RemoveAt(0);

                //消除移出视野的金币                     
                if (front.transform.position.z < -1 * 0.03f)
			    {
				    Destroy(front);
			    }
			    //否则插回队列尾部
			    else
			    {
				    coins.Add(front);
                    allGoldPos.Add(frontPos);
			    }

			    len0--;
		    }


		    //移动距离增加，注意相对坐标和绝对坐标的转换
		    delta += movespeed / 0.03f;

		    //如果移动了一个z间距，那么并产生新一列放在最后 
		    if (delta >= zdis)
		    {
			    Debug.Log(delta);
			    int count = 0; //每列的障碍物数量

			    //每列的三个位置
			    bool[] state = { false, false, false, false, false, false,false, false, false };
			    for (int j = 0; j < 9; j++)
			    {
				    count = 0;
				    //随机一个位置
				    int rpos = (int)Random.Range(0, 9);
				    //该位置必须没有放置过
				    while (state[rpos])
				    {
					    rpos = (int)Random.Range(0, 9);
				    }
				    state[rpos] = true;


				    //在该位置随机产生或不产生障碍物
				    float rand = Random.Range(0, 1);
				    if(rand < 0.5)
				    {
					    //增加金币
					    GameObject coin = (GameObject)Instantiate(Resources.Load("GoldCoin", typeof(GameObject)), 
						    new Vector3(xpos[rpos%3],  ypos [rpos/3], zpos [4]), Quaternion.identity, null);                    
					    coin.transform.parent = this.transform;
					    coin.transform.position *= 0.03f;
					    coin.transform.localScale *= 0.03f;
					    //coin.GetComponent<BoxCollider>().center = coin.transform.position;
					    coins.Add(coin);
                        allGoldPos.Add(coin.transform.position);
					    count++;
				    }


				    //每列的金币不能超过2个
				    if (count >= 1) break;
			    }

			    delta = 0;
		    }
        }
        // 判断龙是否和金币碰撞
        MyDragon.JudgeColli(true, allGoldPos,coins);
        // PauseManager那里一直回传false，除非你希望暂停它，就回传true，暂停结束后，回传false
        //IfPause = PauseButton.GetPause();
    }


}

