using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    public static Vector3 lastSavePointPosition { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lastSavePointPosition = transform.position;
            Debug.Log("Save Point 활성화: " + lastSavePointPosition);
            // 추가적으로 SavePoint가 활성화되었음을 시각적으로 표시할 수도 있습니다.
        }
    }

    // 게임 시작 시 초기 SavePoint를 설정하고 싶다면 이 함수를 호출할 수 있습니다.
    public static void SetInitialSavePoint(Vector3 position)
    {
        lastSavePointPosition = position;
        Debug.Log("초기 Save Point 설정: " + lastSavePointPosition);
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
