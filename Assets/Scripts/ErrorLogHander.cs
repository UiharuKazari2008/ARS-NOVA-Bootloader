using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class ErrorLogHander : MonoBehaviour
{
	public string errorFile = "config_errors.txt";

	public Text messageText;
	public GameObject parentObject;

	private string path;
	private string tempText;
	private string[] fileContents;

	private void Start()
	{
		path = Path.Combine("Q:\\proc\\", errorFile); 
		StartCoroutine(UpdateDisplay());
	}

	private IEnumerator UpdateDisplay()
    {
		while (true)
		{
			try
			{
				if (File.Exists(path))
				{
					fileContents = File.ReadAllLines(path);
					var result = string.Empty;
					foreach (var item in fileContents)
					{
						if (item.Length > 1)
						{
							result += item;
							result += "\n";
						}
					}
					if (result.Length > 0)
					{
						messageText.text = result;
						parentObject.SetActive(true);
					}
				}
				else
				{
					Debug.LogError("File Missing");
				}
			}
			catch
			{
			}

			yield return new WaitForSeconds(0.001f); // Wait for one second before reading
		}
	}
}
