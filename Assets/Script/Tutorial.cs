using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void pushGameButton()
	{
		SceneManager.LoadScene("Main");
	}
	public void pushTitleButton()
	{
		SceneManager.LoadScene("TitleScean");
	}
}
