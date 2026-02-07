using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
 public void OnPlayClick()
    {
        SceneManager.LoadScene("Game Play");
    }
}
