using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject _pref;

    private const float RotationSpeed = 1;
    private const float RunAwaySpeed = 1;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        
        float t = Time.time;

        float radius = RunAwaySpeed * t;
        float angle = RotationSpeed * t;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        if (_timer >= 0.5)
        {
            Instantiate(_pref);
            _pref.transform.position = new Vector3(x, 0.1f, y);
            _timer = 0;
        }
        

    }
}
