using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour {

    public float speed;
    private MeshRenderer mr;
    private Material mat;
    private Vector2 offset;
	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        mat = mr.material;
        offset = mat.mainTextureOffset;
        offset.x += Time.deltaTime/10 *  speed;
        mat.mainTextureOffset = offset;
		
	}
}
