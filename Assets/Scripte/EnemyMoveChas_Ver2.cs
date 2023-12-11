using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMoveChas_Ver2 : MonoBehaviour
{
    public class firstNodePos
    {
        public GameObject firstNodeObjectList; //初回の各経由地点情報
        public float firstNodePosDisList;      //初回の各経由地点の座標差
    }
    List<firstNodePos> firstNodePosList = new List<firstNodePos>();    //初回の経由地点リスト

    [Header("目標地点"), SerializeField] Transform goalTransform;                           //目標地点
    [Header("親のNodeObject"), SerializeField] GameObject nodeObject;                       //親の経由地点
    [Header("エネミーの探知範囲"), SerializeField] float detectionRange = Mathf.Infinity;   //エネミーの探索範囲
    //[Header("エネミーの移動スピード"), SerializeField] float speed = 2f;                  //エネミーの移動スピード
    //[Header("目的座標の許容範囲値"), SerializeField] float tolLevPos = 0.01f;             //目的座標の許容範囲値

    //Transform nextNodeTransform;      //次移動座標

    List<Node> shortNodeObjectList = new List<Node>(); //最短経由地点組合せリスト
    List<Node> tempNodeObjectList = new List<Node>();  //一時経由地点組合せ
    float shortNodePosDis;  //最短経由地点の座標差合計
    float tempNodePosDis;   //一時最短地点の座標差合計


    // Start is called before the first frame update
    void Start()
    {
        Node nodeObject = GetFirstNodeObject(); //初回経由地点探索処理
        GetNextNodeObject(nodeObject);          //経由地点探索処理

        Debug.Log("最短経路：" + string.Join(", ", shortNodeObjectList) + shortNodePosDis);
    }


    // Update is called once per frame
    void Update()
    {
        #region
        ////目的地点に移動
        //transform.position = Vector3.MoveTowards(
        //        transform.position,
        //        new Vector3(nextNodeTransform.position.x,
        //            nextNodeTransform.position.y,
        //            nextNodeTransform.position.z),
        //            speed * Time.deltaTime);

        ////現在地点の座標差を計算
        //float disNextPos = Vector3.Distance(transform.position, nextNodeTransform.position);

        //// 次移動先ノードを更新
        //if (disNextPos < tolLevPos)
        //{
        //    Node node = nextNodeTransform.GetComponent<Node>();
        //    //nextNodeTransform = node.nextNodeTransform[0];
        //}
        #endregion
    }


    #region //初回経由地点探索処理
    Node GetFirstNodeObject()
    {
        Node fIrstNoseObject = null;  //初回経由地点情報
        float firstNodePosDisMin = 0; //初回経由地点の最短座標差
        int firstNodeNumMin = 0;      //初回経由地点の最小要素番号

        RaycastHit hit;

        //目標地点にレイを飛ばし、障害物がなければ目標地点を移動地点にする
        if (Physics.Raycast(transform.position, (goalTransform.position - transform.position), out hit, detectionRange))
        {
            //親の経由地点に格納している数分、繰り返す
            for (int i = 0; i < nodeObject.gameObject.transform.childCount; i++)
            {
                //親の経由地点から各経由地点の座標、座標差を取得
                firstNodePos firstPos = new firstNodePos();

                //子の経由地点と経由地点差（エネミー間）を取得
                firstPos.firstNodeObjectList = nodeObject.gameObject.transform.GetChild(i).gameObject;
                firstPos.firstNodePosDisList = (Vector3.Distance(transform.position,
                                                firstPos.firstNodeObjectList.gameObject.transform.position));

                //初回ループは座標差を必ず保持
                if (i == 0)
                {
                    firstNodePosDisMin = firstPos.firstNodePosDisList;
                    firstNodeNumMin = i;
                }
                //以降のループは座標差を比較し、座標差が小さい値を保持
                else
                {
                    if (firstNodePosDisMin > firstPos.firstNodePosDisList)
                    {
                        firstNodePosDisMin = firstPos.firstNodePosDisList;
                        firstNodeNumMin = i;
                    }
                }

                firstNodePosList.Add(firstPos);
            }
            //firstNodePosList[firstNodeNumMin].firstNodeObjectList.SetActive(false);

            //次の移動座標のNodeクラスを取得
            fIrstNoseObject = firstNodePosList[firstNodeNumMin].firstNodeObjectList.GetComponent<Node>();
        }

        return fIrstNoseObject;
    }
    #endregion


    //経由地点探索処理
    void GetNextNodeObject(Node nextNodeObject)
    {
        //次の座標地点に格納されている要素数分、繰り返す
        for (int i = 0; i < nextNodeObject.nextNodeObjList.Count; i++)
        {
            //一時リストに座標情報と座標間距離を格納
            tempNodeObjectList.Add(nextNodeObject);
            if (nextNodeObject.nextNodeObjList[i] != null)
            {
                tempNodePosDis += Vector3.Distance(nextNodeObject.transform.position,
                                                    nextNodeObject.nextNodeObjList[i].transform.position);
            }

            //最終地点の場合、最短経路リストとの比較、更新を行う
            if (nextNodeObject.isGoal)
            {
                //最短経路リストが空の場合、初回格納を行う
                if (shortNodeObjectList.Count == 0)
                {
                    shortNodeObjectList.AddRange(tempNodeObjectList);
                    shortNodePosDis = tempNodePosDis;
                }
                //初回格納以降の場合、最短経路リストの更新を行う
                else
                {
                    //最短経路リストと一時リストの要素数が一致しない場合、要素数を合わせる
                    if (shortNodeObjectList.Count > tempNodeObjectList.Count)
                    {
                        //不足している要素数を算出、不足箇所に要素番号を最短経路リストから挿入
                        int elementsToAdd = shortNodeObjectList.Count - tempNodeObjectList.Count;

                        for (int n = 0; n < elementsToAdd; n++)
                        {
                            tempNodeObjectList.Insert(0, shortNodeObjectList[n]);
                        }
                    }
                    //現在の座標間距離合計の方が距離が短い場合、最短座標間距離合計を更新する
                    if (shortNodePosDis > tempNodePosDis)
                    {
                        shortNodeObjectList.Clear();
                        shortNodeObjectList.AddRange(tempNodeObjectList);
                        shortNodePosDis = tempNodePosDis;
                    }
                }

                Debug.Log("経路一覧：" + string.Join(", ", tempNodeObjectList) + tempNodePosDis);
                //一時リスト、座標間距離をクリア
                tempNodeObjectList.Clear();
                tempNodePosDis = 0;
            }

            //次の移動座標を取得する
            Node temp = nextNodeObject.nextNodeObjList[i];
            //nextNodeObject.nextNodeObjList[i].gameObject.SetActive(false);

            //最終地点の場合、処理を終了する
            if (nextNodeObject.isGoal) return;

            //再帰関数として実行
            GetNextNodeObject(temp);
        }
    }
}
