using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapCoordinateCalculator : MonoBehaviour
{
    public Tilemap targetTilemap; // 인스펙터에서 타일맵 컴포넌트를 할당해주세요.
    public Camera mainCamera; // 메인 카메라를 할당하거나, Camera.main을 사용하세요.

    void Update()
    {
        // 마우스 클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            // 1. 마우스 스크린 좌표를 월드 좌표로 변환
            Vector3 mouseScreenPos = Input.mousePosition;
            // Z 값을 카메라의 Z 값과 같게 설정하거나, 0으로 설정하여 2D 평면을 기준으로 합니다.
            // Orthographic Camera의 경우 Z 값은 크게 중요하지 않을 수 있습니다.
            mouseScreenPos.z = mainCamera.nearClipPlane; // 또는 transform.position.z 등으로 조정
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

            // 2. 월드 좌표를 타일맵의 셀 좌표로 변환
            Vector3Int cellPos = targetTilemap.WorldToCell(worldPos);

            Debug.Log($"클릭된 월드 좌표: {worldPos}");
            Debug.Log($"해당 타일맵 셀 좌표: {cellPos}");

            // 해당 셀의 중앙 월드 좌표도 얻을 수 있습니다.
            Vector3 cellCenterWorldPos = targetTilemap.GetCellCenterWorld(cellPos);
            Debug.Log($"해당 셀의 중앙 월드 좌표: {cellCenterWorldPos}");
        }
    }
}