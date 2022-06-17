using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{

    public int QantityOfPoints = 40;
    [Range(2, 4)]
    public int QantityOfPlayers = 2;

    [SerializeField] private GameObject _pointPref;
    [SerializeField] private Transform _villagePref;
    [SerializeField] private Transform _playerPref;
    [SerializeField] private Transform _pit;

    public static int NumberPoints;
    public static int NumberPlayers; // от 2 до 4 игроков

    private GameController _gameController;
    private List<Color> _colors = new List<Color>();
    private Material _material;

    private int _a = 1;
    private float _b = 0.8f;
    private int _stepSpiral = 90;
    private int _f = 1; // Корректирующий коэффициент (выравнивает расстояние между точками по спирали)

    private List<Transform> _points;
    private List<Transform> _players;

    void Awake()
    {
        NumberPoints = QantityOfPoints;
        NumberPlayers = QantityOfPlayers;

        SetColor();
        _points = GameController.Points;
        _players = GameController.Players;
        _points.Add(_pit);
        for (int i = 0; i < NumberPoints; i++)
        {
            StepSpiral(out float _x, out float _z);


            var point = Instantiate(_pointPref);
            point.name = $"Point {i + 1}";
            point.transform.position = new Vector3(_x, 0.1f, _z);
            _points.Add(point.transform);
            
            if (_f < 15)
            {
                _f++;
            }
            if (_points.Count > 50)
            {
                if (_f < 20)
                {
                    _f++;
                }
            }
        }

        _stepSpiral += 10;

        StepSpiral(out float X, out float Z);
        var village = Instantiate(_villagePref);
        village.position = new Vector3(X, 0.1f, Z);
        _points.Add(village);

        CreatePlayers(village);
    }

    

    private void StepSpiral(out float X, out float Z)
    {
        _stepSpiral += 30 - _f;
        float t = _stepSpiral * Mathf.PI / 180;
        float r = _a + _b * t;
        X = (r * Mathf.Cos(t));
        Z = (r * Mathf.Sin(t));
    }

    private void SetColor()
    {
        _colors.Add(Color.blue);
        _colors.Add(Color.black);
        _colors.Add(Color.grey);
        _colors.Add(Color.red);
    }

    private void CreatePlayers(Transform startPosition)
    {
        for (int i = 0; i < NumberPlayers; i++)
        {
            var player = Instantiate(_playerPref);
            player.name = $"Player {i + 1}";

            var myRenderer = player.GetComponent<Renderer>();
            _material = myRenderer.material;
            _material.color = _colors[i];

            var x = 0;
            var z = 0;

            switch (i)
            {
                case 0:
                    x = 1;
                    z = 1;
                    break;
                case 1:
                    x = 1;
                    z = -1;
                    break;
                case 2:
                    x = -1;
                    z = 1;
                    break;
                case 3:
                    x = -1;
                    z = -1;
                    break;
                default:
                    break;
            }
            player.position = startPosition.position + new Vector3(x, 1, z);
            _players.Add(player);
        }
    }
}
