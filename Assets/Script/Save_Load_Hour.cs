using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Save_Load_Hour : MonoBehaviour {

	public GameObject Label,Edad;
	Change_States CS;
	// Use this for initialization
	void Start () {	
		CS = GameObject.Find ("Scripts").GetComponent (typeof(Change_States)) as Change_States;
		//System.DateTime departure = new System.DateTime(2010, 6, 1, 23, 50, 0);
		//System.DateTime arrival = new System.DateTime(2010, 6, 3, 00, 5, 0);
		System.DateTime Now = System.DateTime.Now;
		//System.TimeSpan travelTime = arrival - departure;  
		//Debug.Log("travelTime: " + travelTime );  
		
		int Inicio = PlayerPrefs.GetInt("Inicios");
		if (Inicio == 0)
		{
			Debug.Log("Inicio por primera vez");
			// Save the first Now
			PlayerPrefs.SetInt("Inicios", 1);	
			//	Save birth 
			PlayerPrefs.SetInt("Año_Nacimiento",Now.Year);
			PlayerPrefs.SetInt("Mes_Nacimiento",Now.Month);
			PlayerPrefs.SetInt("Dia_Nacimiento",Now.Day);
			PlayerPrefs.SetInt("Hora_Nacimiento",Now.Hour);
			PlayerPrefs.SetInt("Minuto_Nacimiento",Now.Minute);			
			// Initiate Temp Stats
			System.DateTime reinicio = System.DateTime.Now;
			PlayerPrefs.SetInt("A_rC",reinicio.Year);
			PlayerPrefs.SetInt("Me_rC",reinicio.Month);
			PlayerPrefs.SetInt("D_rC",reinicio.Day);
			PlayerPrefs.SetInt("H_rC",reinicio.Hour);
			PlayerPrefs.SetInt("Mi_rC",reinicio.Minute);

			PlayerPrefs.SetInt("A_rB",reinicio.Year);
			PlayerPrefs.SetInt("Me_rB",reinicio.Month);
			PlayerPrefs.SetInt("D_rB",reinicio.Day);
			PlayerPrefs.SetInt("H_rB",reinicio.Hour);
			PlayerPrefs.SetInt("Mi_rB",reinicio.Minute);

			PlayerPrefs.SetInt("A_rA",reinicio.Year);
			PlayerPrefs.SetInt("Me_rA",reinicio.Month);
			PlayerPrefs.SetInt("D_rA",reinicio.Day);
			PlayerPrefs.SetInt("H_rA",reinicio.Hour);
			PlayerPrefs.SetInt("Mi_rA",reinicio.Minute);
			//
			Label.GetComponent<Text>().text = "Primer inicio"; 
			Edad.GetComponent<Text>().text = "Edad: 0 horas";
		}else{
			Debug.Log("Ya inicio una vez");
			//Load Last Desconnection
			System.DateTime Last = new System.DateTime(  PlayerPrefs.GetInt("A_Desc"), 
														 PlayerPrefs.GetInt("Me_Desc"),
														 PlayerPrefs.GetInt("D_Desc"),
														 PlayerPrefs.GetInt("H_Desc"),
														 PlayerPrefs.GetInt("Mi_Desc"),
														 PlayerPrefs.GetInt("S_Desc"));
			//Load Birth
			System.DateTime Birth = new System.DateTime( PlayerPrefs.GetInt("Año_Nacimiento"), 
														 PlayerPrefs.GetInt("Mes_Nacimiento"),
														 PlayerPrefs.GetInt("Dia_Nacimiento"),
														 PlayerPrefs.GetInt("Hora_Nacimiento"),
														 PlayerPrefs.GetInt("Minuto_Nacimiento"),
														 0);
			// Load Comida
			System.DateTime ultimaCom = new System.DateTime( PlayerPrefs.GetInt("A_rC"), 
														  PlayerPrefs.GetInt("Me_rC"),
														  PlayerPrefs.GetInt("D_rC"),
														  PlayerPrefs.GetInt("H_rC"),
														  PlayerPrefs.GetInt("Mi_rC"),
													 	  0);
			//

			Debug.Log("Now = D: " + Now.Day + ", H: " + Now.Hour + ", M: " + Now.Minute + ", S: " + Now.Second);
			//Calculate the diference between the saved time and the atual time
			System.TimeSpan Diferencia = Now - Last;	
			System.TimeSpan DiferenciaCom = Now - ultimaCom;		
			//Put into the label the information
			//Label.GetComponent<Text>().text = "Minutos" + Diferencia.TotalMinutes;
			Label.GetComponent<Text>().text = "Ultima conexión: " + Mathf.RoundToInt((float)(Diferencia.TotalMinutes)) + " min " + " - Ultima Comida: " + Mathf.RoundToInt((float)(DiferenciaCom.TotalSeconds)) + " s";
														
			//Calcular edad
			System.TimeSpan E_dad = Now - Birth;
			if (E_dad.Days > 0)
			{
				Edad.GetComponent<Text>().text = "Edad: " + E_dad.Days + " días";
			}else{
				Edad.GetComponent<Text>().text = "Edad: " + E_dad.Hours + " horas";
			}
			//Calcular el cambio de status
			CS.calcularComida();
			CS.calcularBaño();
			CS.calcularAseo();		
			CS.calcularSueño();
		}
	}	
	
	void OnApplicationQuit()
    {		
		System.DateTime Now = System.DateTime.Now;			
		//Reset Now
		PlayerPrefs.SetInt("A_Desc",Now.Year);
		PlayerPrefs.SetInt("Me_Desc",Now.Month);
		PlayerPrefs.SetInt("D_Desc",Now.Day);
		PlayerPrefs.SetInt("H_Desc",Now.Hour);
		PlayerPrefs.SetInt("Mi_Desc",Now.Minute);
		PlayerPrefs.SetInt("S_Desc",Now.Second);
		PlayerPrefs.SetInt("dormirEnCama",0);	
		CS.Sueño();
	}
	void OnApplicationPause(bool pauseStatus)
    {
		if (pauseStatus) {			
			System.DateTime Now = System.DateTime.Now;			
			//Reset Now
			PlayerPrefs.SetInt("A_Desc",Now.Year);
			PlayerPrefs.SetInt("Me_Desc",Now.Month);
			PlayerPrefs.SetInt("D_Desc",Now.Day);
			PlayerPrefs.SetInt("H_Desc",Now.Hour);
			PlayerPrefs.SetInt("Mi_Desc",Now.Minute);
			PlayerPrefs.SetInt("S_Desc",Now.Second);
			PlayerPrefs.SetInt("dormirEnCama",0);	
			CS.Sueño();
		}        
	}
	void OnApplicationFocus(bool focusStatus)
    {
		if (focusStatus) {			
			System.DateTime Now = System.DateTime.Now;			
			//Reset Now
			PlayerPrefs.SetInt("A_Desc",Now.Year);
			PlayerPrefs.SetInt("Me_Desc",Now.Month);
			PlayerPrefs.SetInt("D_Desc",Now.Day);
			PlayerPrefs.SetInt("H_Desc",Now.Hour);
			PlayerPrefs.SetInt("Mi_Desc",Now.Minute);
			PlayerPrefs.SetInt("S_Desc",Now.Second);	
			PlayerPrefs.SetInt("dormirEnCama",0);			
			CS.Sueño();
		}       
	}
	public void Sleep () {
		SceneManager.LoadScene("sleepScene");
	}
	public void Reset()
	{
		PlayerPrefs.DeleteAll();
	}
}
