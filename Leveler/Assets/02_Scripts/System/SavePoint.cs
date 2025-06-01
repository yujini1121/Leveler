using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public static Vector3 lastSavePosition; // 마지막 저장 위치 (static으로 선언하여 모든 인스턴스에서 공유)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lastSavePosition = transform.position;
            Debug.Log("Save Point Activated! Position saved: " + lastSavePosition);

            // SavePoint 오브젝트를 비활성화
            gameObject.SetActive(false);
            // 필요하다면 시각적인 피드백 (예: 파티클 효과)을 추가할 수 있습니다.
        }
    }
}





