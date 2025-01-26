using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class EffectClickManager : MonoBehaviour
{
    [SerializeField] private GameObject effectHit, effecTextDamage;
    [SerializeField] private Vector3 offSetForEffects;
    [SerializeField] private float distance = 200;
    [SerializeField] private float durationMoveText = 1.6f;
    private PoolObject effectClickPool, effectTextAfterClickPool;
    private const int countEffect = 5;
    private DamagePerClick _damagePerClick;
    
    public void Initialize(DamagePerClick damagePerClick)
    {
        _damagePerClick = damagePerClick;
        effectClickPool = new PoolObject(effectHit, countEffect, null);
        effectTextAfterClickPool = new PoolObject(effecTextDamage, countEffect, null);
    }
    private void OnEnable()
    {
        _damagePerClick.OnClickHit += Spawn;
    }
    private void OnDisable()
    {
        _damagePerClick.OnClickHit -= Spawn;
    }
    private void Spawn(Vector3 point)
    {
        StartCoroutine(SpawnMove(point));
    }
    IEnumerator SpawnMove(Vector3 point)
    {
        GameObject effectHit = effectClickPool.GetObjectFromPool();
        effectHit.transform.position = point + offSetForEffects;
        StartCoroutine(ReturnEffect(effectHit));

        GameObject effecTextDamage = effectTextAfterClickPool.GetObjectFromPool();
        effecTextDamage.GetComponent<RectTransform>().position = point + offSetForEffects;
        effecTextDamage.GetComponent<RectTransform>().localScale = Vector3.one;
        yield return new WaitForSeconds(.1f);
        effecTextDamage.GetComponent<RectTransform>().DOAnchorPosY(point.y + distance, durationMoveText - 0.1f).SetEase(Ease.InExpo);
        effecTextDamage.GetComponent<RectTransform>().DOScale(Vector3.zero, 1).SetEase(Ease.InFlash);
        effecTextDamage.GetComponent<TextMeshPro>().text = _damagePerClick.GetDamage().ToString("F0") + "+";
        StartCoroutine(ReturnText(effecTextDamage));
    }
    IEnumerator ReturnEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1);
        effectClickPool.ReturnObjectToPool(effect);
    }
    IEnumerator ReturnText(GameObject text)
    {
        yield return new WaitForSeconds(durationMoveText);
        text.GetComponent<RectTransform>().position = Vector3.zero;
        effectTextAfterClickPool.ReturnObjectToPool(text);
    }
}
