using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameBalancer : MonoBehaviour
{
	[Header("난이도 점수 (0.0 ~ 1.0)")]
	[Range(0f, 1f)]
	[SerializeField] private float difficultyScore = 0.5f;
	[SerializeField] private TextMeshProUGUI difficultyText;

	[Header("플레이어 통계")]
	[SerializeField] private float trainingTime = 0f;
	[SerializeField] private int totalDeaths;
	[SerializeField] private int fallDeaths;
	[SerializeField] private int monsterHits;

	[Header("가중치 설정")]
	[SerializeField] private float weightTime = 0.3f;
	[SerializeField] private float weightDeath = 0.3f;
	[SerializeField] private float weightHit = 0.3f;
	[SerializeField] private float weightFallPenalty = 0.1f;

	[Header("난이도 전환 속도")]
	[SerializeField] private float lerpSpeed = 0.1f;

	private void Update()
	{
		trainingTime += Time.deltaTime;

		float targetScore = CalculateTargetDifficulty();
		difficultyScore = Mathf.Lerp(difficultyScore, targetScore, Time.deltaTime * lerpSpeed);

		if (difficultyText != null)
			difficultyText.text = $"난이도: {difficultyScore:F2}";
	}

	private float CalculateTargetDifficulty()
	{
		float score = 0f;

		score += Normalize(trainingTime, 0f, 600f) * weightTime;
		score += Normalize(totalDeaths, 0, 30) * weightDeath;
		score += Normalize(monsterHits, 0, 50) * weightHit;
		score -= Normalize(fallDeaths, 0, Mathf.Max(totalDeaths, 1)) * weightFallPenalty;

		return Mathf.Clamp01(score);
	}

	private float Normalize(float value, float min, float max)
	{
		if (Mathf.Approximately(max - min, 0f)) return 0f;
		return Mathf.Clamp01((value - min) / (max - min));
	}

	// 외부에서 통계 업데이트용 메서드
	public void RegisterDeath(bool isFall)
	{
		totalDeaths++;
		if (isFall) fallDeaths++;
	}

	public void RegisterMonsterHit()
	{
		monsterHits++;
	}

	public float GetDifficultyScore()
	{
		return difficultyScore;
	}
}
