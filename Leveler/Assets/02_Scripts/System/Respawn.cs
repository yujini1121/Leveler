using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Transform playerTransform; // �÷��̾��� Transform ������Ʈ�� ������ ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerTransform != null && SavePoint.lastSavePosition != Vector3.zero)
            {
                playerTransform.position = SavePoint.lastSavePosition;
                Debug.Log("Respawned at: " + SavePoint.lastSavePosition);
                // �ʿ��ϴٸ� �ð����� �ǵ�� (��: ��ƼŬ ȿ��)�� �߰��� �� �ֽ��ϴ�.
            }
            else
            {
                Debug.LogWarning("No save point activated yet or Player Transform is not assigned.");
            }
        }
    }
}