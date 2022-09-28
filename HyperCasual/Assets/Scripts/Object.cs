using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour, IUpdateable
{
    [SerializeField]
    private string _tag;
    private Rigidbody rb;
    private Vector3 destination = new Vector3(-6.87f, 1.413f, -1.592f);
    public void Tick()
    {
        transform.Translate(destination * 1 * Time.deltaTime);
        if (transform.position == destination)
        {
            rb.useGravity = true;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("Click");

        ObjectPool.Instance.SpawnFromPool(_tag, transform.position, Quaternion.identity);

        transform.localScale = transform.localScale / 1.75f;
        transform.rotation = Quaternion.Euler(90, 0, 0);
        transform.position = destination;
        rb.useGravity = true;
    }
}
