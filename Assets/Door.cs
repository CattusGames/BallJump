using UnityEngine;

public class Door : MonoBehaviour
{
    private Animation anim;
    private bool isPlayed = false;
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void OpenDoor()
    {
        if (!isPlayed)
        {
            anim.Play();
            isPlayed = true;
        }
        
    }
}
