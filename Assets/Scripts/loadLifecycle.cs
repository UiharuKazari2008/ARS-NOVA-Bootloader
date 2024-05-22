using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadLifecycle : MonoBehaviour
{
	public Text statusText;
	public Text configText;
	public string errorSceneName = "error";

	private string statusFilePath;

	private void Start()
	{
		statusFilePath = Path.Combine("Q:\\proc\\", "lifecycle_state.txt");
		StartCoroutine(UpdateDisplay());
	}

	private IEnumerator UpdateDisplay()
	{
		while (true)
		{
			// Use a StreamReader to read the file
			try
			{
				using (StreamReader reader = new StreamReader(statusFilePath))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						if (line.StartsWith("STEP "))
						{
							string[] array5 = line.Split('=');
							string text1 = array5[0];
							string text2 = array5[1];
							statusText.text = text2;
							//stepText.text = text1;
							bool result = false;
							if (array5.Length > 2 && bool.TryParse(array5[2], out result) && result == true)
							{
								NextScene();
							}
						}
						else if (line.StartsWith("config"))
						{
							string[] array5 = line.Split('=');
							string text1 = array5[0];
							string text2 = array5[1];
							configText.text = text2;
						}
						else if (line.StartsWith("error"))
						{
							string[] array5 = line.Split('=');
							if (array5[1] == "true")
							{
								Time.timeScale = 1;
								LoadErrorScene();
							}
						}
					}
				}
			}
			catch
			{
			}

			yield return new WaitForSeconds(0.001f); // Wait for one second before reading the file again
		}
	}
	private void LoadErrorScene()
	{
		SceneManager.LoadScene(errorSceneName);
	}
	private void NextScene()
	{
		if (File.Exists(Path.Combine("C:\\Windows\\ARS_NOVA\\update\\system_update.ps1")))
		{
			Debug.Log("System Update Found");
			SceneManager.LoadScene("LevelUpdate");
		}
		else if (File.Exists(Path.Combine("Q:\\nvram\\PLATFORM_INSTALLED")))
		{
			Debug.Log("Ready to Boot");
			SceneManager.LoadScene("LevelNormal");
		}
		else
		{
			Debug.Log("Platform Installer Triggred");
			SceneManager.LoadScene("LevelPre");
		}
	}
}
