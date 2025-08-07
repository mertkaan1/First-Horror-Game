using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    public HashSet<int> collectedKeys = new HashSet<int>();

    private void Awake()
    {
        // Singleton oluşturuluyor
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne değişiminde silinmez
        }
        else
        {
            Destroy(gameObject); // Zaten varsa yenisini yok et
        }
    }

    public void CollectKey(int keyID)
    {
        if (!collectedKeys.Contains(keyID))
        {
            collectedKeys.Add(keyID);
            Debug.Log($"Anahtar alındı: {keyID}");
            UIManager.Instance.UpdateKeyCount(collectedKeys.Count, 4);

        }
        else
        {
            Debug.Log($"Anahtar zaten alındı: {keyID}");
        }
    }

    public bool HasKey(int keyID)
    {
        return collectedKeys.Contains(keyID);
    }
}