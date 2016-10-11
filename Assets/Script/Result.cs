using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Result : MonoBehaviour {
	private float[] ranks = new float[3];

	//score:今回のスコアを呼び出す 
	//rankSave:ソート中の値保存用　
	//newRankScore:scoreのコピー兼ソート中の比較用
	public float score, rankSave, newRankScore;

	//今回のスコアを表示する用
	//unity上ヒエラルキーメニューであらかじめTextを入れておく
	public Text scoreText;

	//ランキングのスコアを表示する用
	//unity上ヒエラルキーメニューであらかじめTextを入れておく
	public Text[] rankTexts;

	public AudioClip[] ac;
	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
		
		ranks[0] = PlayerPrefs.GetFloat("1th", 0.0f);
		ranks[1] = PlayerPrefs.GetFloat("2nd", 0.0f);
		ranks[2] = PlayerPrefs.GetFloat("3nd", 0.0f);
		score = PlayerPrefs.GetFloat("playscore", 0.0f);

		scoreText.text = "今回のScore:" + score.ToString();

		newRankScore = score;


		for (int i = 0; i < 3; i++)
		{
			if (newRankScore > ranks[i])
			{
				rankSave = ranks[i];
				ranks[i] = newRankScore;

				newRankScore = rankSave;
			}
			if (i == 0)
			{
				PlayerPrefs.SetFloat("1th", ranks[i]);
			}
			if (i == 1)
			{
				PlayerPrefs.SetFloat("2nd", ranks[i]);
			}
			if (i == 2)
			{
				PlayerPrefs.SetFloat("3nd", ranks[i]);
			}
			rankTexts[i].text = "" + (i + 1) + "位: " + ranks[i].ToString();
		}
		if(score>79)
		{
			source.PlayOneShot(ac[0]);
		}
		else
		{
			source.PlayOneShot(ac[1]);
		}

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
