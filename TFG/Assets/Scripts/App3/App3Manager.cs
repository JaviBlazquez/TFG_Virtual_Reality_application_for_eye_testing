using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class App3Manager : MonoBehaviour
{
    public AudioSource audioinicio;
    public Canvas menu;
    public Canvas countdown;
    public Canvas canvasInicio;
    public TextMeshProUGUI timerText;
    private Coroutine countdownCoroutine;
    private int numeroveces;
    public GameObject rightcanvas;
    public GameObject leftcanvas;
    public Camera rightCamera;
    public Camera leftCamera;

    public GameObject object1; 
    public GameObject object2;
    private float collisionTime;


    public Canvas canvasFin;
    public AudioSource audioFin;
    // Start is called before the first frame update
    void Start()
    {
        rightcanvas.SetActive(false);
        leftcanvas.SetActive(false);
        canvasFin.gameObject.SetActive(false);
        numeroveces = 0;
        countdown.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        canvasInicio.gameObject.SetActive(true);
        audioinicio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(numeroveces >= 2)
        {
            menu.gameObject.SetActive(false);
        }*/
    }

    public void begin()
    {
        menu.gameObject.SetActive(true);
        canvasInicio.gameObject.SetActive(false);
        audioinicio.Stop();
    }

    private IEnumerator MeasureCollisionTime()
    {
        float startTime = Time.time;
        yield return new WaitUntil(() => AreObjectsColliding());
        collisionTime = Time.time - startTime;
        Debug.Log($"Colisión ocurrió después de {collisionTime} segundos.");
        Destroy(object1);
        Destroy(object2);
        PlayerPrefs.SetFloat("TiempoColision", collisionTime);
        canvasFin.gameObject.SetActive(true);
        audioFin.Play();
    }

    private bool AreObjectsColliding()
    {
        return object1.GetComponent<Collider>().bounds.Intersects(object2.GetComponent<Collider>().bounds);
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
        countdown.gameObject.SetActive(false);
        StartCoroutine(MeasureCollisionTime());
    }

    public void taparderecho()
    {
        if(leftcanvas.activeSelf == false) { 
            rightcanvas.SetActive(true);
        }    
    }

    public void taparizquierdo()
    {
        if (rightcanvas.activeSelf == false)
        {
            leftcanvas.SetActive(true);
        }
    }
    public void botheyes()
    {
        leftcanvas.SetActive(false);
        rightcanvas.SetActive(false);
    }

    public void empezar()
    {
        leftcanvas.SetActive(false);
        rightcanvas.SetActive(false);
        menu.gameObject.SetActive(false);
        countdown.gameObject.SetActive(true);
        StartCountdown();
        numeroveces++;
    }

    public void continuar()
    {
        SceneManager.LoadScene("ForthApp");
    }
}
