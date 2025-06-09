using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // "Player" �±׸� ���� ������Ʈ�� �浹�ߴ��� Ȯ��
        {
            // TileManager ��ũ��Ʈ�� �ν��Ͻ��� ã���ϴ�.
            // TileManager�� ���� �� �ϳ��� �����Ѵٸ� FindObjectOfType���� ã�� �� �ֽ��ϴ�.
            TileManager tileManager = FindObjectOfType<TileManager>();

            if (tileManager != null)
            {
                tileManager.PauseTilePlacement(); // TileManager�� Ÿ�� ��ġ �Ͻ� ���� �Լ� ȣ��
                Debug.Log("Goal reached! TileManager paused.");
            }
        }
    }
}
