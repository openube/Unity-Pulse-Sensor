using UnityEngine;
using System.Collections;
using System.Threading;
using System.IO.Ports;
using UnityEngine.UI;

public class DetectPulseRate : MonoBehaviour 
{
	public static SerialPort serialPort;
	public static string streamRead;
	public static int BPM;

	public GameObject heartImg;
	public GameObject heartText;

	void Start() 
	{
		OpenConnection();
	}

	public void OpenConnection() 
	{
		serialPort = new SerialPort("/dev/tty.usbmodem1421", 9600, Parity.None, 8, StopBits.One);

		if(serialPort != null) 
		{
			if(serialPort.IsOpen) 
			{
				serialPort.Close();
				Debug.Log ("Chiudo la porta, perché è già aperta");
			}
			else 
			{
				serialPort.Open();
				serialPort.ReadTimeout = 100;
				Debug.Log("Porta aperta!");
			}
		}
		else 
		{
			if(serialPort.IsOpen)
				Debug.Log("La porta è già aperta");
		}
	}

	void FixedUpdate()
	{
		if(serialPort.IsOpen && serialPort.BytesToRead > 0)
		{
			streamRead = serialPort.ReadLine();
			BPM = int.Parse(streamRead);

			heartImg.transform.localScale = new Vector3(0, 0, 1);
		}

		Vector3 heartScale = new Vector3(BPM / 100.0f, BPM / 100.0f, 1);

		heartText.GetComponent<Text>().text = "BPM " + BPM;
		heartImg.transform.localScale = Vector3.Lerp(heartImg.transform.localScale, heartScale, 0.1f);

		serialPort.BaseStream.Flush();
	}
	
	void OnApplicationQuit()
	{
		// Chiudo la porta quando esco

		if (serialPort != null)
			serialPort.Close();
	}
}
