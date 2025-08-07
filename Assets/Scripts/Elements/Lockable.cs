using UnityEngine;

public class Lockable : MonoBehaviour
{
    public int lockID;
    private bool isUnlocked = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryUnlock();
        }
    }

    private void TryUnlock()
    {
        if (isUnlocked) return;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (KeyManager.Instance.HasKey(lockID))
                {
                    isUnlocked = true;
                    UIManager.Instance.ShowMessage("Kilit açıldı!");
                    Destroy(gameObject); // Kilidi yok et (görsel zinciri kaldır)
                    ExitDoor.Instance.Unlock();
                }
                else
                {
                    UIManager.Instance.ShowMessage("Uygun anahtarın yok!");
                }
            }
        }
    }
}