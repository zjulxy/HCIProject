using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	private const float ydis = 0.5f;//y间隔    
	private float[] ypos;//y位置    
	private const float zdis = 2;//z间隔    
	private float[] zpos;//z位置
	private const float xdis = 0.5f;//z间隔    
	private float[] xpos;//z位置
	//private GameObject[] barriers;//所有障碍物
	private List<GameObject> barriers;//所有障碍物
    private List<Vector3> barrierPos;
	private float delta;//移动距离
	private float movespeed;
	private float lastscore=0;
	private int barriercount = 5;
    public Dragon MyDragon;
    public bool IfPause = false;
    private PauseButtons PauseButton=new PauseButtons();

    // Use this for initialization
    void Start () {
		//初始化队列
		barriers = new List<GameObject>();
        barrierPos = new List<Vector3>();

		//初始化y位置
		ypos = new float[3];
		for (int i = 0; i < 3; i++)
		{
			ypos[i] = ydis * i; //0.3是偏移
		}

		//初始化z位置
		zpos = new float[5];
		for (int i = 0; i < 5; i++)
		{            
			zpos[i] = zdis * (i + 1f); //保证障碍物全在视野外
		}

		//初始化x位置
		xpos = new float[5];
		for (int i = 0; i < 5; i++)
		{            
			xpos[i] = xdis * i; //0.3是偏移
		}

		Debug.Log(xpos);
		Debug.Log(ypos);
		Debug.Log(zpos);

		//移入视野前产生5列障碍物，确保移动过程中能无缝衔接
		for (int i = 0; i < 5; i++)
		{
			int count = 0; //每列的障碍物数量

			//每列的三个位置
			bool[] state = { false, false, false,false, false, false,false, false, false };
			for (int j = 0; j < 9; j++)
			{
				//随机一个位置
				int rpos = (int)Random.Range(0, 9);
				//该位置必须没有放置过
				while (state[rpos])
				{
					rpos = (int)Random.Range(0, 9);
				}
				state[rpos] = true;

				if (i == 0 && rpos == 0)
					continue;

				/*
				//在该位置随机产生或不产生障碍物
				float rand = Random.Range(0, 1);
				Debug.Log(xpos);
				if (rand < 0) {
					//加载预制障碍物
					GameObject barrier = (GameObject)Instantiate (Resources.Load ("Barrier", typeof(GameObject)), 
						new Vector3 (xpos[rpos%3], ypos [rpos/3], zpos [i]), Quaternion.identity, null);                    
					barrier.transform.parent = this.transform;
					barrier.transform.position *= 0.03f;
					barrier.transform.localScale *= 0.03f;
					//barrier.GetComponent<BoxCollider>().center = barrier.transform.position;
					barriers.Enqueue (barrier);
					count++;
				}

				//每列的障碍物不能超过2个
				if (count >= barriercount) break;*/
			}
		}

		//初始化距离
		delta = 0;
		movespeed = 0.0006f;
		lastscore = 0;
	}

	// Update is called once per frame
	void Update () {
        if (!Globle.IfPause) { 
		    if (lastscore < ScoreManager.score) {
			    lastscore = ScoreManager.score;
			    if (ScoreManager.score/2000 < 4 && ScoreManager.score % 20000 == 0) {
				    movespeed += ScoreManager.score / 2000 * 0.0001f;
				    barriercount += 1;
			    };
		    }

		    //移动

		    //遍历队列
		    int len = barriers.Count;
		    while (len > 0)
		    {
			    //取出第一个
			    GameObject front = barriers[0];
                Vector3 frontPos = barrierPos[0];
			    front.transform.position -= new Vector3(0, 0, movespeed);           
                frontPos -= new Vector3(0, 0, movespeed);
                barriers.RemoveAt(0);
                barrierPos.RemoveAt(0);

			    //Debug.Log("front pos: " + front.transform.position);

			    //消除移出视野的障碍物                       
			    if (front.transform.position.z < -1 * 0.03f)
			    {
				    Destroy(front);
			    }
			    //否则插回队列尾部
			    else
			    {
				    barriers.Add(front);
                    barrierPos.Add(frontPos);
			    }

			    len--;
		    }

		    //移动距离增加，注意相对坐标和绝对坐标的转换
		    delta += movespeed / 0.03f;

		    //如果移动了一个z间距，那么并产生新一列放在最后 
		    if (delta >= zdis)
		    {
			    Debug.Log(delta);
			    int count = 0; //每列的障碍物数量

			    //每列的三个位置
			    bool[] state = { false, false, false,false, false, false,false, false, false };
			    for (int j = 0; j < 9; j++)
			    {
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
				    if (rand < 0.5) {
					    //加载预制障碍物
					    GameObject barrier = (GameObject)Instantiate (Resources.Load ("Barrier", typeof(GameObject)), 
						    new Vector3 (xpos[rpos%3], ypos [rpos/3], zpos [4]), Quaternion.identity, null);                    
					    barrier.transform.parent = this.transform;
					    barrier.transform.position *= 0.03f;
					    barrier.transform.localScale *= 0.03f;
					    //barrier.GetComponent<BoxCollider>().center = barrier.transform.position;
					    barriers.Add (barrier);
                        barrierPos.Add(barrier.transform.position); 
                        count++;
				    }


				    //每列的障碍物不能超过2个
				    if (count >= barriercount) break;
			    }

			    delta = 0;
		    }
        }
        MyDragon.JudgeColli(false, barrierPos, barriers);
        // PauseManager那里一直回传false，除非你希望暂停它，就回传true，暂停结束后，回传false
        //IfPause = PauseButton.GetPause();
    }


}

