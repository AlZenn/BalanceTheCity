using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSkipButton : MonoBehaviour
{
    public GameObject videoPlayerObject;
    public string nextSceneName = "game2";
    public VideoPlayer videoPlayer;

    private void Start()
    {
        // Video bitince çağrılacak metodu belirle
        videoPlayer.loopPointReached += OnVideoEnd;

        // Videoyu hemen başlat
        videoPlayer.Play();
    }
    private void OnVideoEnd(VideoPlayer vp)
    {
        // Video bittiğinde diğer sahneye geç
        SceneManager.LoadScene(nextSceneName);
    }


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
