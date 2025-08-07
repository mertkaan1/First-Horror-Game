using UnityEngine;

public class keyPickup : MonoBehaviour, IInteractable
{
    public int keyId; // Bu anahtar hangi kapıya ait?

    public void Interact()
    {
        PlayerInventory.AddKey(keyId);
        UIManager.Instance.ShowMessage($"Anahtar {keyId} envantere eklendi.");
        KeyManager.Instance.CollectKey(keyId); // KeyManager'a da ekle
        Debug.Log($"Anahtar alındı! ID: {keyId}");
        Destroy(gameObject); // Anahtarı sahneden kaldır
    }
}