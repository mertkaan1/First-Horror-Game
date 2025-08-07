using DG.Tweening;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public static ExitDoor Instance;

    public Transform doorTransform;
    public int totalLocks = 3;
    private int unlockedCount = 0;
    private bool doorOpened = false;
    private FirstPersonController firstPersonController;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        firstPersonController = FindObjectOfType<FirstPersonController>();
    }

    public void Unlock()
    {
        unlockedCount++;
        if (unlockedCount >= totalLocks && !doorOpened)
        {
            doorOpened = true;
            OpenDoor();
            firstPersonController.isMainDoorUnlocked = true; // Ana kapı kilidi açıldı
        }
    }

    private void OpenDoor()
    {
        UIManager.Instance.ShowMessage("Tüm kilitler açıldı!");
        doorTransform.DORotate(new Vector3(0, 140f, 0), 2f); // Basit kapı açılma hareketi
        UIManager.Instance.DoorSound();
    }
}