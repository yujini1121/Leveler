using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private int respawnCount = 0;

    public int GetCurrentRespawnCount()
    {
        return respawnCount;
    }

    public void IncrementRespawnCount()
    {
        respawnCount++;
        Debug.Log("Respawn Count: " + respawnCount);
        // �ʿ��ϴٸ� ���⼭ �÷��̾� ��ġ �ʱ�ȭ �� ������ ���� ���� �߰�
    }
}
