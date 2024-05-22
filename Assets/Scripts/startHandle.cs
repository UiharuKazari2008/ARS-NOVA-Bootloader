using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startHandle : MonoBehaviour
{
	public string batchFileName = "game.bat";
	public bool showWindow = false;

	private void Start()
	{
		ProcessStartInfo startInfo = new ProcessStartInfo();
		startInfo.FileName = "powershell.exe";
		startInfo.UseShellExecute = true;
		startInfo.CreateNoWindow = showWindow;
		startInfo.WorkingDirectory = Path.Combine("Q:\\lib\\boot");
		if (showWindow)
        {
			startInfo.WindowStyle = ProcessWindowStyle.Normal;
		}
		else
        {
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
		}
		startInfo.Arguments = "-File \"" + Path.Combine("Q:\\lib\\boot") + "/" + batchFileName + "\"";
		Process process = new Process();
		process.StartInfo = startInfo;
		process.Start();
	}
}
