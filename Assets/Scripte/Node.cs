using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    public class NodePos    //�o�R�n�_�N���X
    {
        public GameObject nodeObject; //�e�o�R�n�_���
        public float nodePosDis;      //�e�o�R�n�_�̍��W�ԋ���
    }

    [Header("�s��m�[�h"), SerializeField] public List<Node> nextNodeObjList = new List<Node>();
    [Header("�ŏI�n�_"), SerializeField] public bool isPlayer;


    void Reset()
    {
        //�o�R�n�_��񃊃X�g�������A��̗v�f�𐶐�����
        nextNodeObjList.Add(null);
    }
}
