using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class SceneButon : MonoBehaviour
{
    public void Click(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
