using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    //�o�R�n�_��񃊃X�g
    [Header("�s��m�[�h"), SerializeField] public List<Node> nextNodeObjList = new List<Node>();
    //�ŏI�n�_
    [Header("�ŏI�n�_"), SerializeField] public bool isGoal;


    void Reset()
    {
        //�o�R�n�_��񃊃X�g�������A��̗v�f�𐶐�����
        nextNodeObjList.Add(null);
    }
}
