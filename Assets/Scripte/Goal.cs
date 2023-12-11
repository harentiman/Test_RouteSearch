using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public class NodePos
    {
        public GameObject nodeObject; //�e�o�R�n�_���
        public float nodePosDis;      //�e�o�R�n�_�̍��W�ԋ���
    }
    List<NodePos> goalNodePosList = new List<NodePos>();     //�ڕW�̌o�R�n�_���X�g

    [Header("�e�̌o�R�n�_"), SerializeField] GameObject masterNodeObject;
    [Header("�ڕW�̌o�R�n�_"), HideInInspector] public Node goalNodeObject = null;


    void Start()
    {
        goalNodeObject = GetGoalNodeObject();
    }


    #region//�ڕW�̌o�R�n�_�T������
    Node GetGoalNodeObject()
    {
        Node goalNodeObject = null;     //�o�R�n�_���
        float goalNodePosDisMin = 0;    //�o�R�n�_�̍ŒZ���W�ԋ���
        int goalNodeNumMin = 0;         //�o�R�n�_�̍ŒZ�����̗v�f�ԍ�

        //�q�̌o�R�n�_�����A�J��Ԃ�
        for (int i = 0; i < masterNodeObject.gameObject.transform.childCount; i++)
        {
            //�e�o�R�n�_���ƍ��W�ԋ������擾
            NodePos tempNodePos = new NodePos();

            tempNodePos.nodeObject = masterNodeObject.gameObject.transform.GetChild(i).gameObject;
            tempNodePos.nodePosDis = (Vector3.Distance(transform.position,
                                            tempNodePos.nodeObject.gameObject.transform.position));

            //����͕K���ێ�
            if (i == 0)
            {
                goalNodePosDisMin = tempNodePos.nodePosDis;
                goalNodeNumMin = i;
            }
            //�ȍ~�͍��W�ԋ������r�A�������Z�����ɍX�V
            else
            {
                if (goalNodePosDisMin > tempNodePos.nodePosDis)
                {
                    goalNodePosDisMin = tempNodePos.nodePosDis;
                    goalNodeNumMin = i;
                }
            }

            //���W���A���W�ԋ����̑g���������X�g�Ɋi�[
            goalNodePosList.Add(tempNodePos);
        }

        goalNodePosList[goalNodeNumMin].nodeObject.SetActive(false);
        //�ڕW�n�_����ł��߂��o�R�n�_���擾
        goalNodeObject = goalNodePosList[goalNodeNumMin].nodeObject.GetComponent<Node>();

        return goalNodeObject;
    }
    #endregion
}
