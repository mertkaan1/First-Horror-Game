using System.Collections.Generic;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public GameObject door;
    public FirstPersonController player;
    public GameObject collectablePrefab;
    public List<GameObject> collectables;
    public float spawnRadius; // Rastgele üretilecek alanın yarıçapı
    public GameObject surface;
    public EnemyManager enemyManager;
    public UIManager uiManager;
    public TimerManager timerManager;
    public KeyManager keyManager;

    void Start()
    {
        keyManager = KeyManager.Instance;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RestartLevel()
    {
        elmaAreaMask = 1 << NavMesh.GetAreaFromName("Collectable");
        Debug.Log("Restarting Level...");
        enemyManager.RestartEnemyManager();
        player.RestartPlayer();
        ClearCollectables();
        CreateCollectables();
        timerManager.oyunBitti = false; // Oyunu yeniden başlatırken süreyi sıfırla
        timerManager.kalanSure = timerManager.baslangicSuresi; // Başlangıç süresini ayarla
        uiManager.HideLevelCompletedUI();
        uiManager.UpdateKeyCount(keyManager.collectedKeys.Count, 4);
        uiManager.SureyiGuncelle(30f); // Örnek olarak 30 saniye süre verildi
        Time.timeScale = 1f; // Oyunu başlat
    }

    private void ClearCollectables()
    {
        foreach (var collectable in collectables)
        {
            Destroy(collectable);
        }
        collectables.Clear();
    }

    private void CreateCollectables()
    {
        collectables.Clear();

        Vector3 randomPosition = GetRandomNavMeshPoint(surface.transform.position, spawnRadius);


        if (randomPosition != Vector3.zero)
        {
            var newCollectable = Instantiate(collectablePrefab, randomPosition, Quaternion.identity);
            collectables.Add(newCollectable);
        }
    }

    // Update is called once per frame
    public void AppleCollected()
    {
    }
    int elmaAreaMask;

    Vector3 GetRandomNavMeshPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += center;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 7f, elmaAreaMask))
            {
                return hit.position;
            }
        }


        Debug.LogWarning("NavMesh üzerinde uygun nokta bulunamadı.");
        return Vector3.zero;
    }
}
