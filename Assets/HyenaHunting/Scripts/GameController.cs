using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static List<Transform> Players = new List<Transform>();
    public static List<Transform> Points = new List<Transform>();

    [SerializeField] private Transform _hyenaPref;
    [SerializeField] private GameObject _panelGameOver;

    private List<int> _playersCurrentPosition = new List<int>();
    private List<bool> _playersDirection = new List<bool>();
    private List<bool> _playersHyena = new List<bool>();

    private int _index = 0;
    private int _repeatRoll = 0;
    private int _numberPoints;
    private Transform _currentPlayer;

    private bool _isEndGame = false;

    private void Start()
    {
        Time.timeScale = 4;

        _numberPoints = Points.Count - 1;

        CurrentPlayerText.CurrentPlayer = Players[0].name;
        _currentPlayer = Players[0];

        for (int i = 0; i < Players.Count; i++)
        {
            _playersCurrentPosition.Add(_numberPoints);
            _playersDirection.Add(true);
            _playersHyena.Add(false);
        }
    }

    public void CheckRoll()
    {
        var currentPosition = _playersCurrentPosition[_index];
        var diceResult = DiceResultRollText.diceNumber;

        var direction = _playersDirection[_index];

        var isHyena = _playersHyena[_index];

        // Проверка старта из деревни
        if (currentPosition == _numberPoints && diceResult != 6)
        {
            ChangeOrderMove();
            return;
        }
        // Проверка выхода от колодца
        if (currentPosition == 0 && diceResult != 6)
        {
            ChangeOrderMove();
            return;
        }
        // Проверка на попадание к колодцу
        if (!isHyena)
        {
            if (currentPosition < 6 && diceResult > currentPosition && direction)
            {
                _repeatRoll = 0;
                ChangeOrderMove();
                return;
            }
        }
        else
        {
            // Проверка на попадание к колодцу гиены, если остался 1 шаг
            if (direction && currentPosition == 1 && diceResult == 1)
            {
                Debug.Log("Исключение");
                currentPosition += -diceResult;
                _playersDirection[_index] = false;

                MovePawn(currentPosition);

                _repeatRoll = 0;
                ChangeOrderMove();
                return;
            }
            else if (currentPosition < 10 && diceResult * 2 > currentPosition && direction)
            {
                ChangeOrderMove();
                return;
            }
        }
        // Проверка направления движения к колодцу/ от колодца
        if (direction)
        {
            if (!isHyena)
            {
                currentPosition += -diceResult;
                if (currentPosition == 0)
                {
                    _playersDirection[_index] = false;
                }
            }
            else
            {
                currentPosition += -diceResult * 2;
                if (currentPosition == 0)
                {
                    _playersDirection[_index] = false;
                }
            }
        }
        // Проверка на возвращение в деревню
        else if (!direction && isHyena && diceResult * 2 >= _numberPoints - currentPosition)
        {
            currentPosition = _numberPoints;
            MovePawn(currentPosition);
            _isEndGame = true;
            return;
        }
        else if  (!direction && !isHyena && diceResult >= _numberPoints - currentPosition)
        {
            currentPosition = _numberPoints;

            var currentPlayer = Players[_index];
            var hyena = Instantiate(_hyenaPref);
            hyena.name = $"Hyena {_index + 1}";
            hyena.position = currentPlayer.position;
            Destroy(currentPlayer.gameObject);
            Players[_index] = hyena;
            _playersHyena[_index] = true;
            _currentPlayer = Players[_index];
            MovePawn(currentPosition);

            _playersDirection[_index] = true;

            _repeatRoll = 0;
            ChangeOrderMove();
            return;
        }
        else
        {
            if (!isHyena)
            {
                currentPosition += diceResult;
            }
            else
            {
                currentPosition += diceResult * 2;
                EatOpponent(currentPosition);
            }
        }

        MovePawn(currentPosition);

        // Повторный ход, если выпало 6 на кубике
        if (diceResult == 6 && _repeatRoll < 2)
        {
            _repeatRoll++;
            return;
        }
        else
        {
            _repeatRoll = 0;
        }

        ChangeOrderMove();
    }

    private void MovePawn(int targetPosition)
    {
        var targetPoint = Points[targetPosition];
        _currentPlayer.GetComponent<PlayerMove>().MovePawn(targetPoint);
        _playersCurrentPosition[_index] = targetPosition;
    }

    private void EatOpponent(int hyenaPosition)
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (i != _index && _playersCurrentPosition[i] == hyenaPosition)
            {
                Destroy(Players[i].gameObject);
                Players.RemoveAt(i);
            }
        }
    }

    private void Update()
    {
        if (_isEndGame)
        {
            _panelGameOver.SetActive(true);
        }
    }


    public void ChangeOrderMove()
    {
        _index++;
        if (_index == Players.Count) _index = 0;
        _currentPlayer = Players[_index];
        CurrentPlayerText.CurrentPlayer = _currentPlayer.name;
    }

}
