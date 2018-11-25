using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {
	private bool[] collectable;
	[SerializeField]private int levelIndex;
	[SerializeField]private int nextLevelIndex;
	// Use this for initialization
	void Start () {
		collectable = SaveManager.dataInUse.levels[levelIndex].collectable;
	}
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		//touch the end, save and transfer back to level selection
		if(other.CompareTag("Player"))
		{
			//unlock next level, update the stuff collected, save
			SaveManager.dataInUse.levels[nextLevelIndex].unlocked = true;
			SaveManager.dataInUse.levels[levelIndex].collectable = collectable;
			SaveManager.Save(SaveManager.filename);
			//transfer to LevelSeletion Scene
			UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
		}
	}

	public void Collect(int index)
	{
		collectable[index] = true;
	}
}
