using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMainMap : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint; // StartPoint 오브젝트의 Transform을 저장할 변수

    [SerializeField]
    private string goalTag = "Goal"; // Goal 오브젝트의 태그 (Inspector에서 설정 가능)

    // 다른 Collider2D와 충돌했을 때 호출되는 함수 (Is Trigger가 체크된 경우)
    private void OnTriggerEnter2D(Collider2D other) // 2D용으로 변경
    {
        // 충돌한 오브젝트의 태그가 GoalTag와 일치하는지 확인
        if (other.CompareTag(goalTag))
        {
            // StartPoint가 할당되어 있는지 확인
            if (startPoint != null)
            {
                // 현재 오브젝트(플레이어)의 위치를 StartPoint의 위치로 이동
                transform.position = startPoint.position;
                Debug.Log("Goal에 닿았습니다! StartPoint로 이동합니다.");
            }
            else
            {
                Debug.LogWarning("StartPoint가 할당되지 않았습니다. Inspector에서 StartPoint 오브젝트를 할당해주세요.");
            }
        }
    }

    // 디버그 용으로 StartPoint가 제대로 할당되었는지 확인
    private void OnValidate()
    {
        if (startPoint == null)
        {
            Debug.LogWarning("StartPoint가 할당되지 않았습니다. Inspector에서 StartPoint 오브젝트를 할당해주세요.");
        }
    }
}

