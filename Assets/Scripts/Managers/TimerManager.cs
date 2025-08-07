using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float baslangicSuresi = 60f; // kaç saniye
    public float kalanSure;
    public bool oyunBitti = false;

    public TimerManager Instance;

    void Start()
    {
        kalanSure = baslangicSuresi;
        UIManager.Instance.SureyiGuncelle(kalanSure);
        Instance = this;
    }

    void Update()
    {
        if (oyunBitti) return;
        kalanSure -= Time.deltaTime;
        UIManager.Instance.SureyiGuncelle(kalanSure);

        if (kalanSure <= 0f)
        {
            OyunuKaybettin();
        }
    }

    void OyunuKaybettin()
    {
        UIManager.Instance.LoseSound();
        UIManager.Instance.ShowLevelCompletedUI("Süre Doldu!");
        oyunBitti = true;
    }
}