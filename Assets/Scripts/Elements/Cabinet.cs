using DG.Tweening;
using UnityEngine;

public class Cabinet : MonoBehaviour, IInteractable
{
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    public bool rIsOpen = false;
    public bool lIsOpen = false;
    public float openAngle = 130f; // Açılma açısı
    public float closeAngle = 0f; // Kapanma açısı
    public float animationDuration = 1.2f; // Animasyon süresi
    public float closeAnimationDuration = .2f; // Kapanma animasyon süresi
    public Transform rDoorTransform; // Dönecek obje (genelde parent olur)
    public Transform lDoorTransform; // Dönecek obje (genelde parent olur)
    private bool rIsAnimating = false;
    private bool lIsAnimating = false;
    public int lockID = -1; // -1 = kilitsiz, 0 veya üzeri = anahtar gerekli

    private void Start()
    {
        if (rDoorTransform == null)
            rDoorTransform = transform.GetChild(1); // Parent objeyi al
        if (lDoorTransform == null)
            lDoorTransform = transform.GetChild(0);
    }

    public void Interact()
    {
        if (rIsAnimating || lIsAnimating) return;

        if (!rIsOpen && !lIsOpen)
        {
            Open();
            return;
        }
        else
        {
            Close();
            audioSource.PlayOneShot(closeSound);
        }
    }

    private void Open()
    {
        rIsAnimating = true;
        lIsAnimating = true;

        Vector3 rOpenRotation = rDoorTransform.localEulerAngles;
        rOpenRotation.y = -openAngle;

        Vector3 lOpenRotation = lDoorTransform.localEulerAngles;
        lOpenRotation.y = openAngle;

        rDoorTransform.DOLocalRotate(rOpenRotation, animationDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                rIsOpen = true;
                rIsAnimating = false;
                Debug.Log("Kapı açıldı");
            });

        lDoorTransform.DOLocalRotate(lOpenRotation, animationDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                lIsOpen = true;
                lIsAnimating = false;
                Debug.Log("Kapı açıldı");
            });
    }

    private void Close()
    {
        rIsAnimating = true;
        lIsAnimating = true;

        Vector3 rCloseRotation = rDoorTransform.localEulerAngles;
        rCloseRotation.y = -closeAngle;

        Vector3 lCloseRotation = lDoorTransform.localEulerAngles;
        lCloseRotation.y = closeAngle;

        rDoorTransform.DOLocalRotate(rCloseRotation, closeAnimationDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                rIsOpen = false;
                rIsAnimating = false;
                Debug.Log("Kapı kapandı");
            });

        lDoorTransform.DOLocalRotate(lCloseRotation, closeAnimationDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                lIsOpen = false;
                lIsAnimating = false;
                Debug.Log("Kapı kapandı");
            });
    }
}