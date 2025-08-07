using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject messagePanel; // Panel objesi
    public TextMeshProUGUI messageText; // Mesaj yazısı
    public AudioSource audioSource;
    public AudioClip elmaSesi;
    public AudioClip kapiSesi;
    public AudioClip kaybettinSesi;
    public static UIManager Instance;
    public TextMeshProUGUI elmaSayaciText;
    public GameObject kapıAcildiText;
    public TextMeshProUGUI pressE;
    public GameObject kazandinPanel;
    public GameObject restartYazisi;
    public GameDirector gameDirector;
    public TextMeshProUGUI sureText;
    public TextMeshProUGUI bilgiText;
    void Awake()
    {
        Instance = this;
    }

    public void UpdateKeyCount(int mevcut, int toplam)
    {
        elmaSayaciText.text = "Anahtarlar: " + mevcut + "/" + toplam;
    }

    public void SureyiGuncelle(float kalanSure)
    {
        int kalanSaniye = Mathf.CeilToInt(kalanSure);
        sureText.text = "Süre: " + kalanSaniye.ToString();
    }
    public void ShowDoorOpened()
    {
        kapıAcildiText.SetActive(true);
        HideDoorOpenedWithDelay(2f); // 2 saniye sonra kapı açıldı mesajını gizle
    }
    public void HideDoorOpenedWithDelay(float delay)
    {
        StartCoroutine(HideDoorOpenedCoroutine(delay));
    }
    public void ShowPressE(OpenDoor door)
    {
        if (door.isOpen) // Kapı açıldıysa Press E mesajını göster
        {
            pressE.text = "Press E to Close Door";
        }
        else // Kapı kapalıysa Press E mesajını göster
        {
            pressE.text = "Press E to Open Door";
        }
        pressE.gameObject.SetActive(true);
    }
    public void HidePressE()
    {
        pressE.gameObject.SetActive(false);
    }

    private IEnumerator HideDoorOpenedCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        kapıAcildiText.SetActive(false);
    }
    public void ShowLevelCompletedUI(string mesaj = "Kazandın!")
    {
        kazandinPanel.SetActive(true);
        kazandinPanel.GetComponentInChildren<TextMeshProUGUI>().text = mesaj;
        Time.timeScale = 0f; // Oyunu dondur
    }
    public void HideLevelCompletedUI()
    {
        kazandinPanel.SetActive(false);
    }
    public void ShowMessage(string message, float duration = 2f)
    {
        if (messageText != null && messagePanel != null)
        {
            messageText.text = message;
            messagePanel.SetActive(true);
            CancelInvoke(nameof(HideMessage));
            Invoke(nameof(HideMessage), duration);
        }
    }

    public void HideMessage()
    {
        if (messagePanel != null)
            messagePanel.SetActive(false);
    }
    public void ShowInfo(string mesaj)
    {
        bilgiText.text = mesaj;
        bilgiText.gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        bilgiText.gameObject.SetActive(false);
    }
    public void RestartGame()
    {
        gameDirector.levelManager.RestartLevel();
    }
    public void CollectSound()
    {
        audioSource.PlayOneShot(elmaSesi);
    }

    public void DoorSound()
    {
        audioSource.PlayOneShot(kapiSesi);
    }

    public void LoseSound()
    {
        audioSource.PlayOneShot(kaybettinSesi);
    }
}