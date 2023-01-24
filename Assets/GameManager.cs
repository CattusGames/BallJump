using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onFinish;
    public UnityEvent onDoorRange;
    public UnityEvent onBulletShot;
    public UnityEvent onGameOver;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            onFinish.Invoke();
        }
    }

}
