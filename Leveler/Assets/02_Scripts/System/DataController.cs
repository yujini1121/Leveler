using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float trainingTime;
    public int totalDeaths;
    public int fallDeaths;
    public int monsterHits;
}

public class DataController : MonoBehaviour
{
    private string savePath => Application.dataPath + "/05_JsonFiles/PlayerData.json";

    public PlayerData playerData = new PlayerData();

    public void SavePlayerData()
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"[DataController] 저장 완료: {savePath}");
    }

    public void LoadPlayerData()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("[DataController] 저장된 파일이 없습니다. 새 데이터로 시작합니다.");
            playerData = new PlayerData();
            return;
        }

        string json = File.ReadAllText(savePath);
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("[DataController] 불러오기 완료");
    }
}
