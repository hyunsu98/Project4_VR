using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ǥ
//��� �� ����Ʈ�� ��� �÷��� �ϸ� �� ��ü�� �÷��� �Լ��� ����
//���� ��ȭ ��ư�� ������ �� ����� ����� ��ü�� �ִٸ�
//�÷��� �� �� �ְ�
//���� ��ȭ //�ʴ� �÷��� �� !!!!

//��ȭ ���
public class RecSet : MonoBehaviour
{
    // ��� �� ����Ʈ 
    [SerializeField]
    PlayerRecord recrod;

    //����� �������� 
    //����� �÷��̰� �ִٸ� �����Ű��
    //������� �ʾ����� ��ȭ��Ų��.

    //��ȭ ����
    //�ڽ� ��ü�� player

   PlayerMove pm;
    public Transform mainPlayer;

    public void Start()
    {
        pm = this.GetComponent<PlayerMove>();
    }

    public void OnRecordStart()
    {
        //�ڽĿ� �پ��ִ� �÷��̾��� ��ȭ������Ʈ�� �����´�.
        recrod = transform.GetComponentInChildren<PlayerRecord>();

        //recrod�� null�� �ƴҶ���
        if (recrod != null)
        {
            //���� ����� �ִ� ���÷��� ��ü�� �ִٸ� �����Ű�鼭 
            //�߿�! ���÷��̰� �ɶ� ��ȭ�� ����Ǿ���.

            Debug.Log("RM" + recrod + "�� ��ȭ����");
            //��ȭ �÷��̸� ��� ��Ŵ!
            recrod.OnRecordStart();
        }

        else
        {
            print("��ȭ�Ұ��� �÷��̾�");
        }
    }

    public void OnRecordEnd()
    {
        if (recrod != null)
        {
            Debug.Log("RM" + recrod + "�� ��ȭ����");
            recrod.OnRecordEnd();

            //��ȭ ������ ������ main �÷��̾�� �ٲ�� ��!
            pm.targetPlayer = mainPlayer;
            //pm.CharChange();
        }

        else
        {
            print("��ȭ�����Ұ� ����");
        }
    }
}
