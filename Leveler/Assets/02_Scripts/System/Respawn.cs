using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Transform playerTransform; // 플레이어의 Transform 컴포넌트를 연결할 변수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerTransform != null && SavePoint.lastSavePosition != Vector3.zero)
            {
                playerTransform.position = SavePoint.lastSavePosition;
                Debug.Log("Respawned at: " + SavePoint.lastSavePosition);
                // 필요하다면 시각적인 피드백 (예: 파티클 효과)을 추가할 수 있습니다.
            }
            else
            {
                Debug.LogWarning("No save point activated yet or Player Transform is not assigned.");
            }
        }
    }
}