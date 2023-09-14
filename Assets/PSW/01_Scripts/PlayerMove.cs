using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMove : MonoBehaviour
{
    // Player 1 �� ��ġ
    public Transform trEye;
    // OVR Rig 
    public Transform trOvrRig;
    // CenterEye
    public Transform trCenterEye;
    // Model -> Rig Builder
    public RigBuilder rigBuilder;

    //�̵��ؾ� �ϴ� Player
    public Transform targetPlayer;

    public Transform player;

    void Update()
    {
        // ī�޶� ��ġ ����
        Vector3 offset = trEye.position - trCenterEye.position;
        trOvrRig.position += offset;

        // Vr ī�޶��� ��ġ�� ȸ���� �÷��̾ ����
        //���� �ڽ����� �ִ� �÷��̾�� ��� �ٲ��� ��.
        CharacterModel myPlayer = transform.GetComponentInChildren<CharacterModel>();

        Quaternion newRotation = Quaternion.Euler(0, trCenterEye.rotation.eulerAngles.y, 0);
        myPlayer.transform.rotation = newRotation;

        // �÷��̾� ��ü �ڵ�
        if (Input.GetKeyDown(KeyCode.G))
        {
            //rigBuilder �� ��Ȱ��ȭ
            rigBuilder.enabled = false;
            //rigBuilder �� �̿��ؼ� �θ�κ��� ������
            rigBuilder.transform.SetParent(null);
            //���� ��ġ�� targetPlayer �� ��ġ�� ����
            transform.position = targetPlayer.position;
            //���� ������ targetPlayer �� ������ ����
            transform.rotation = targetPlayer.rotation;
            //targetPlayer ���� CharacterModel �� ��������.
            CharacterModel cm = targetPlayer.GetComponent<CharacterModel>();
            //trEye �� ������ ������Ʈ�� trEye �� ����
            trEye = cm.trEye;

            //targetPlayer ���� RigBuilder �� ��������. (���� ������ �޾ƶ�)
            RigBuilder rb = targetPlayer.GetComponent<RigBuilder>();
            //������ ������Ʈ �� Ȱ��ȭ
            rb.enabled = true;
            //targetPlayer �� �θ� ���� ����
            targetPlayer.SetParent(transform);
            //targetPlayer �� rigBuilder �� transform �� ����.
            targetPlayer = rigBuilder.transform;
            //rigBuilder �� ���� ���������� �޾Ƴ��� rigBuilder�� ����
            rigBuilder = rb;
        }
    }
}
