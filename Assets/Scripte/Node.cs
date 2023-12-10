using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    //経由地点情報リスト
    [Header("行先ノード"), SerializeField] public List<Node> nextNodeObjList = new List<Node>();
    //最終地点
    [Header("最終地点"), SerializeField] public bool isGoal;


    void Reset()
    {
        //経由地点情報リスト生成時、空の要素を生成する
        nextNodeObjList.Add(null);
    }
}
