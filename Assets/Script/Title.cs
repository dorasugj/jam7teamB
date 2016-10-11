using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Title : MonoBehaviour
{
	private Image img;
	private Image img2;
	public Sprite[] imo;
	
	// Use this for initialization
	void Start()
	{
		img = GameObject.Find("Canvas/GameStrat").GetComponent<Image>();
		img2 = GameObject.Find("Canvas/Ranking").GetComponent<Image>();
	}

	// Update is called once per frame
	void Update()
	{

	}
	public void buttonPush()
	{
		SceneManager.LoadScene("Main");
	}

	public void rankingButton()
	{
		SceneManager.LoadScene("ResultScene");
	}

	public void pushTutorialButton()
	{
        SceneManager.LoadScene("TutorialScean");
	}
	public void buttonOnMouce()
	{

		img.sprite = imo[0];
	}
	public void buttonOutMouce()
	{
		img.sprite = imo[1];
	}
	public void RessultOnMouce()
	{

		img2.sprite = imo[2];
	}
	public void RessultOutMouce()
	{
		img2.sprite = imo[3];
	}
}
