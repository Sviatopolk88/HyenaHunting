using UnityEngine;

public class DiceCheckZone : MonoBehaviour 
{
	[SerializeField] private GameController _gameController;
	private Vector3 _diceVelocity;
	private bool _isRolling = false;

    private void FixedUpdate () 
	{
		_diceVelocity = DiceRoll.DiceVelocity;
	}

	public void IsRolling()
    {
		_isRolling = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
			DiceResultRollText.diceNumber = 1;
			_gameController.CheckRoll();
		}        
		
		if (Input.GetKeyDown(KeyCode.Alpha2))
        {
			DiceResultRollText.diceNumber = 2;
			_gameController.CheckRoll();
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha3))
        {
			DiceResultRollText.diceNumber = 3;
			_gameController.CheckRoll();
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha4))
        {
			DiceResultRollText.diceNumber = 4;
			_gameController.CheckRoll();
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha5))
        {
			DiceResultRollText.diceNumber = 5;
			_gameController.CheckRoll();
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha6))
        {
			DiceResultRollText.diceNumber = 6;
			_gameController.CheckRoll();
		}

    }
    
	private void OnTriggerStay(Collider col)
	{
		if (_diceVelocity == Vector3.zero && _isRolling)
		{
			_isRolling = false;
			
			switch (col.gameObject.name) {
			case "Side1":
				DiceResultRollText.diceNumber = 1;
				break;
			case "Side2":
				DiceResultRollText.diceNumber = 2;
				break;
			case "Side3":
				DiceResultRollText.diceNumber = 3;
				break;
			case "Side4":
				DiceResultRollText.diceNumber = 4;
				break;
			case "Side5":
				DiceResultRollText.diceNumber = 5;
				break;
			case "Side6":
				DiceResultRollText.diceNumber = 6;
				break;
			}
			_gameController.CheckRoll();
		}
	}
	
}
