using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // "Player" 태그를 가진 오브젝트와 충돌했는지 확인
        {
            // TileManager 스크립트의 인스턴스를 찾습니다.
            // TileManager가 씬에 단 하나만 존재한다면 FindObjectOfType으로 찾을 수 있습니다.
            TileManager tileManager = FindObjectOfType<TileManager>();

            if (tileManager != null)
            {
                tileManager.PauseTilePlacement(); // TileManager의 타일 배치 일시 정지 함수 호출
                Debug.Log("Goal reached! TileManager paused.");
            }
        }
    }
}
