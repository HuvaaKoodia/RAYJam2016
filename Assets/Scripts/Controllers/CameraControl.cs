using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public static CameraControl I;

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
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) shakeDuration = 5f; // SPACE TO TEST SHAKE EFFECT *REMOVE AT LAUNCH FOR PROFIT*
	
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


}
