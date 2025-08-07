using DG.Tweening;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    void Start()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        transform.DOMoveY(1f, 1f).SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}
