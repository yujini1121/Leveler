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
        // 필요하다면 여기서 플레이어 위치 초기화 등 리스폰 관련 로직 추가
    }
}
