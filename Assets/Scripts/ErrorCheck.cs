using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorCheck : MonoBehaviour
{
	public string fileName = "config.txt";

	public string errorSceneName = "error";

	private void Start()
	{
		string path = Path.Combine("Q:\\proc\\", fileName);
		if (File.Exists(path))
		{
			string[] array = File.ReadAllLines(path);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains("error=true"))
				{
					LoadErrorScene();
					break;
				}
			}
		}
		else
		{
			Debug.LogError("指定されたファイルが見つかりません: " + fileName);
		}
	}

	private void LoadErrorScene()
	{
		SceneManager.LoadScene(errorSceneName);
	}
}
