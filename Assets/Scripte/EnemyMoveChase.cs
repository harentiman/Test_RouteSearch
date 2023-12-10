using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyMoveChase : MonoBehaviour
{
    [Header("�o�R�n�_"), SerializeField] Transform nodeTransform;                 //�e�o�R�n�_
    [Header("�ڕW�n�_"), SerializeField] Transform goalTransform;                 //�S�[���n�_
    [Header("�G�l�~�[�̈ړ��X�s�[�h�l"), SerializeField] float speed = 2;         //�G�l�~�[�̈ړ��X�s�[�h�l
    //[Header("�ړI���W�̋��e�͈͒l"), SerializeField] float tolLevelPos = 0.01f;   //�ړI���W�̋��e�͈͒l

    Transform[] parentNodeTransform = new Transform[3];    //�e�o�R�n�_
    float[] parentNodeDisPos = new float[3];               //�e�o�R�n�_�̍��W��
    float nextNodeTransform;                               //�ŏ��̌o�R�n�_�̍��W��
    int nextNodeNum;                                       //�ŏ��̌o�R�n�_�̗v�f�ԍ�

    bool nodePointReachFLG = false;                        //�o�R�n�_���B�t���O


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�e�o�R�n�_���̊e�o�R�n�_���W���擾
        for (int i = 0; i < parentNodeTransform.Length; i++)
        {
            parentNodeTransform[i] = nodeTransform.GetChild(i);
            parentNodeDisPos[i] = Vector3.Distance(transform.position, parentNodeTransform[i].position);

            //���񃋁[�v�͍��W����K���ێ��A
            if (i == 0)
            {
                nextNodeTransform = parentNodeDisPos[i];
                nextNodeNum = i;
            }
            //�ȍ~�̃��[�v�͍��W�����r���A���W�����������l��ێ�
            else
            {
                if (nextNodeTransform > parentNodeDisPos[i])
                {
                    nextNodeTransform = parentNodeDisPos[i];
                    nextNodeNum = i;
                }
            }
        }

        //�o�R�n�_�ړ�
        transform.position = Vector3.MoveTowards(
             transform.position,
             new Vector3(parentNodeTransform[nextNodeNum].position.x,
                 parentNodeTransform[nextNodeNum].position.y,
                 parentNodeTransform[nextNodeNum].position.z),
                 speed * Time.deltaTime);

        //�o�R�n�_�̍��W��
        float disNextPos = Vector3.Distance(transform.position, parentNodeTransform[nextNodeNum].position);



        //�o�R�n�_�ɓ��B�����ꍇ�A
        if (nodePointReachFLG == true)
        {
            //�S�[���n�_�ړ�
            transform.position = Vector3.MoveTowards(
                 transform.position,
                 new Vector3(goalTransform.position.x, goalTransform.position.y, goalTransform.position.z),
                 speed * Time.deltaTime);
        }
    }
}
