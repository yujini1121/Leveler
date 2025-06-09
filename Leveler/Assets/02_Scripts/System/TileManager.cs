using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap targetTilemap; // 타일을 나타낼 타일맵 (인스펙터에서 할당)
    public TileBase tileToAppear;  // 나타낼 타일 에셋 (인스펙터에서 할당)

    // 모든 나타날 수 있는 타일의 위치를 담는 리스트 (인스펙터에서 설정)
    // 이 리스트의 순서대로 타일이 3개씩 나타납니다.
    public List<Vector3Int> allPossibleTiles = new List<Vector3Int>();

    private bool canPlaceTiles = true; // 타일 배치를 일시 정지/재개하기 위한 플래그

    void Start()
    {
        // 초기에는 모든 나타날 수 있는 타일들을 숨김 (null로 설정)
        // 게임 시작 시 모든 타일이 숨겨진 상태로 시작하도록 합니다.
        ClearAllPossibleTiles();
    }

    // 모든 가능한 타일 위치에서 타일을 제거하는 헬퍼 함수
    private void ClearAllPossibleTiles()
    {
        foreach (Vector3Int pos in allPossibleTiles)
        {
            targetTilemap.SetTile(pos, null);
        }
    }

    // RespawnPoint 스크립트에서 호출될 함수
    public void SetTilesBasedOnRespawnCount(int currentRespawnCount)
    {
        if (!canPlaceTiles) // canPlaceTiles가 false면, 타일 배치 로직을 실행하지 않음
        {
            Debug.Log("Tile placement is currently paused by Goal trigger.");
            return;
        }

        // 항상 모든 타일을 먼저 지우고, 현재 횟수에 맞춰 다시 그립니다.
        // 이렇게 하면 이전 횟수에서 나타났던 타일들이 중복으로 남지 않습니다.
        ClearAllPossibleTiles();

        // 리스폰 횟수에 따라 나타낼 타일의 개수를 계산합니다.
        // 예를 들어, 1번 이하는 0개, 2번은 3개, 3번은 6개, 4번은 9개...
        // 0회 리스폰 시 0개, 1회 리스폰 시 3개, 2회 리스폰 시 6개 ...
        // (currentRespawnCount) * 3 개가 나타나도록 계산
        int tilesToSpawnCount = currentRespawnCount * 3;

        // allPossibleTiles 리스트의 크기를 넘지 않도록 제한합니다.
        tilesToSpawnCount = Mathf.Min(tilesToSpawnCount, allPossibleTiles.Count);

        // 계산된 개수만큼 타일을 배치합니다.
        for (int i = 0; i < tilesToSpawnCount; i++)
        {
            if (i < allPossibleTiles.Count) // 리스트 범위를 벗어나지 않도록 방어 코드
            {
                targetTilemap.SetTile(allPossibleTiles[i], tileToAppear);
            }
        }

        Debug.Log($"Respawn Count: {currentRespawnCount}, Tiles Spawned: {tilesToSpawnCount}");
    }

    // GoalTrigger 스크립트에서 호출될 함수
    public void PauseTilePlacement()
    {
        canPlaceTiles = false;
        Debug.Log("TileManager: Tile placement paused by Goal.");
    }


}
