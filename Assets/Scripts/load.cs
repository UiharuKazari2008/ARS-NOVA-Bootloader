using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class load : MonoBehaviour
{
	public Text displayText;

	public Text installText;

	public Text stepText;

	public Text modelText;

	public Color warningColor;
	public string warningTrigger;
	public Color defaultColor;

	private string configFilePath;
	private string statusFilePath;
	private string installFilePath;
	private string cfgStateFilePath;

	public bool ContinueRunning = true;
	public string errorSceneName = "error";

	public GameObject coverFrame;

	public Image harunaIcon;
	public Image displayStateIcon;

	public Sprite harunaBad;
	public Sprite harunaWarn;
	public Sprite harunaOkA;
	public Sprite harunaOkB;
	public Sprite harunaOkC;
	public Sprite harunaOkD;

	public Sprite checkCvt;
	public Sprite checkSp;
	public Sprite modeCvt;
	public Sprite modeSp;

	private void Start()
	{
		configFilePath = Path.Combine(Application.streamingAssetsPath, "config.txt");
		statusFilePath = Path.Combine(Application.streamingAssetsPath, "state.txt");
		installFilePath = Path.Combine(Application.streamingAssetsPath, "install.txt");
		cfgStateFilePath = Path.Combine(Application.streamingAssetsPath, "current_config.txt");
		initText();
		StartCoroutine(UpdateDisplay());
	}

	private void initText()
	{
		string[] array = File.ReadAllLines(configFilePath);
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text.StartsWith("Model"))
			{
				string[] array3 = text.Split('=');
				string text5 = array3[0];
				string text2 = array3[1];
				modelText.text = text2;
			}
			if (text.StartsWith("STEP "))
			{
				string[] array4 = text.Split('=');
				string text3 = array4[0];
				string text4 = array4[1];
				displayText.text = text4;
				stepText.text = text3;
				installText.text = "";
			}
		}
	}

	private IEnumerator UpdateDisplay()
	{
		while (ContinueRunning)
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
							displayText.text = text2;
							stepText.text = text1;
							bool result = false;
							if (array5.Length > 2 && bool.TryParse(array5[2], out result) && result == true)
							{
								coverFrame.SetActive(true);
								Time.timeScale = 0.00125f;
							} else if (coverFrame.activeSelf == true)
							{
								Time.timeScale = 1;
								coverFrame.SetActive(false);
							}
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
			// Use a StreamReader to read the file
			try
			{
				string[] lines = File.ReadAllLines(installFilePath);

				if (lines.Length > 0)
				{
					string lastLine = lines[lines.Length - 1];
					if (lastLine.Contains(warningTrigger))
                    {
						installText.color = warningColor;
					} else
                    {
						installText.color = defaultColor;
					}
					installText.text = lastLine;
				}
			}
			catch
			{
			}
			// Use a StreamReader to read the file
			try
			{
				using (StreamReader reader = new StreamReader(cfgStateFilePath))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						if (line.StartsWith("haruna"))
						{
							string[] array5 = line.Split('=');
							if (array5.Length > 1)
							{
								string value = array5[1];
								if (value == "false")
								{
									harunaIcon.overrideSprite = harunaBad;
								}
								else if (value == "true")
								{
									harunaIcon.overrideSprite = harunaWarn;
								}
								else if (value == "A")
								{
									harunaIcon.overrideSprite = harunaOkA;
								}
								else if (value == "B")
								{
									harunaIcon.overrideSprite = harunaOkB;
								}
								else if (value == "C")
								{
									harunaIcon.overrideSprite = harunaOkC;
								}
								else if (value == "D")
								{
									harunaIcon.overrideSprite = harunaOkD;
								}
							}
						}
						else if (line.StartsWith("sp_en"))
						{
							string[] array5 = line.Split('=');
							if (array5.Length > 1)
							{
								string value = array5[1];
								if (value == "found_cvt")
								{
									displayStateIcon.overrideSprite = checkCvt;
								}
								else if (value == "found_sp")
								{
									displayStateIcon.overrideSprite = checkSp;
								}
								else if (value == "true")
								{
									displayStateIcon.overrideSprite = modeSp;
								}
								else
								{
									displayStateIcon.overrideSprite = modeCvt;
								}
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
}
