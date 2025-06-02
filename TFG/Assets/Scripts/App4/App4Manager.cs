using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App4Manager : MonoBehaviour
{
    public Canvas inicio1;
    public AudioSource inicio1audio;
    public Canvas inicio2;
    public AudioSource inicio2audio;

    public Canvas countDown;
    public TextMeshProUGUI timerText;

    private Coroutine countdownCoroutine;

    public GameObject pelota;

    public GameObject controlHead;

    public GameObject rightEyeQuad;
    public GameObject leftEyeQuad;

    private bool right;
    private bool left;

    public Canvas finished;
    public AudioSource finishedaudio;

    public GameObject RightEyeTracking;
    public GameObject LeftEyeTracking;

    private CSVManager csvManager;

    // Start is called before the first frame update
    void Start()
    {
        inicio1audio.Play();
        RightEyeTracking.SetActive(false);
        LeftEyeTracking.SetActive(false);
        finished.gameObject.SetActive(false);
        right = false;
        left = false;
        rightEyeQuad.SetActive(false);
        leftEyeQuad.SetActive(false);
        controlHead.SetActive(false);
        pelota.SetActive(false);
        inicio1.gameObject.SetActive(true);
        inicio2.gameObject.SetActive(false);
        countDown.gameObject.SetActive(false);
        csvManager = FindObjectOfType<CSVManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        int timeRemaining = 5;
        while (timeRemaining > 0)
        {
            timerText.text = timeRemaining.ToString();
            yield return null;
            timeRemaining--;
            yield return new WaitForSeconds(1f);
        }

        timerText.text = "0";
        countDown.gameObject.SetActive(false);
        pelota.SetActive(true);
        controlHead.SetActive(true);
        if (left == false)
        {
            StartCountdownfirst30secs();
            leftEyeQuad.SetActive(true);
            RightEyeTracking.SetActive(true);
        }
        if(right == true)
        {
            StartCountdownlast30secs();
            rightEyeQuad.SetActive(true);
            LeftEyeTracking.SetActive(true);
        }
    }

    public void StartCountdownfirst30secs()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(CountdownCoroutinefirst30secs());
    }
    private IEnumerator CountdownCoroutinefirst30secs()
    {
        int timeRemaining = 30;
        while (timeRemaining > 0)
        {
            yield return null;
            timeRemaining--;
            yield return new WaitForSeconds(1f);
        }
        countDown.gameObject.SetActive(true);
        RightEyeTracking.SetActive(false);
        StartCountdown();
        pelota.SetActive(false);
        controlHead.SetActive(true);
        leftEyeQuad.SetActive(false);
        right = true;
        left = true;
    }

    public void StartCountdownlast30secs()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(CountdownCoroutinelast30secs());
    }
    private IEnumerator CountdownCoroutinelast30secs()
    {
        int timeRemaining = 30;
        while (timeRemaining > 0)
        {
            yield return null;
            timeRemaining--;
            yield return new WaitForSeconds(1f);
        }
        finished.gameObject.SetActive(true);
        finishedaudio.Play();
        LeftEyeTracking.SetActive(false);
        pelota.SetActive(false);
        controlHead.SetActive(false);
        rightEyeQuad.SetActive(false);
        csvManager.GuardarDatos();
        right = false;
        left = true;
    }


    public void Retry()
    {
        SceneManager.LoadScene("ForthApp");
    }
    public void showinicio2()
    {
        inicio1audio.Stop();
        inicio2audio.Play();
        inicio1.gameObject.SetActive(false);
        inicio2.gameObject.SetActive(true);
    }
    public void empezarprueba()
    {
        inicio2audio.Stop();
        inicio2.gameObject.SetActive(false);
        countDown.gameObject.SetActive(true);
        StartCountdown();
    }
}
