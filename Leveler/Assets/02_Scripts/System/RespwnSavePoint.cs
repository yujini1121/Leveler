using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespwnSavePoint : MonoBehaviour
{
    public GameObject mainSavePoint; // MainSavePoint 오브젝트를 할당할 변수
    public int respawnTriggerCount = 8; // MainSavePoint 활성화에 필요한 Respawn 횟수

    private int currentRespawnCount = 0; // 현재 Respawn 횟수

    void Start()
    {
        // 게임 시작 시 MainSavePoint를 비활성화합니다.
        if (mainSavePoint != null)
        {
            mainSavePoint.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Player 태그를 가진 오브젝트가 닿았을 때만 처리
        if (other.CompareTag("Player"))
        {
            currentRespawnCount++;
            Debug.Log("Respawn Count: " + currentRespawnCount); // 디버깅을 위해 콘솔에 출력

            // Respawn 횟수가 지정된 횟수에 도달했는지 확인
            if (currentRespawnCount >= respawnTriggerCount)
            {
                // MainSavePoint가 null이 아니고 비활성화 상태일 경우 활성화
                if (mainSavePoint != null && !mainSavePoint.activeSelf)
                {
                    mainSavePoint.SetActive(true);
                    Debug.Log("MainSavePoint Activated!");
                }
                // 횟수를 초기화하여 다음 8회에도 활성화될 수 있도록 합니다.
                currentRespawnCount = 0;
            }
        }
    }
}

