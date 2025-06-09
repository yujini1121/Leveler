using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player 태그를 사용
        {
            // 플레이어의 리스폰 횟수를 증가시키고 타일맵을 업데이트하도록 요청
            PlayerRespawn player = other.GetComponent<PlayerRespawn>();
            if (player != null)
            {
                player.IncrementRespawnCount();
                // 타일맵을 관리하는 스크립트에 리스폰 횟수를 전달
                TileManager tileManager = FindObjectOfType<TileManager>();
                if (tileManager != null)
                {
                    tileManager.SetTilesBasedOnRespawnCount(player.GetCurrentRespawnCount());
                }
            }
        }
    }
}