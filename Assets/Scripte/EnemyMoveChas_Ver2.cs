using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMoveChas_Ver2 : MonoBehaviour
{
    public class firstNodePos
    {
        public GameObject firstNodeObjectList; //����̊e�o�R�n�_���
        public float firstNodePosDisList;      //����̊e�o�R�n�_�̍��W��
    }
    List<firstNodePos> firstNodePosList = new List<firstNodePos>();    //����̌o�R�n�_���X�g

    [Header("�ڕW�n�_"), SerializeField] Transform goalTransform;                           //�ڕW�n�_
    [Header("�e��NodeObject"), SerializeField] GameObject nodeObject;                       //�e�̌o�R�n�_
    [Header("�G�l�~�[�̒T�m�͈�"), SerializeField] float detectionRange = Mathf.Infinity;   //�G�l�~�[�̒T���͈�
    //[Header("�G�l�~�[�̈ړ��X�s�[�h"), SerializeField] float speed = 2f;                  //�G�l�~�[�̈ړ��X�s�[�h
    //[Header("�ړI���W�̋��e�͈͒l"), SerializeField] float tolLevPos = 0.01f;             //�ړI���W�̋��e�͈͒l

    //Transform nextNodeTransform;      //���ړ����W

    List<Node> shortNodeObjectList = new List<Node>(); //�ŒZ�o�R�n�_�g�������X�g
    List<Node> tempNodeObjectList = new List<Node>();  //�ꎞ�o�R�n�_�g����
    float shortNodePosDis;  //�ŒZ�o�R�n�_�̍��W�����v
    float tempNodePosDis;   //�ꎞ�ŒZ�n�_�̍��W�����v


    // Start is called before the first frame update
    void Start()
    {
        Node nodeObject = GetFirstNodeObject(); //����o�R�n�_�T������
        GetNextNodeObject(nodeObject);          //�o�R�n�_�T������

        Debug.Log("�ŒZ�o�H�F" + string.Join(", ", shortNodeObjectList) + shortNodePosDis);
    }


    // Update is called once per frame
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
    Node GetFirstNodeObject()
    {
        Node fIrstNoseObject = null;  //����o�R�n�_���
        float firstNodePosDisMin = 0; //����o�R�n�_�̍ŒZ���W��
        int firstNodeNumMin = 0;      //����o�R�n�_�̍ŏ��v�f�ԍ�

        RaycastHit hit;

        //�ڕW�n�_�Ƀ��C���΂��A��Q�����Ȃ���ΖڕW�n�_���ړ��n�_�ɂ���
        if (Physics.Raycast(transform.position, (goalTransform.position - transform.position), out hit, detectionRange))
        {
            //�e�̌o�R�n�_�Ɋi�[���Ă��鐔���A�J��Ԃ�
            for (int i = 0; i < nodeObject.gameObject.transform.childCount; i++)
            {
                //�e�̌o�R�n�_����e�o�R�n�_�̍��W�A���W�����擾
                firstNodePos firstPos = new firstNodePos();

                //�q�̌o�R�n�_�ƌo�R�n�_���i�G�l�~�[�ԁj���擾
                firstPos.firstNodeObjectList = nodeObject.gameObject.transform.GetChild(i).gameObject;
                firstPos.firstNodePosDisList = (Vector3.Distance(transform.position,
                                                firstPos.firstNodeObjectList.gameObject.transform.position));

                //���񃋁[�v�͍��W����K���ێ�
                if (i == 0)
                {
                    firstNodePosDisMin = firstPos.firstNodePosDisList;
                    firstNodeNumMin = i;
                }
                //�ȍ~�̃��[�v�͍��W�����r���A���W�����������l��ێ�
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

            //���̈ړ����W��Node�N���X���擾
            fIrstNoseObject = firstNodePosList[firstNodeNumMin].firstNodeObjectList.GetComponent<Node>();
        }

        return fIrstNoseObject;
    }
    #endregion


    //�o�R�n�_�T������
    void GetNextNodeObject(Node nextNodeObject)
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
            if (nextNodeObject.isGoal)
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
            //nextNodeObject.nextNodeObjList[i].gameObject.SetActive(false);

            //�ŏI�n�_�̏ꍇ�A�������I������
            if (nextNodeObject.isGoal) return;

            //�ċA�֐��Ƃ��Ď��s
            GetNextNodeObject(temp);
        }
    }
}
