using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ConfigCheck : MonoBehaviour
{
	public Image harunaIcon;
	public Image displayStateIcon;

	public Sprite harunaBad;
	public Sprite harunaWarn;
	public Sprite harunaOkA;
	public Sprite harunaOkB;
	public Sprite harunaOkC;
	public Sprite harunaOkD;

	public Sprite modeCvt;
	public Sprite modeSp;

	private string fileName;

	private void Start()
	{
		fileName = Path.Combine("Q:\\proc\\", "current_config.txt");
		StartCoroutine(UpdateDisplay());
	}

	private IEnumerator UpdateDisplay()
	{
		while (true)
		{
			// Use a StreamReader to read the file
			try
			{
				using (StreamReader reader = new StreamReader(fileName))
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
								if (value == "true")
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
}
