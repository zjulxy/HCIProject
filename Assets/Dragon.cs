using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour {
	public bool isOver = false;
	public int score = 0;
    public int UpDown =0;
    public int LeftRight = 0;
	public AudioClip AC;
    public float CollisionStep=0.005f;



	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update () {

	}

    // 判断是否发生碰撞
    // 输入：是否是金币，所有的位置信息，物体
    public void JudgeColli(bool isCoin, List<Vector3> allPosi,List<GameObject> allObjects)
    {
        Vector3 MyPosi = GameObject.Find("SJ001").transform.position;
        if (isCoin) { 
            int totalNum = allPosi.Count;
            for(int i = 0; i < totalNum; i++)
            {
                // 比较x y是否完全一致
                if ((allPosi[i].x == MyPosi.x) && (allPosi[i].y == (MyPosi.y+0.015f)))
                { 
                    if(allPosi[i].z>MyPosi.z-CollisionStep && allPosi[i].z < MyPosi.z + CollisionStep )
                    {
                        ScoreManager.score++;
                        score++;
                        allObjects[i].GetComponent<Renderer>().enabled = false;
                        if (ScoreManager.score % 100 == 0)
                        {
                            AudioSource.PlayClipAtPoint(AC, transform.localPosition);
                        }
                    }
                }
            }
        }
        else
        {
            int totalNum = allPosi.Count;
            for (int i = 0; i < totalNum; i++)
            {
                if ((allPosi[i].x == MyPosi.x) && (allPosi[i].y == (MyPosi.y + 0.015f)))
                {
                    if (allPosi[i].z > MyPosi.z - CollisionStep && allPosi[i].z < MyPosi.z + CollisionStep)
                    {
                        isOver = true;
                        this.GetComponent<Animation>().Play("sj001_die");
                        allObjects[i].GetComponent<Renderer>().enabled = false;
                    }
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
		Debug.Log (collision.gameObject.tag);
        // 如果撞到了barrier
		if (collision.gameObject.tag == "Barrier") {
			//isOver = true;
			//Debug.Log ("Game Over");
			ContactPoint contact = collision.contacts [0];
			Quaternion rot = Quaternion.FromToRotation (Vector3.up, contact.normal);
			Vector3 pos = contact.point;
			Debug.Log (collision.gameObject.transform.position);
			//this.GetComponent<Animation> ().Play ("sj001_die");
			this.GetComponent<BoxCollider> ().enabled = false;
		}
        // 如果撞到了金币
        else if (collision.gameObject.tag == "GoldCoin") {
			AudioSource.PlayClipAtPoint(AC, transform.localPosition);  
			collision.gameObject.GetComponent<Renderer>().enabled = false;
			Debug.Log ("score+1");
			//ScoreManager.score++;
			//score++;
		}
    }
}
