using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoSkipButton : MonoBehaviour
{
    public GameObject videoPlayerObject;
    public string nextSceneName = "game2"; 

    
    public void SkipVideo()
    {
        if (videoPlayerObject != null)
        {
            videoPlayerObject.SetActive(false); 
        }

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); 
            //PlayerPrefs.DeleteAll();
        }
        else
        {
            Debug.LogWarning("Geçilecek sahne adı belirtilmedi!");
        }
    }
}
