using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    public class NodePos    //経由地点クラス
    {
        public GameObject nodeObject; //各経由地点情報
        public float nodePosDis;      //各経由地点の座標間距離
    }

    [Header("行先ノード"), SerializeField] public List<Node> nextNodeObjList = new List<Node>();
    [Header("最終地点"), SerializeField] public bool isPlayer;


    void Reset()
    {
        //経由地点情報リスト生成時、空の要素を生成する
        nextNodeObjList.Add(null);
    }
}
