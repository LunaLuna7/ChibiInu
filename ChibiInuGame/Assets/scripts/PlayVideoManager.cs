using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideoManager : MonoBehaviour {

    public RawImage rawImage;
    public VideoPlayer videoPlayer;

	// Use this for initialization
	void OnEnable () {
        StartCoroutine(PlayVideo());	
	}
	
    public void StopVideo()
    {
        videoPlayer.Stop();
    }
	public IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds temp = new WaitForSeconds(.73f);
        while (!videoPlayer.isPrepared)
        {
            rawImage.gameObject.SetActive(false);
            yield return temp;
            break;
        }
        rawImage.gameObject.SetActive(true);
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }
}
