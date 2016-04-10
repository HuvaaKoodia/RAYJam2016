using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public static CameraControl I;

    public Text scoreText;
    public Text gameOverText;
    public Text levelText;
    public float score;

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
}
