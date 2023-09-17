using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMove : MonoBehaviour
{
    // * Player 1 �� ��ġ
    public Transform trEye;
    // OVR Rig 
    public Transform trOvrRig;
    // CenterEye
    public Transform trCenterEye;

    // * Model -> Rig Builder
    public RigBuilder rigBuilder;

    // * �̵��ؾ� �ϴ� Player
    public Transform targetPlayer;

    public List<GameObject> playerList;
    int player_index = 0;

    LHandTarget lt;
    RHandTarget rt;

    void Update()
    {
        //�ؼҿ� ī�޶� ��ġ ����
        /*Vector3 offset = trEye.position - trCenterEye.position;
        trOvrRig.position += offset;*/

        //�������߰��κ�(������ �ƴ� y�ุ ���� �� �̵��� �� �ְ�)
        Vector3 offset = trEye.position - trCenterEye.position;
        Vector3 newPosition2 = trOvrRig.position + new Vector3(0, offset.y, 0);
        trOvrRig.position = newPosition2;

        // Vr ī�޶��� ��ġ�� ȸ���� �÷��̾ ����
        //���� �ڽ����� �ִ� �÷��̾�� ��� �ٲ��� ��.
        CharacterModel myPlayer = transform.GetComponentInChildren<CharacterModel>();

        Quaternion newRotation = Quaternion.Euler(0, trCenterEye.rotation.eulerAngles.y, 0);
        myPlayer.transform.rotation = newRotation;

        //�������߰��κ�(��ġ�߰�)
        // ī�޶�� �� �� �տ� �ְ� �ʹ�. ������ ī�޶��� �̵��� ���� �̵��ϰ� �ʹ�.
        // �÷��̾�� ī�޶��� x z���� �������� ī�޶�� trEye�� y���� ���󰣴�.
        Vector3 newPosition = new Vector3(trCenterEye.transform.position.x, myPlayer.transform.position.y, trCenterEye.transform.position.z);
        myPlayer.transform.position = newPosition;

        //�������߰��κ�(�ȸ���)
        lt = transform.GetComponentInChildren<LHandTarget>();
        rt = transform.GetComponentInChildren<RHandTarget>();

        //player�� �������� ������ �� �ְ�
        lt.isTargeting = true;
        rt.isTargeting = true;

        if(Input.GetKeyDown(KeyCode.C))
        {
            player_index = (player_index + 1) % playerList.Count;

            if(playerList != null)
            {
                for(int i = 0; i < playerList.Count; i++)
                {
                    //���� ����
                    if(i == player_index)
                    {
                        targetPlayer = playerList[i].transform;
                    }

                    //���� �ʴ�
                    else
                    {
                        
                    }
                }
            }
        }

        // �÷��̾� ��ü �ڵ�
        if (Input.GetKeyDown(KeyCode.G))
        {
            CharChange();
        }
    }

    public void CharChange()
    {
        //rigBuilder �� ��Ȱ��ȭ -> �Ȳ��� ��.
        //rigBuilder.enabled = false;

        //player �������� ������ �� ����
        lt.isTargeting = false;
        rt.isTargeting = false;

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
