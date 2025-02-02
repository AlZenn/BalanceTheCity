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
        videoPlayer = GameObject.FindWithTag("VideoPlayerTag").GetComponent<VideoPlayer>();
        // Video bitince çağrılacak metodu belirle
        videoPlayer.loopPointReached += OnVideoEnd;
        //videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"cutscene.mp4"); 
        videoPlayer.source = VideoSource.Url;
        
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
    }
}
