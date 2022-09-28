using UnityEngine;
using DG.Tweening;

public class Object : MonoBehaviour, IUpdateable
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

    public void Tick()
    {
        if (transform.position == destination)
        {
            rb.freezeRotation = false;
            rb.useGravity = true;
            ObjectPool.Instance.SpawnFromPool(_tag, startPosition, Quaternion.identity);
            UpdateManager.UnregisterLogic(this);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void OnMouseDown()
    {
        startPosition = transform.position;

        transform.DOMoveY(1.3f, 1f).OnComplete(() => transform.DOMove(destination, 1f).SetEase(Ease.OutSine));
        transform.DORotate(new Vector3(90, 0, 0), 2f);
        transform.DOScale(transform.localScale / 1.75f, 2f);

        UpdateManager.RegisterLogic(this);
    }
}
