using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class NextScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject skipButton;
    public GameObject UI;

    private void Start()
    {
        StartCoroutine(StartNextScene());
        UI.SetActive(false);
        skipButton.SetActive(true);
    }

    private IEnumerator StartNextScene()
    {
        yield return new WaitForSeconds(45);
        skip();
    }

    public void skip()
    {
        videoPlayer.gameObject.SetActive(false);
        skipButton.SetActive(false);
        UI.SetActive(true);
    }
}
