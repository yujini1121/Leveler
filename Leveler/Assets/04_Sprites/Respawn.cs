using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public Transform playerTransform; // 리스폰시킬 플레이어의 Transform

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        if (playerTransform != null && SavePoint.lastSavePointPosition != Vector3.zero)
        {
            playerTransform.position = SavePoint.lastSavePointPosition;
            Debug.Log("플레이어 리스폰: " + SavePoint.lastSavePointPosition);
            // 추가적으로 리스폰 시의 효과를 줄 수도 있습니다.
        }
        else
        {
            Debug.LogWarning("플레이어 Transform이 설정되지 않았거나 저장된 Save Point가 없습니다.");
            // 기본 리스폰 위치를 설정하거나 다른 처리를 할 수 있습니다.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
