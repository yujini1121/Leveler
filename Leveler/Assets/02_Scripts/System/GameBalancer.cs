using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameBalancer : MonoBehaviour
{
    [Header("난이도 조절 점수 (0 ~ 1의 값)")]
    [Range(0f, 1f)] 
    [SerializeField] private float difficultyScore = 0.5f;

    [Header("시간 관련 변수")]
    [SerializeField] private float traningTime = 0f;
    [SerializeField] private TextMeshProUGUI timeText;

	void Start()
    {
        
    }

    void Update()
    {
        
    }
}



// 1. 시간
// 2. 총 사망 횟수 (이때, 추락사 횟수가 적을 수록 더 실력자로 판단함)
// 3. 몬스터 피격 횟수