using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Keyboard_CSV : MonoBehaviour
{
    public TMP_InputField inputField;
    private string firstLetters;
    private float distancia1;
    private string secondLetters;
    private float distancia2;
    private string thirdLetters;
    private float distancia3;
    private string fourthLetters;
    private float distancia4;
    private string fifthLetters;
    private float distancia5;
    private string sixthLetters;
    private float distancia6;
    public TextMeshProUGUI letters1;
    public TextMeshProUGUI letters2;
    public TextMeshProUGUI letters3;
    public TextMeshProUGUI letters4;
    public TextMeshProUGUI letters5;
    public TextMeshProUGUI letters6;
    private int counter;
    public Canvas canvasSlider;
    public GameObject panelLetras;
    public GameObject canvasLeft;
    public GameObject canvasRight;
    public Canvas canvasFinished;
    public AudioSource audiofinished;
    // Start is called before the first frame update
    void Start()
    {
        canvasLeft.gameObject.SetActive(false);
        canvasRight.gameObject.SetActive(false);
        canvasFinished.gameObject.SetActive(false);
        letters1.enabled = true;
        letters2.enabled = false;
        letters3.enabled = false;
        letters4.enabled = false;
        letters5.enabled = false;
        letters6.enabled = false;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter ==6) { 
        Debug.Log(distancia1 + " " + distancia2 + " " + distancia3+ " " + distancia4 + " " + distancia5 + " " + distancia6); 
        }
    }

    public void RunFirstApp()
    {
        SceneManager.LoadScene("FirstApp");
    }

    public void AddCharacter(string character)
    {
        inputField.text += character;
        canvasSlider.enabled = false;
    }
    public void AddSpace()
    {
        inputField.text += " ";
    }
    public void DeleteCharacter()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void Submit()
    {
        if(counter == 0)
        {
            firstLetters = inputField.text;
            letters1.enabled = false;
            letters2.enabled = true;
            canvasSlider.enabled = true;
            distancia1 = panelLetras.gameObject.transform.position.z;
            PlayerPrefs.SetFloat("Distancia1", distancia1);
            counter++;
        }
         else if (counter == 1)
        {
            secondLetters = inputField.text;
            letters2.enabled = false;
            letters3.enabled = true;
            canvasSlider.enabled = true;
            distancia2 = panelLetras.gameObject.transform.position.z;
            PlayerPrefs.SetFloat("Distancia2", distancia2);
            counter++;
            SwitchToLeft();
        }
         else if (counter == 2)
        {
            thirdLetters = inputField.text;
            letters3.enabled = false;
            letters4.enabled = true;
            canvasSlider.enabled = true;
            distancia3 = panelLetras.gameObject.transform.position.z;
            PlayerPrefs.SetFloat("Distancia3", distancia3);
            counter++;
        }
         else if (counter == 3)
        {
            fourthLetters = inputField.text;
            letters4.enabled = false;
            letters5.enabled = true;
            canvasSlider.enabled = true;
            distancia4 = panelLetras.gameObject.transform.position.z;
            PlayerPrefs.SetFloat("Distancia4", distancia4);
            counter++;
            SwitchToRight();
        }
        else if (counter == 4)
        {
            fifthLetters = inputField.text;
            letters5.enabled = false;
            letters6.enabled = true;
            canvasSlider.enabled = true;
            distancia5 = panelLetras.gameObject.transform.position.z;
            PlayerPrefs.SetFloat("Distancia5", distancia5);
            counter++;
        }
        else if (counter == 5)
        {
            sixthLetters = inputField.text;
            letters6.enabled = false;
            canvasSlider.enabled = true;
            distancia6 = panelLetras.gameObject.transform.position.z;
            counter++;
            PlayerPrefs.SetFloat("Distancia6", distancia6);
            BackToNormal();
            canvasFinished.gameObject.SetActive(true);
            canvasSlider.gameObject.SetActive(false);
            panelLetras.gameObject.SetActive(false);
            audiofinished.Play();
        }
        inputField.text = "";
    }

    private void SwitchToLeft()
    {
        canvasLeft.gameObject.SetActive(false);
        canvasRight.gameObject.SetActive(true);
    }
    private void SwitchToRight()
    {
        canvasLeft.gameObject.SetActive(true);
        canvasRight.gameObject.SetActive(false);
    }
    private void BackToNormal()
    {
        canvasLeft.gameObject.SetActive(false);
        canvasRight.gameObject.SetActive(false);
    }

}