using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameBalancer : MonoBehaviour
{
    [Header("���̵� ���� (0.0 ~ 1.0)")]
    [Range(0f, 1f)]
    [SerializeField] private float difficultyScore = 0.5f;
    [SerializeField] private TextMeshProUGUI difficultyText;

    [Header("����ġ ����")]
    [SerializeField] private float weightTime = 0.3f;
    [SerializeField] private float weightDeath = 0.3f;
    [SerializeField] private float weightHit = 0.3f;
    [SerializeField] private float weightFallPenalty = 0.1f;

    [Header("���̵� ��ȯ �ӵ�")]
    [SerializeField] private float lerpSpeed = 0.1f;

    [Header("������ ��Ʈ�ѷ�")]
    [SerializeField] private DataController dataController;

    private void Start()
    {
        if (dataController != null)
        {
            dataController.LoadPlayerData();
        }
        else
        {
            Debug.LogWarning("[GameBalancer] DataController�� ������� �ʾҽ��ϴ�.");
        }
    }

    private void Update()
    {
        if (dataController == null) return;

        // trainingTime ���� �� ����ȭ
        dataController.playerData.trainingTime += Time.deltaTime;

        float targetScore = CalculateTargetDifficulty();
        difficultyScore = Mathf.Lerp(difficultyScore, targetScore, Time.deltaTime * lerpSpeed);

        if (difficultyText != null)
            difficultyText.text = $"���̵�: {difficultyScore:F2}";
    }

    private float CalculateTargetDifficulty()
    {
        var d = dataController.playerData;

        float score = 0f;
        score += Normalize(d.trainingTime, 0f, 600f) * weightTime;
        score += Normalize(d.totalDeaths, 0, 30) * weightDeath;
        score += Normalize(d.monsterHits, 0, 50) * weightHit;
        score -= Normalize(d.fallDeaths, 0, Mathf.Max(d.totalDeaths, 1)) * weightFallPenalty;

        return Mathf.Clamp01(score);
    }

    private float Normalize(float value, float min, float max)
    {
        if (Mathf.Approximately(max - min, 0f)) return 0f;
        return Mathf.Clamp01((value - min) / (max - min));
    }

    public void RegisterDeath(bool isFall)
    {
        if (dataController == null) return;

        dataController.playerData.totalDeaths++;
        if (isFall) dataController.playerData.fallDeaths++;
    }

    public void RegisterMonsterHit()
    {
        if (dataController == null) return;

        dataController.playerData.monsterHits++;
    }

    public float GetDifficultyScore()
    {
        return difficultyScore;
    }

    private void OnApplicationQuit()
    {
        if (dataController != null)
        {
            dataController.SavePlayerData();
        }
    }
}
