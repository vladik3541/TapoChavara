using System;
using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 5f; // Швидкість руху кулі
    public float thresholdDistance = 0.1f; // Дистанція, на якій куля знищується
    public bool Rotate = false;
    public float speedRotate;
    public bool LookRotation = false;
    public GameObject effectHit;
    private const int durationEffect = 1;
    public void StartMovement(Vector3 targetPosition, Action onFinish)
    {
        StartCoroutine(MoveToTarget(targetPosition, onFinish));
    }

    private IEnumerator MoveToTarget(Vector3 targetPosition, Action onFinish)
    {
        while (Vector3.Distance(transform.position, targetPosition) > thresholdDistance)
        {
            if(Rotate)
            {
                transform.Rotate(speedRotate * Time.deltaTime, 0, 0);
            }
            else if(LookRotation)
            {
                Vector3 direction = targetPosition - transform.position;
                transform.rotation = Quaternion.LookRotation(direction.normalized);
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        onFinish?.Invoke();
    }
}
