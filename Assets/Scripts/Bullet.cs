using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private PlayerShot _playerShot;
    [HideInInspector] public float infectionRadius;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _playerShot.gameManager.onBulletShot.AddListener(Shot);
    }
    private void Shot()
    {
        infectionRadius = transform.localScale.x * 2;
        rb.isKinematic = false;
        rb.AddForce(_playerShot.transform.forward * 30f, ForceMode.Impulse);
    }
    public void Reload()
    {
        rb.isKinematic = true;
        transform.position = Vector3.zero - new Vector3(0,0,5f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, _playerShot.infectionRadius);
    }
}
