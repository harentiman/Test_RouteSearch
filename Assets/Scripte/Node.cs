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

    [Header("���̍s��o�R�n�_"), SerializeField] public List<Node> nextNodeObjList = new List<Node>();

    [Header("�v���C���[�̍Ŋ�o�R�n�_"), HideInInspector] public bool isPlayer;


    void Reset()
    {
        //�o�R�n�_��񃊃X�g�������A��̗v�f�𐶐�����
        nextNodeObjList.Add(null);
    }


    #region �f�o�b�O�@�\
    void OnDrawGizmos()
    {
        // �e�A���̍s��o�R�n�_�Ƃ̐Ԑ���`��
        Gizmos.color = Color.red;

        foreach (Node nextNode in nextNodeObjList)
        {
            if (nextNode != null)
            {
                Gizmos.DrawLine(transform.position, nextNode.transform.position);
            }
        }
    }
    #endregion
}
