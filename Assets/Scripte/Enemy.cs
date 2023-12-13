using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    List<Node.NodePos> firstNodePosList = new List<Node.NodePos>();    //����̌o�R�n�_���X�g
    List<Node> shortNodeObjectList = new List<Node>(); //�ŒZ�o�R�n�_���[�g���X�g
    List<Node> tempNodeObjectList = new List<Node>();  //�ꎞ�o�R�n�_���[�g���X�g

    [Header("�ڕW�n�_"), SerializeField] Transform playerTransform;
    [Header("�e�̌o�R�n�_���"), SerializeField] GameObject masterNodeObject;
    [Header("�T�m�͈�"), SerializeField] float detectionRange = Mathf.Infinity;
    //[Header("�ړ��X�s�[�h"), SerializeField] float speed = 2f;
    //[Header("���W�̋��e�덷�l"), SerializeField] float tolLevPos = 0.01f;

    [Header("����̌o�R�n�_"), HideInInspector] public Node firstNodeObject;
    [Header("�ŒZ�o�R�n�_�̍��W�����v"), HideInInspector] float shortNodePosDis;
    [Header("�ꎞ�o�R�n�_�̍��W�����v"), HideInInspector] float tempNodePosDis;
    //[Header("���̈ړ����W"), HideInInspector] Transform nextNodeTransform;


    void Start()
    {
        firstNodeObject = GetFirstNode(); //����o�R�n�_�T������
        GetNextNode(firstNodeObject);     //�o�R�n�_���[�g�T������

        Debug.Log("�ŒZ�o�H�F" + string.Join(", ", shortNodeObjectList) + shortNodePosDis);
    }


    void Update()
    {
        #region
        ////�ړI�n�_�Ɉړ�
        //transform.position = Vector3.MoveTowards(
        //        transform.position,
        //        new Vector3(nextNodeTransform.position.x,
        //            nextNodeTransform.position.y,
        //            nextNodeTransform.position.z),
        //            speed * Time.deltaTime);

        ////���ݒn�_�̍��W�����v�Z
        //float disNextPos = Vector3.Distance(transform.position, nextNodeTransform.position);

        //// ���ړ���m�[�h���X�V
        //if (disNextPos < tolLevPos)
        //{
        //    Node node = nextNodeTransform.GetComponent<Node>();
        //    //nextNodeTransform = node.nextNodeTransform[0];
        //}
        #endregion
    }


    #region //����o�R�n�_�T������
    Node GetFirstNode()
    {
        Node fIrstNoseObject = null;  //�o�R�n�_���
        float firstNodePosDisMin = 0; //�o�R�n�_�̍ŒZ���W�ԋ���
        int firstNodeNumMin = 0;      //�o�R�n�_�̍ŒZ�����̗v�f�ԍ�

        //�ڕW�n�_�Ƀ��C���΂��A
        RaycastHit hit;

        if (Physics.Raycast(transform.position, (playerTransform.position - transform.position), out hit, detectionRange))
        {
            //�q�̌o�R�n�_�����A�J��Ԃ�
            for (int i = 0; i < masterNodeObject.gameObject.transform.childCount; i++)
            {
                //�e�o�R�n�_���ƍ��W�ԋ������擾
                Node.NodePos tempNodePos = new Node.NodePos();

                //�q�̌o�R�n�_�ƌo�R�n�_���i�G�l�~�[�ԁj���擾
                tempNodePos.nodeObject = masterNodeObject.gameObject.transform.GetChild(i).gameObject;
                tempNodePos.nodePosDis = (Vector3.Distance(transform.position,
                                                tempNodePos.nodeObject.gameObject.transform.position));

                //����͕K���ێ�
                if (i == 0)
                {
                    firstNodePosDisMin = tempNodePos.nodePosDis;
                    firstNodeNumMin = i;
                }
                //�ȍ~�͍��W�ԋ������r�A�������Z�����ɍX�V
                else
                {
                    if (firstNodePosDisMin > tempNodePos.nodePosDis)
                    {
                        firstNodePosDisMin = tempNodePos.nodePosDis;
                        firstNodeNumMin = i;
                    }
                }

                //���W���A���W�ԋ����̑g���������X�g�Ɋi�[
                firstNodePosList.Add(tempNodePos);
            }

            //�ڕW�n�_����ł��߂��o�R�n�_���擾
            fIrstNoseObject = firstNodePosList[firstNodeNumMin].nodeObject.GetComponent<Node>();
            Debug.Log("�G�l�~�[�Ŋ�o�R�n�_�F" + fIrstNoseObject);
        }

        return fIrstNoseObject;
    }
    #endregion


    #region //�o�R�n�_���[�g�T������
    void GetNextNode(Node nextNodeObject)
    {
        //���̍��W�n�_�Ɋi�[����Ă���v�f�����A�J��Ԃ�
        for (int i = 0; i < nextNodeObject.nextNodeObjList.Count; i++)
        {
            //�ꎞ���X�g�ɍ��W���ƍ��W�ԋ������i�[
            tempNodeObjectList.Add(nextNodeObject);
            if (nextNodeObject.nextNodeObjList[i] != null)
            {
                tempNodePosDis += Vector3.Distance(nextNodeObject.transform.position,
                                                    nextNodeObject.nextNodeObjList[i].transform.position);
            }

            //�ŏI�n�_�̏ꍇ�A�ŒZ�o�H���X�g�Ƃ̔�r�A�X�V���s��
            if (nextNodeObject.isPlayer)
            {
                //�ŒZ�o�H���X�g����̏ꍇ�A����i�[���s��
                if (shortNodeObjectList.Count == 0)
                {
                    shortNodeObjectList.AddRange(tempNodeObjectList);
                    shortNodePosDis = tempNodePosDis;
                }
                //����i�[�ȍ~�̏ꍇ�A�ŒZ�o�H���X�g�̍X�V���s��
                else
                {
                    //�ŒZ�o�H���X�g�ƈꎞ���X�g�̗v�f������v���Ȃ��ꍇ�A�v�f�������킹��
                    if (shortNodeObjectList.Count > tempNodeObjectList.Count)
                    {
                        //�s�����Ă���v�f�����Z�o�A�s���ӏ��ɗv�f�ԍ����ŒZ�o�H���X�g����}��
                        int elementsToAdd = shortNodeObjectList.Count - tempNodeObjectList.Count;

                        for (int n = 0; n < elementsToAdd; n++)
                        {
                            tempNodeObjectList.Insert(0, shortNodeObjectList[n]);
                        }
                    }
                    //���݂̍��W�ԋ������v�̕����������Z���ꍇ�A�ŒZ���W�ԋ������v���X�V����
                    if (shortNodePosDis > tempNodePosDis)
                    {
                        shortNodeObjectList.Clear();
                        shortNodeObjectList.AddRange(tempNodeObjectList);
                        shortNodePosDis = tempNodePosDis;
                    }
                }

                Debug.Log("�o�H�ꗗ�F" + string.Join(", ", tempNodeObjectList) + tempNodePosDis);
                //�ꎞ���X�g�A���W�ԋ������N���A
                tempNodeObjectList.Clear();
                tempNodePosDis = 0;
            }

            //���̈ړ����W���擾����
            Node temp = nextNodeObject.nextNodeObjList[i];

            //�ŏI�n�_�̏ꍇ�A�������I������
            if (nextNodeObject.isPlayer)
            {
                return;
            }

            //�ċA�֐��Ƃ��Ď��s
            GetNextNode(temp);
        }
    }
    #endregion
}
