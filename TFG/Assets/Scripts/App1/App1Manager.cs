using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App1Manager : MonoBehaviour
{
    public Canvas canvas;
    public Canvas canvasFinal;
    public TextMeshProUGUI instruction1;
    public TextMeshProUGUI instruction2;
    public TextMeshProUGUI instruction3;
    public GameObject buttom1;
    public GameObject buttom2;
    public GameObject buttom3;
    public GameObject pelota;
    public GameObject Balltracking;
    public GameObject RegisterEyes;
    public int countdownTime = 10;
    public AudioSource instructionaudio1;
    public AudioSource instructionaudio2;
    public AudioSource instructionaudio3;
    public AudioSource instructionaudio4;

    public Transform cameraTransform;
    public float canvasInFront = 1.5f;

    public GameObject controlCamerashake;
    // Start is called before the first frame update
    void Start()
    {
        canvasFinal.gameObject.SetActive(false);
        instructionaudio1.Play();
        canvas.gameObject.SetActive(true);
        instruction2.gameObject.SetActive(false);
        instruction3.gameObject.SetActive(false);
        buttom2.gameObject.SetActive(false);
        buttom3.gameObject.SetActive(false);
        pelota.gameObject.SetActive(false);
        Balltracking.gameObject.SetActive(false);
        controlCamerashake.gameObject.SetActive(false );
        RegisterEyes.gameObject.SetActive(false);
        PositionCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        if(instruction3.isActiveAndEnabled == true)
        {
            instructionaudio3.Play();
        }
    }

    private void PositionCanvas()
    {
        Vector3 newPosition = cameraTransform.position + cameraTransform.forward * canvasInFront;
        transform.position = newPosition;
        transform.rotation = Quaternion.LookRotation(cameraTransform.forward, Vector3.up);
    }

    IEnumerator StartCountdown()
    {
        int timeRemaining = countdownTime;

        while (timeRemaining > 0)
        {
            instruction2.text = $"The trial will begin in {timeRemaining} seconds.\nLook to the front, and try to stand still during the process, if you move too much you will restart the process";

            yield return new WaitForSeconds(1);

            timeRemaining--;
        }
        instruction2.text = "The trial is starting...";
        yield return new WaitForSeconds(1);
        canvas.gameObject.SetActive(false);
        instruction2.gameObject.SetActive(false);
        pelota.gameObject.SetActive(true);
        Balltracking.gameObject.SetActive(true);
        RegisterEyes.gameObject.SetActive(true);
        controlCamerashake.gameObject.SetActive(true);
        StartCoroutine(HideAfterLifetime());
    }

    IEnumerator HideAfterLifetime()
    {
        yield return new WaitForSeconds(60);
        pelota.gameObject.SetActive(false);
        controlCamerashake.gameObject.SetActive(false);
        canvasFinal.gameObject.SetActive(true);
        instructionaudio4.Play();
        RegisterEyes.gameObject.SetActive(false);
        //Balltracking.gameObject.SetActive(false);
    } 

    public void ready()
    {
        Destroy(instruction1);
        Destroy(buttom1);
        instruction2.gameObject.SetActive(true);
        instructionaudio1.Stop();
        instructionaudio2.Play();
        StartCoroutine(StartCountdown());
    }

    public void retry()
    {
        SceneManager.LoadScene("FirstApp");
    }
    public void exit()
    {
        SceneManager.LoadScene("Menu");
    }
    public void nextTest()
    {
        SceneManager.LoadScene("SecondApp");
    }
}


