using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App2Manager : MonoBehaviour
{
    public Canvas canvasSlider;
    public GameObject panelLetras;
    public Canvas canvasInicio;
    public AudioSource firstInstruction;
    public Canvas Keyboard;
    public GameObject k_CSV;
    // Start is called before the first frame update
    void Start()
    {
        canvasSlider.enabled = false;
        panelLetras.gameObject.SetActive(false);
        firstInstruction.Play();
        Keyboard.enabled = false;
        k_CSV.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ready()
    {
        canvasSlider.enabled = true;
        panelLetras.gameObject.SetActive(true);
        firstInstruction.Stop();
        canvasInicio.enabled = false;
        Keyboard.enabled = true;
        k_CSV.gameObject.SetActive(true);
    }

    public void finished()
    {
        SceneManager.LoadScene("ThirdApp");
    }
}
