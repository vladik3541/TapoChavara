using DG.Tweening;
using UnityEngine;

public class BoxDOTween : MonoBehaviour
{
    public ParticleSystem dust;
    public float jumpDistance = 4;
    public Transform point;
    private Vector2 direction;
    private bool allowJump = true;

    public RectTransform text;
    // Start is called before the first frame update
    void Start()
    {
        text.DOAnchorPosY(250, 1).SetEase(Ease.OutBack);
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        Vector3 newPosition =  new Vector3(direction.x, 0, direction.y) * jumpDistance;

        if(direction.magnitude != 0 && allowJump)
        {
            allowJump = false;
            print(newPosition + transform.position);
            transform.DOJump(newPosition+transform.position, 3, 1, 1).OnComplete(EndJump);
        }
        
    }
    private void EndJump()
    {
        allowJump = true;
        dust.Play();
    }
}
