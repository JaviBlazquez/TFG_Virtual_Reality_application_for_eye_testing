using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Boton1;
    public TMP_InputField inputField;
    public GameObject teclado;
    public GameObject textoinput;
    // Start is called before the first frame update
    void Start()
    {
        inputField.gameObject.SetActive(true);
        teclado.gameObject.SetActive(true);
        textoinput.gameObject.SetActive(true);
        Boton1.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RunFirstApp()
    {
        SceneManager.LoadScene("FirstApp");
    }

    public void AddCharacter(string character)
    {
        inputField.text += character;
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
        string playerName = inputField.text;
        PlayerPrefs.SetString("UserName", playerName);
        PlayerPrefs.Save();
        Debug.Log("Nombre guardado: " + playerName);
        inputField.gameObject.SetActive(false);
        teclado.gameObject.SetActive(false);
        textoinput.gameObject.SetActive(false);
        Boton1.gameObject.SetActive(true);
    }
}