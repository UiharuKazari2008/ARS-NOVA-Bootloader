using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bootgame : MonoBehaviour
{
	public string batchFileName = "game.bat";
	public string sceneName = "level0";
	public bool transition = false;
	public bool quit = false;
	public bool showWindow = false;

	private void Start()
	{
		ProcessStartInfo startInfo = new ProcessStartInfo();
		startInfo.FileName = "powershell.exe";
		startInfo.UseShellExecute = true;
		startInfo.CreateNoWindow = showWindow;
		startInfo.WorkingDirectory = Application.streamingAssetsPath;
		if (showWindow)
        {
			startInfo.WindowStyle = ProcessWindowStyle.Normal;
		}
		else
        {
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
		}
		startInfo.Arguments = "-File \"Q:/lib/boot/" + batchFileName + "\"";
		Process process = new Process();
		process.StartInfo = startInfo;
		process.Start();
		if (transition)
		{
			SceneManager.LoadScene(sceneName);
		}
		if (quit)
        {
			Application.Quit();
		}
	}
}
