using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private LayerMask _obstacleLayerMask;
    private MeshRenderer mesh;
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _bullet.gameObject)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _bullet.infectionRadius, _obstacleLayerMask);
            foreach (Collider obstacle in hitColliders)
            {
                obstacle.GetComponent<Obstacle>().Explosion();
                _bullet.Reload();
            }
        }
    }
    private void Explosion()
    {
        mesh.material.color = Color.red;
        Destroy(gameObject);
        
    }
}
