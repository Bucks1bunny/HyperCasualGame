using UnityEngine;
using DG.Tweening;

public class Object : MonoBehaviour, IUpdateable
{
    [SerializeField]
    private string _tag;
    private Rigidbody rb;
    private Vector3 startPosition;
    private Vector3 destination = new Vector3(-6.87f, 1.413f, -1.62f);

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

        transform.DOMoveY(1.3f, 1f).OnComplete(() => transform.DOMove(destination, 2f).SetEase(Ease.OutSine));
        transform.DORotate(new Vector3(90, 0, 0), 3f);
        transform.DOScale(transform.localScale / 1.75f, 2.5f);

        UpdateManager.RegisterLogic(this);
    }
}
