using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class CSVManager : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/UserData.csv";
        // Verificar si el archivo existe antes de escribir la cabecera
        if (!File.Exists(filePath))
        {
            string header = "Nombre;Tiempo de Seguimiento (s);Fecha y Hora; Distancia 1 ambos ojos (m);Distancia 2 ambos ojos (m)" +
                ";Distancia 3 ojo izq (m);Distancia 4 ojo izq (m);Distancia 5 ojo der (m);Distancia 6 ojo der (m);Tiempo colision objetos" +
                ";Tiempo seguimiento ojo derecho (s);Tiempo seguimiento ojo izquierdo (s) " + Environment.NewLine;
            File.WriteAllText(filePath, header);
        }
    }

    public void GuardarDatos()
    {
        String userName = PlayerPrefs.GetString("UserName", "Usuario Desconocido");
        string fechaHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        float Seguimiento = PlayerPrefs.GetFloat("GazeTime", 0);
        float dis1 = PlayerPrefs.GetFloat("Distancia1", 0);
        float dis2 = PlayerPrefs.GetFloat("Distancia2", 0);
        float dis3 = PlayerPrefs.GetFloat("Distancia3", 0);
        float dis4 = PlayerPrefs.GetFloat("Distancia4", 0);
        float dis5 = PlayerPrefs.GetFloat("Distancia5", 0);
        float dis6 = PlayerPrefs.GetFloat("Distancia6", 0);
        float tiempocolision = PlayerPrefs.GetFloat("TiempoColision", 0);
        float seguimientoIzq = PlayerPrefs.GetFloat("GazeTimeLeft", 0);
        float seguimientoDer = PlayerPrefs.GetFloat("GazeTimeder", 0);
        string nuevaLinea = $"{userName};{Seguimiento};{fechaHora};{dis1};" +
            $"{dis2};{dis3};{dis4};{dis5};{dis6};{tiempocolision};{seguimientoDer};{seguimientoIzq}" +
            $"{Environment.NewLine}";
        File.AppendAllText(filePath, nuevaLinea); // Agregar la línea al archivo
    }
}


