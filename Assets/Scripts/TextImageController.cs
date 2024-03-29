using UnityEngine;
using UnityEngine.UI;

public class TextImageController : MonoBehaviour
{
	public Text textField;

	public RawImage imageField;

	public float imageOffset = 20f;

	private float yOffset;

	private void Start()
    {
		yOffset = imageField.rectTransform.localPosition.y;
	}
	
	private void Update()
	{
		float num = textField.preferredWidth / 2f + imageOffset;
		imageField.rectTransform.localPosition = new Vector3(0f - num, yOffset, 0f);
	}

	public void ChangeText(string newText)
	{
		textField.text = newText;
	}
}
