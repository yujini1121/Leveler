using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player �±׸� ���
        {
            // �÷��̾��� ������ Ƚ���� ������Ű�� Ÿ�ϸ��� ������Ʈ�ϵ��� ��û
            PlayerRespawn player = other.GetComponent<PlayerRespawn>();
            if (player != null)
            {
                player.IncrementRespawnCount();
                // Ÿ�ϸ��� �����ϴ� ��ũ��Ʈ�� ������ Ƚ���� ����
                TileManager tileManager = FindObjectOfType<TileManager>();
                if (tileManager != null)
                {
                    tileManager.SetTilesBasedOnRespawnCount(player.GetCurrentRespawnCount());
                }
            }
        }
    }
}