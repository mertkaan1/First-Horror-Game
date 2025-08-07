using UnityEngine;

public class BakisKontrol : MonoBehaviour
{
    public float bakisMesafesi = 3f;
    public Camera oyuncuKamerasi;
    public LayerMask InteractLayer;
    void Update()
    {
        Ray ray = new Ray(oyuncuKamerasi.transform.position, oyuncuKamerasi.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, bakisMesafesi, InteractLayer))
        {
            if (hit.collider.CompareTag("Key"))
            {
                KeyInfo anahtar = hit.collider.GetComponent<KeyInfo>();
                if (anahtar != null)
                {
                    UIManager.Instance.ShowInfo($"Anahtar: {anahtar.anahtarAdi} (ID: {anahtar.anahtarID})");
                    UIManager.Instance.HidePressE();
                    return;
                }
            }
            else if (hit.collider.CompareTag("Openable Door"))
            {
                UIManager.Instance.ShowPressE(hit.collider.GetComponent<OpenDoor>());
                UIManager.Instance.HideInfo();
                return;
            }
        }

        // Hiçbir şeyde değilse:
        UIManager.Instance.HideInfo();
        UIManager.Instance.HidePressE();
    }
}