using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public class NodePos
    {
        public GameObject nodeObject; //各経由地点情報
        public float nodePosDis;      //各経由地点の座標間距離
    }
    List<NodePos> goalNodePosList = new List<NodePos>();     //目標の経由地点リスト

    [Header("親の経由地点"), SerializeField] GameObject masterNodeObject;
    [Header("目標の経由地点"), HideInInspector] public Node goalNodeObject = null;


    void Start()
    {
        goalNodeObject = GetGoalNodeObject();
    }


    #region//目標の経由地点探索処理
    Node GetGoalNodeObject()
    {
        Node goalNodeObject = null;     //経由地点情報
        float goalNodePosDisMin = 0;    //経由地点の最短座標間距離
        int goalNodeNumMin = 0;         //経由地点の最短距離の要素番号

        //子の経由地点数分、繰り返す
        for (int i = 0; i < masterNodeObject.gameObject.transform.childCount; i++)
        {
            //各経由地点情報と座標間距離を取得
            NodePos tempNodePos = new NodePos();

            tempNodePos.nodeObject = masterNodeObject.gameObject.transform.GetChild(i).gameObject;
            tempNodePos.nodePosDis = (Vector3.Distance(transform.position,
                                            tempNodePos.nodeObject.gameObject.transform.position));

            //初回は必ず保持
            if (i == 0)
            {
                goalNodePosDisMin = tempNodePos.nodePosDis;
                goalNodeNumMin = i;
            }
            //以降は座標間距離を比較、距離が短い方に更新
            else
            {
                if (goalNodePosDisMin > tempNodePos.nodePosDis)
                {
                    goalNodePosDisMin = tempNodePos.nodePosDis;
                    goalNodeNumMin = i;
                }
            }

            //座標情報、座標間距離の組合せをリストに格納
            goalNodePosList.Add(tempNodePos);
        }

        goalNodePosList[goalNodeNumMin].nodeObject.SetActive(false);
        //目標地点から最も近い経由地点を取得
        goalNodeObject = goalNodePosList[goalNodeNumMin].nodeObject.GetComponent<Node>();

        return goalNodeObject;
    }
    #endregion
}
