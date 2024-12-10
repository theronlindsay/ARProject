using UnityEngine;
using UnityEngine.Video;



public class PlayVideo : MonoBehaviour
{

    private VideoPlayer videoPlayer; //create a private Video Player Var that instanciates video player

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

}