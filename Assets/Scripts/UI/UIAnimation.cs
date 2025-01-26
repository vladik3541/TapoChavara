using UnityEngine;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private float durationShacke;
    [SerializeField] private float strenghtShacke;
    [SerializeField] private int vibratio;
    [SerializeField] private float randomness;
    [SerializeField] private Vector3 originalPosition;

    public void OpenPanelAbility(RectTransform trans) => trans.DOAnchorPos(new Vector2(-385, 0), 0.5f).SetEase(Ease.OutFlash);
    public void ClosePanelAbility(RectTransform trans) => trans.DOAnchorPos(new Vector2(400, 0), 0.5f).SetEase(Ease.InFlash);

    public void EnableSlider(RectTransform transform) => transform.DOScale(4, 0.2f);
    public void DisableSlider(RectTransform transform) => transform.DOScale(0, 0.2f);

    public void TextDoubleDamageAnimation(RectTransform transform) => transform.DOScale(1.2f, 0.01f).OnComplete(() => transform.DOScale(0.2f, 0.01f));
}
