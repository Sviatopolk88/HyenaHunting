using UnityEngine;
using TMPro;

public class CurrentPlayerText : MonoBehaviour
{
	private TextMeshProUGUI text;
	public static string CurrentPlayer;

	void Start()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	void Update()
	{
		text.text = CurrentPlayer;
	}
}
