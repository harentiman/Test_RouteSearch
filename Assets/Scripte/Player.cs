using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    List<Node.NodePos> playerNodePosList = new List<Node.NodePos>();     //目標の経由地点リスト

    [Header("親の経由地点情報"), SerializeField] GameObject masterNodeObject;

    [Header("目標の経由地点"), HideInInspector] public Node playerNodeObject;


    private void Awake()
    {
        playerNodeObject = GetPlayerNode();
    }


    #region//目標経由地点探索処理
    Node GetPlayerNode()
    {
        Node playerNodeObject = null;     //経由地点情報
        float playerNodePosDisMin = 0;    //経由地点の最短座標間距離
        int playerNodeNumMin = 0;         //経由地点の最短距離の要素番号

        //前回のプレイヤーの最寄経由地点を戻す

        //子の経由地点数分、繰り返す
        for (int i = 0; i < masterNodeObject.gameObject.transform.childCount; i++)
        {
            //各経由地点情報と座標間距離を取得
            Node.NodePos tempNodePos = new Node.NodePos();

            tempNodePos.nodeObject = masterNodeObject.gameObject.transform.GetChild(i).gameObject;
            tempNodePos.nodePosDis = (Vector3.Distance(transform.position,
                                            tempNodePos.nodeObject.gameObject.transform.position));

            //初回は必ず保持
            if (i == 0)
            {
                playerNodePosDisMin = tempNodePos.nodePosDis;
                playerNodeNumMin = i;
            }
            //以降は座標間距離を比較、距離が短い方に更新
            else
            {
                if (playerNodePosDisMin > tempNodePos.nodePosDis)
                {
                    playerNodePosDisMin = tempNodePos.nodePosDis;
                    playerNodeNumMin = i;
                }
            }

            //座標情報、座標間距離の組合せをリストに格納
            playerNodePosList.Add(tempNodePos);
        }

        //目標地点から最も近い経由地点を取得
        playerNodeObject = playerNodePosList[playerNodeNumMin].nodeObject.GetComponent<Node>();
        //プレイヤーの最寄経由地点を立てる
        playerNodeObject.isPlayer = true;

        return playerNodeObject;
    }
    #endregion
}
