using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
