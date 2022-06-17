using UnityEngine;
using TMPro;

public class DiceResultRollText : MonoBehaviour {

	private TextMeshProUGUI text;
	public static int diceNumber;

	void Start () {
		text = GetComponent<TextMeshProUGUI> ();
	}
	
	void Update () {
		text.text = diceNumber.ToString ();
	}
}
