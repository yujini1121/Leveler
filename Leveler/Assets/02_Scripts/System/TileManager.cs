using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap targetTilemap; // Ÿ���� ��Ÿ�� Ÿ�ϸ� (�ν����Ϳ��� �Ҵ�)
    public TileBase tileToAppear;  // ��Ÿ�� Ÿ�� ���� (�ν����Ϳ��� �Ҵ�)

    // ��� ��Ÿ�� �� �ִ� Ÿ���� ��ġ�� ��� ����Ʈ (�ν����Ϳ��� ����)
    // �� ����Ʈ�� ������� Ÿ���� 3���� ��Ÿ���ϴ�.
    public List<Vector3Int> allPossibleTiles = new List<Vector3Int>();

    private bool canPlaceTiles = true; // Ÿ�� ��ġ�� �Ͻ� ����/�簳�ϱ� ���� �÷���

    void Start()
    {
        // �ʱ⿡�� ��� ��Ÿ�� �� �ִ� Ÿ�ϵ��� ���� (null�� ����)
        // ���� ���� �� ��� Ÿ���� ������ ���·� �����ϵ��� �մϴ�.
        ClearAllPossibleTiles();
    }

    // ��� ������ Ÿ�� ��ġ���� Ÿ���� �����ϴ� ���� �Լ�
    private void ClearAllPossibleTiles()
    {
        foreach (Vector3Int pos in allPossibleTiles)
        {
            targetTilemap.SetTile(pos, null);
        }
    }

    // RespawnPoint ��ũ��Ʈ���� ȣ��� �Լ�
    public void SetTilesBasedOnRespawnCount(int currentRespawnCount)
    {
        if (!canPlaceTiles) // canPlaceTiles�� false��, Ÿ�� ��ġ ������ �������� ����
        {
            Debug.Log("Tile placement is currently paused by Goal trigger.");
            return;
        }

        // �׻� ��� Ÿ���� ���� �����, ���� Ƚ���� ���� �ٽ� �׸��ϴ�.
        // �̷��� �ϸ� ���� Ƚ������ ��Ÿ���� Ÿ�ϵ��� �ߺ����� ���� �ʽ��ϴ�.
        ClearAllPossibleTiles();

        // ������ Ƚ���� ���� ��Ÿ�� Ÿ���� ������ ����մϴ�.
        // ���� ���, 1�� ���ϴ� 0��, 2���� 3��, 3���� 6��, 4���� 9��...
        // 0ȸ ������ �� 0��, 1ȸ ������ �� 3��, 2ȸ ������ �� 6�� ...
        // (currentRespawnCount) * 3 ���� ��Ÿ������ ���
        int tilesToSpawnCount = currentRespawnCount * 3;

        // allPossibleTiles ����Ʈ�� ũ�⸦ ���� �ʵ��� �����մϴ�.
        tilesToSpawnCount = Mathf.Min(tilesToSpawnCount, allPossibleTiles.Count);

        // ���� ������ŭ Ÿ���� ��ġ�մϴ�.
        for (int i = 0; i < tilesToSpawnCount; i++)
        {
            if (i < allPossibleTiles.Count) // ����Ʈ ������ ����� �ʵ��� ��� �ڵ�
            {
                targetTilemap.SetTile(allPossibleTiles[i], tileToAppear);
            }
        }

        Debug.Log($"Respawn Count: {currentRespawnCount}, Tiles Spawned: {tilesToSpawnCount}");
    }

    // GoalTrigger ��ũ��Ʈ���� ȣ��� �Լ�
    public void PauseTilePlacement()
    {
        canPlaceTiles = false;
        Debug.Log("TileManager: Tile placement paused by Goal.");
    }


}
