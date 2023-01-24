using UnityEngine;
using UnityEngine.Events;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shotPosition;
    [SerializeField] private float _minPlayerScale;
    [SerializeField] private GameObject _ground;
    [HideInInspector] public GameManager gameManager;
    private float holdingTime;
    [HideInInspector]public float infectionRadius;
    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        
    }
    private void Start()
    {
        _bullet.GetComponent<Rigidbody>().isKinematic = true;
    }
    void Update()
    {
        if (gameObject.transform.localScale.x < _minPlayerScale)
        {
           gameManager.onGameOver.Invoke();
        }
    }
    private void OnMouseDown()
    {
        _bullet.GetComponent<Rigidbody>().isKinematic = true;
        _bullet.transform.SetPositionAndRotation(_shotPosition.position, Quaternion.identity);
        _bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            holdingTime += Time.deltaTime;
            _bullet.transform.localScale += new Vector3(holdingTime * 0.001f, holdingTime * 0.001f, holdingTime * 0.001f);
            _ground.transform.localScale -= new Vector3(holdingTime * 0.001f, 0, 0);
            gameObject.transform.localScale -= new Vector3(holdingTime * 0.001f, holdingTime * 0.001f, holdingTime * 0.001f);
        }
       
    }
    private void OnMouseUp()
    {
       gameManager.onBulletShot.Invoke();
    }

}
