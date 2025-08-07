using DG.Tweening;
using UnityEngine;

public class OpenDoor : MonoBehaviour, IInteractable
{
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    public bool isOpen = false;
    public Transform doorTransform; // Dönecek obje (genelde parent olur)
    private bool isAnimating = false;

    public int lockID = -1; // -1 = kilitsiz, 0 veya üzeri = anahtar gerekli

    private void Start()
    {
        if (doorTransform == null)
            doorTransform = transform.parent; // Parent objeyi al
    }

    public void Interact()
    {
        if (isAnimating) return;

        // Eğer kapı kilitliyse ve oyuncuda anahtar yoksa açma
        if (lockID != -1 && !KeyManager.Instance.HasKey(lockID))
        {
            Debug.Log("Kapı kilitli. Anahtar gerekli.");
            UIManager.Instance.ShowMessage("Kapı kilitli. Anahtar lazım.");
            return;
        }

        if (!isOpen)
        {
            Open();
            audioSource.PlayOneShot(openSound);
        }
        else
        {
            Close();
            audioSource.PlayOneShot(closeSound);
        }
    }

    private void Open()
    {
        isAnimating = true;
        doorTransform.DORotate(doorTransform.eulerAngles + new Vector3(0, 80, 0), 1.2f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                isOpen = true;
                isAnimating = false;
                Debug.Log("Kapı açıldı");
            });
    }

    private void Close()
    {
        isAnimating = true;
        doorTransform.DORotate(doorTransform.eulerAngles - new Vector3(0, 80, 0), .2f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                isOpen = false;
                isAnimating = false;
                Debug.Log("Kapı kapandı");
            });
    }
}