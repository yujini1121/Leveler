using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameBalancer : MonoBehaviour
{
    [Header("���̵� ���� ���� (0 ~ 1�� ��)")]
    [Range(0f, 1f)] 
    [SerializeField] private float difficultyScore = 0.5f;

    [Header("�ð� ���� ����")]
    [SerializeField] private float traningTime = 0f;
    [SerializeField] private TextMeshProUGUI timeText;

	void Start()
    {
        
    }

    void Update()
    {
        
    }
}



// 1. �ð�
// 2. �� ��� Ƚ�� (�̶�, �߶��� Ƚ���� ���� ���� �� �Ƿ��ڷ� �Ǵ���)
// 3. ���� �ǰ� Ƚ��