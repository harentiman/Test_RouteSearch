using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    List<Node.NodePos> playerNodePosList = new List<Node.NodePos>();     //�ڕW�̌o�R�n�_���X�g

    [Header("�e�̌o�R�n�_���"), SerializeField] GameObject masterNodeObject;
    //[Header("�����Ԋu����"), SerializeField] float processIntervalTime = 1f;

    [Header("�ڕW�̌o�R�n�_"), HideInInspector] public Node playerNodeObject;
    [Header("�O��̃v���C���[�Ŋ�o�R�n�_"), HideInInspector] public Node lastPlayerNode = null;


    private void Awake()
    {
        playerNodeObject = GetPlayerNode();
        //StartCoroutine(GetPlayerNode());�����������{��
    }


    #region �ڕW�o�R�n�_�T������
    Node GetPlayerNode()
    {
        //�O��̃v���C���[�̍Ŋ�o�R�n�_��߂�
        if (lastPlayerNode != null)
        {
            lastPlayerNode.isPlayer = false;
        }

        Node playerNodeObject = null;     //�o�R�n�_���
        float playerNodePosDisMin = 0;    //�o�R�n�_�̍ŒZ���W�ԋ���
        int playerNodeNumMin = 0;         //�o�R�n�_�̍ŒZ�����̗v�f�ԍ�

        //�q�̌o�R�n�_�����A�J��Ԃ�
        for (int i = 0; i < masterNodeObject.gameObject.transform.childCount; i++)
        {
            //�e�o�R�n�_���ƍ��W�ԋ������擾
            Node.NodePos tempNodePos = new Node.NodePos();

            tempNodePos.nodeObject = masterNodeObject.gameObject.transform.GetChild(i).gameObject;
            tempNodePos.nodePosDis = (Vector3.Distance(transform.position,
                                            tempNodePos.nodeObject.gameObject.transform.position));

            //����͕K���ێ�
            if (i == 0)
            {
                playerNodePosDisMin = tempNodePos.nodePosDis;
                playerNodeNumMin = i;
            }
            //�ȍ~�͍��W�ԋ������r�A�������Z�����ɍX�V
            else
            {
                if (playerNodePosDisMin > tempNodePos.nodePosDis)
                {
                    playerNodePosDisMin = tempNodePos.nodePosDis;
                    playerNodeNumMin = i;
                }
            }

            //���W���A���W�ԋ����̑g���������X�g�Ɋi�[
            playerNodePosList.Add(tempNodePos);
        }

        //�ڕW�n�_����ł��߂��o�R�n�_���擾
        playerNodeObject = playerNodePosList[playerNodeNumMin].nodeObject.GetComponent<Node>();

        //�v���C���[�̍Ŋ�o�R�n�_�𗧂Ă�
        if (playerNodeObject != null)
        {
            playerNodeObject.isPlayer = true;
            lastPlayerNode = playerNodeObject;
            Debug.Log("�v���C���[�Ŋ�o�R�n�_�F" + playerNodeObject);
        }

        return playerNodeObject;
    }
    #endregion


    //#region �ڕW�o�R�n�_�T�����������������{��
    //IEnumerator GetPlayerNode()
    //{
    //    while (true)
    //    {
    //        //�O��̃v���C���[�̍Ŋ�o�R�n�_��߂�
    //        if (lastPlayerNode != null)
    //        {
    //            lastPlayerNode.isPlayer = false;
    //        }

    //        playerNodeObject = null;          //�o�R�n�_���
    //        float playerNodePosDisMin = 0;    //�o�R�n�_�̍ŒZ���W�ԋ���
    //        int playerNodeNumMin = 0;         //�o�R�n�_�̍ŒZ�����̗v�f�ԍ�

    //        //�q�̌o�R�n�_�����A�J��Ԃ�
    //        for (int i = 0; i < masterNodeObject.gameObject.transform.childCount; i++)
    //        {
    //            //�e�o�R�n�_���ƍ��W�ԋ������擾
    //            Node.NodePos tempNodePos = new Node.NodePos();

    //            tempNodePos.nodeObject = masterNodeObject.gameObject.transform.GetChild(i).gameObject;
    //            tempNodePos.nodePosDis = (Vector3.Distance(transform.position,
    //                                            tempNodePos.nodeObject.gameObject.transform.position));

    //            //����͕K���ێ�
    //            if (i == 0)
    //            {
    //                playerNodePosDisMin = tempNodePos.nodePosDis;
    //                playerNodeNumMin = i;
    //            }
    //            //�ȍ~�͍��W�ԋ������r�A�������Z�����ɍX�V
    //            else
    //            {
    //                if (playerNodePosDisMin > tempNodePos.nodePosDis)
    //                {
    //                    playerNodePosDisMin = tempNodePos.nodePosDis;
    //                    playerNodeNumMin = i;
    //                }
    //            }

    //            //���W���A���W�ԋ����̑g���������X�g�Ɋi�[
    //            playerNodePosList.Add(tempNodePos);
    //        }

    //        //�ڕW�n�_����ł��߂��o�R�n�_���擾
    //        playerNodeObject = playerNodePosList[playerNodeNumMin].nodeObject.GetComponent<Node>();

    //        //�v���C���[�̍Ŋ�o�R�n�_�𗧂Ă�
    //        if (playerNodeObject != null)
    //        {
    //            playerNodeObject.isPlayer = true;
    //            lastPlayerNode = playerNodeObject;
    //            Debug.Log("�v���C���[�Ŋ�o�R�n�_�F" + playerNodeObject);
    //        }

    //        yield return new WaitForSeconds(processIntervalTime);
    //    }
    //}
    //#endregion
}
