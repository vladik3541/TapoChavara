using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageTextAnimation : MonoBehaviour
{
    [SerializeField] private float distance;
    private RectTransform rectTransform;
    private TextMeshPro textMeshPro;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshPro>();
    }

    public void Move(Vector3 startPos)
    {
        rectTransform.position = startPos;
        rectTransform.DOScale(Vector3.zero, 0.5f);
        //rectTransform.DOAnchorPosY(startPos.y + distance, 1.5f).SetEase(Ease.InExpo);
    }
}
