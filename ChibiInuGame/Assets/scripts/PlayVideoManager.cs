using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideoManager : MonoBehaviour {

    public RawImage rawImage;
    public VideoPlayer videoPlayer;

	// Use this for initialization
	void Start () {
        StartCoroutine(PlayVideo());	
	}
	
	IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(1f);
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }
}
