using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject enemyPrefab;
    public GameObject applePrefab;
    public GameObject doorPrefab;
    public GameObject playerPrefab;

    public float wallChance = 0.15f;
    public float enemyChance = 0.05f;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(x * 2, 0, z * 2);

                // Zemin yerleştir
                Instantiate(floorPrefab, pos, Quaternion.identity);

                // Oyuncunun başlangıç alanını boş bırak
                if (x == 0 && z == 0)
                {
                    Instantiate(playerPrefab, pos + Vector3.up * 1, Quaternion.identity);
                    continue;
                }

                float rand = Random.value;

                if (rand < wallChance)
                {
                    Instantiate(wallPrefab, pos + Vector3.up * 1, Quaternion.identity);
                }
                else if (rand < wallChance + enemyChance)
                {
                    Instantiate(enemyPrefab, pos + Vector3.up * 1, Quaternion.identity);
                }
            }
        }

        // Elma ve kapı en uzak köşelere koy
        Instantiate(applePrefab, new Vector3((width - 2) * 2, 1, (height - 2) * 2), Quaternion.identity);
        Instantiate(doorPrefab, new Vector3((width - 1) * 2, 1, (height - 1) * 2), Quaternion.identity);
    }
}