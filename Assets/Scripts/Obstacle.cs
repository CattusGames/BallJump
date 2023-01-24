using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private LayerMask _obstacleLayerMask;
    [SerializeField] private ParticleSystem _explosionParticle;
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
        _explosionParticle.Play();
        StartCoroutine(ParticleHalfAnimTime());
    }
    IEnumerator ParticleHalfAnimTime()
    {
        yield return new WaitForSeconds(0.5f);
        mesh.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    }
