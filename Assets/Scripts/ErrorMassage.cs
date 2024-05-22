using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMassage : MonoBehaviour
{
	public String errorFile = "state.txt";

	public Text errorText;

	public Text messageText;

	private void Start()
	{
		string path = Path.Combine("Q:\\proc\\", errorFile);
		if (File.Exists(path))
		{
			string[] array = File.ReadAllLines(path);
			string text = null;
			string text2 = null;
			string[] array2 = array;
			foreach (string text3 in array2)
			{
				if (text3.Contains("errorno="))
				{
					text = text3.Split('=')[1];
				}
				else if (text3.Contains("errormessage="))
				{
					text2 = text3.Split('=')[1];
				}
				if (text != null && text2 != null)
				{
					break;
				}
			}
			if (text != null)
			{
				errorText.text = text.Replace("\\n", Environment.NewLine);
			}
			else
			{
				Debug.LogError("errorno Cant Get");
			}
			if (text2 != null)
			{
				messageText.text = text2.Replace("\\n", Environment.NewLine);
			}
			else
			{
				Debug.LogError("errormessage Cant get");
			}
		}
		else
		{
			Debug.LogError("File Missing");
		}
	}
}
