using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public static CameraControl I;

    public Text scoreText;
    public Text centerText;
    public Text levelText;
    public Text timerText;
    public float score;
    public float countdownSpeed;

    float shakeIntensity;
    float shakeDuration;

    void Awake()
    {
        I = this;
    }

    // Use this for initialization
    void Start()
    { 
        shakeIntensity = 1f;
        score = 0;
        AddScore(0);
    }

    // Update is called once per frame
    void Update() {	
        if (shakeDuration > 0)
        {
            shakeDuration -= 3*Time.deltaTime;
            transform.position = Random.insideUnitCircle * shakeDuration/10 * shakeIntensity;
            transform.position += new Vector3(0, 0, -10);
        }
        timerText.text = GameController.I.RoundTime.ToString("f1");
	}
    public void StartShake(float _duration, float _intensity)
    {
        shakeDuration = _duration;
        shakeIntensity = _intensity;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " +score;
    }

    public void UpdateLevel()
    {
        levelText.text = "Level: " + EnemySpawner.level.ToString();
    }
    public void ShowText(string _text)
    {
        centerText.gameObject.SetActive(true);
        centerText.text = _text;
    }
   
    public IEnumerator Countdown()
    {
        centerText.gameObject.SetActive(true);
        for (int i = 1; i < 4; i++)
        {
            centerText.text = (4 - i).ToString();
            yield return new WaitForSeconds(countdownSpeed);
        }
        centerText.gameObject.SetActive(false);
        GameController.I.StartRound();
    }
}
