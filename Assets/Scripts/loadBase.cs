using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class loadBase : MonoBehaviour
{
	public Text installText;
	public bool DisableTextOnStart = true;
	public GameObject errorText;

	private string installFilePath;

	private void Start()
	{
		installFilePath = Path.Combine("Q:\\proc\\", "install.txt");
		StartCoroutine(UpdateDisplay());
	}

	private IEnumerator UpdateDisplay()
	{
		while (true)
		{
			// Use a StreamReader to read the file
			try
			{
				string[] lines = File.ReadAllLines(installFilePath);

				if (lines.Length > 0)
				{
					string lastLine = lines[lines.Length - 1];
					if (lastLine.Length > 0)
					{
						installText.text = lastLine;
						if (DisableTextOnStart) {
							errorText.active = false;
						}
					}
					else if (DisableTextOnStart)
					{
						errorText.active = true;
					}
				}
			}
			catch
			{
			}

			yield return new WaitForSeconds(0.001f); // Wait for one second before reading the file again
		}
	}
}
