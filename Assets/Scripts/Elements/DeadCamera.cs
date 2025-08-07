using DG.Tweening;
using UnityEngine;

public class DeadCamera : MonoBehaviour
{
    public Transform playerTransform;


    public void Dead()
    {
        transform.position = new Vector3(playerTransform.position.x, 2, playerTransform.position.z); // Reset camera position to a default value

        transform.DOMoveY(5f, 2f).SetLoops(1, LoopType.Incremental).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            // Optionally, you can add logic here to handle what happens after the camera moves up
            UIManager.Instance.ShowLevelCompletedUI("Öldün!");
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Debug.Log("Camera moved up after death.");
        });
    }

}
