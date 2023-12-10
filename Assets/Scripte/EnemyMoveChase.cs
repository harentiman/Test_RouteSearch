using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyMoveChase : MonoBehaviour
{
    [Header("経由地点"), SerializeField] Transform nodeTransform;                 //親経由地点
    [Header("目標地点"), SerializeField] Transform goalTransform;                 //ゴール地点
    [Header("エネミーの移動スピード値"), SerializeField] float speed = 2;         //エネミーの移動スピード値
    //[Header("目的座標の許容範囲値"), SerializeField] float tolLevelPos = 0.01f;   //目的座標の許容範囲値

    Transform[] parentNodeTransform = new Transform[3];    //各経由地点
    float[] parentNodeDisPos = new float[3];               //各経由地点の座標差
    float nextNodeTransform;                               //最小の経由地点の座標差
    int nextNodeNum;                                       //最小の経由地点の要素番号

    bool nodePointReachFLG = false;                        //経由地点到達フラグ


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //親経由地点内の各経由地点座標を取得
        for (int i = 0; i < parentNodeTransform.Length; i++)
        {
            parentNodeTransform[i] = nodeTransform.GetChild(i);
            parentNodeDisPos[i] = Vector3.Distance(transform.position, parentNodeTransform[i].position);

            //初回ループは座標差を必ず保持、
            if (i == 0)
            {
                nextNodeTransform = parentNodeDisPos[i];
                nextNodeNum = i;
            }
            //以降のループは座標差を比較し、座標差が小さい値を保持
            else
            {
                if (nextNodeTransform > parentNodeDisPos[i])
                {
                    nextNodeTransform = parentNodeDisPos[i];
                    nextNodeNum = i;
                }
            }
        }

        //経由地点移動
        transform.position = Vector3.MoveTowards(
             transform.position,
             new Vector3(parentNodeTransform[nextNodeNum].position.x,
                 parentNodeTransform[nextNodeNum].position.y,
                 parentNodeTransform[nextNodeNum].position.z),
                 speed * Time.deltaTime);

        //経由地点の座標差
        float disNextPos = Vector3.Distance(transform.position, parentNodeTransform[nextNodeNum].position);



        //経由地点に到達した場合、
        if (nodePointReachFLG == true)
        {
            //ゴール地点移動
            transform.position = Vector3.MoveTowards(
                 transform.position,
                 new Vector3(goalTransform.position.x, goalTransform.position.y, goalTransform.position.z),
                 speed * Time.deltaTime);
        }
    }
}
