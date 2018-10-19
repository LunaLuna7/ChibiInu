using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour {

    public float speed;
    private MeshRenderer mr;
    private Material mat;
    private Vector2 offset;
    private Renderer r;
	// Use this for initialization
	void Start () {
        r = GetComponent<Renderer>();
        r.sortingLayerName = "Foreground";
        r.sortingOrder = -5;
        mr = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        mat = mr.material;
        offset = mat.mainTextureOffset;
        offset.y += Time.deltaTime/10 *  speed;
        mat.mainTextureOffset = offset;
		
	}
}
