using UnityEngine;
using DG.Tweening;

public class Object : MonoBehaviour
{
    [field: SerializeField]
    public Color objectColor
    {
        get;
        private set;
    }

    [SerializeField]
    private string _tag;
    private Rigidbody rb;
    private Vector3 startPosition;
    private Vector3 destination = new Vector3(-6.6f, 1.413f, -2.5f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void OnMouseDown()
    {
        startPosition = transform.position;
        if (!GameManager.isMixed)
        {
            transform.DOMoveY(1.3f, 1f).OnComplete(() => transform.DOMove(destination, 1f).OnComplete(() => OnDestinationReached()));
            transform.DORotate(new Vector3(90, 0, 0), 2f);
            transform.DOScale(transform.localScale / 1.75f, 2f);
        }
    }

    private void OnDestinationReached()
    {
        rb.freezeRotation = false;
        rb.useGravity = true;
        ObjectPool.Instance.SpawnFromPool(_tag, startPosition, Quaternion.identity);
    }
}
