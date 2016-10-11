using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainGameController : MonoBehaviour {

    #region Button Member
    public Button leftButton;
    public Button rightButton;
    #endregion

    public Text infoLabel;
    public Image fireImage;

    public Slider fireLifeBar;

    public float maxFireLifeTime = 1.5f;
    public float boostFireLifeTime = 0.3f;
    public float normaTime = 8.0f;
    private float fireLifeTime = 0f;

    private float startTime, finishTime;
    private bool timeOverFlg = false;
    private bool fireLostFlg = false;

    private float score = 0.0f;

    enum State {
        Init,
        Standby,
        Ready,
        Main,
        Finish,
        Result,
        Exit
    }

    private State _gameState;
    private State gameState {
        get {
            return _gameState;
        }

        set {
            _gameState = value;
            switch (value) {
                case State.Ready:
                    OnReady();
                    break;
                case State.Main:
                    OnStart();
                    break;
                case State.Finish:
                    OnFinish();
                    break;
                case State.Exit:
                    OnExit();
                    break;
                default:
                    break;
            }
        }
    }

	// Use this for initialization
	void Start () {
        gameState = State.Init;
        infoLabel.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        KeyUpdateOnNonMobile();
        switch (gameState) {
            case State.Init:
                fireLifeBar.maxValue = maxFireLifeTime;
                fireLifeBar.value = 0f;

                gameState = State.Ready;

                if (!Application.isMobilePlatform) {
                    leftButton.gameObject.SetActive(false);
                    rightButton.gameObject.SetActive(false);
                }
                break;

            case State.Ready:
                break;
            case State.Main:
                fireLifeTime -= Time.deltaTime;
                fireLifeBar.value = fireLifeTime;
                var imageRate = fireLifeBar.value / fireLifeBar.maxValue;
                fireImage.rectTransform.localScale = new Vector3(imageRate, imageRate, 1f) * 0.8f;
                if (fireLifeTime < 0f) {
                    fireLifeBar.value = 0f;
                    fireLostFlg = true;
                    gameState = State.Finish;
                }
                if (Time.time > startTime + 10.0f) {
                    timeOverFlg = true;
                    gameState = State.Finish;
                }
                break;
            case State.Finish:
                break;
            case State.Exit:
                //Scene change
                PlayerPrefs.SetFloat("playscore", score);
                SceneManager.LoadScene("ResultScene");
                break;
            default:
                break;
        }
	}

    void OnReady() {
        leftButton.GetComponentInChildren<Text>().text = "Start!";
        rightButton.enabled = false;

        if (!Application.isMobilePlatform) {
            infoLabel.text = "Spaceキーで始める";
        } else {
            infoLabel.text = "Startボタンで始める";
        }
    }

    void OnStart() {
        leftButton.GetComponentInChildren<Text>().text = "Fire!!!";
        rightButton.enabled = true;

        fireLifeTime = maxFireLifeTime;
        startTime = Time.time;
        Debug.Log("Start!!");
        if (!Application.isMobilePlatform) {
            infoLabel.text = "Spaceキー連打で火を起こせ！\n" + string.Format("{0:##.##}秒経ったらEnterキーを押せ！",normaTime);
        } else {
            infoLabel.text = "Fireボタンで火を起こせ！" + string.Format("{0:##.##}秒経ったらPickボタンを押せ！", normaTime); ;
        }
    }

    void OnFinish() {
        leftButton.enabled = false;
        rightButton.GetComponentInChildren<Text>().text = "Finish.";

        //終了アニメーション
        if (timeOverFlg) {
            //時間切れの旨のアニメーション
            infoLabel.text = "時間切れ...";
            if (!Application.isMobilePlatform) {
                infoLabel.text = infoLabel.text + "\nEnterキーで結果発表へ";
            } else {
                infoLabel.text = infoLabel.text + "\nFinishボタンで結果発表へ";
            }
        } else if (fireLostFlg) {
            //時間切れの旨のアニメーション
            infoLabel.text = "火が消えてしまった...";
            if (!Application.isMobilePlatform) {
                infoLabel.text = infoLabel.text + "\nEnterキーで結果発表へ";
            } else {
                infoLabel.text = infoLabel.text + "\nFinishボタンで結果発表へ";
            }
        } else {
            if (!Application.isMobilePlatform) {
                infoLabel.text = "Enterキーで結果発表へ";
            } else {
                infoLabel.text = "Finishボタンで結果発表へ";
            }
            //calc score
            var diff = (Time.time - startTime);
            score = (100.0f - Mathf.Abs(normaTime - diff) * 10.0f);
            Debug.Log("Time:" + diff);
            Debug.Log("Score:" + score);
        }
    }

    void OnExit() {
        leftButton.enabled = false;
        rightButton.enabled = false;
    }

    public void boostFire() {
        fireLifeTime += boostFireLifeTime;
        if (fireLifeTime > maxFireLifeTime) {
            fireLifeTime = maxFireLifeTime;
        }
    }

    public void pickPotato() {
        //イモ拾う
        Debug.Log("Pick Potato.");

        finishTime = Time.time;
        //終了状態へ
        gameState = State.Finish;
    }

    void KeyUpdateOnNonMobile() {
        if (!Application.isMobilePlatform) {
            //FireButton
            if (Input.GetButtonDown("Fire")) {
                OnTapLeftButton();
            }
            //PickButton
            if (Input.GetButtonDown("Pick")) {
                OnTapRightButton();
            }
        }
    }

    #region ButtonEvents

    public void OnTapLeftButton() {
        switch (gameState) {
            case State.Ready:
                gameState = State.Main;
                break;
            case State.Main:
                boostFire();
                break;
            default:
                break;
        }
    }

    public void OnTapRightButton() {
        switch (gameState) {
            case State.Main:
                pickPotato();
                break;
            case State.Finish:
                //Segue to Result
                gameState = State.Exit;
                break;
            default:
                break;
        }
    }

    #endregion
}
