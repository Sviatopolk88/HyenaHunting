using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public List<Transform> Sides = new List<Transform>();
    public static Vector3 DiceVelocity;

    private Rigidbody _body;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();

    }
    public void Roll()
    {
        DiceResultRollText.diceNumber = 0;
        transform.position = new Vector3(30, 4, -11);
        transform.rotation = Quaternion.identity;
        
        float x = Random.Range(5, 20) * 30;
        float y = Random.Range(5, 15) * 30;
        float z = Random.Range(5, 20) * 30;
        Vector3 rolling = new Vector3(x, y, z);
        
        _body.AddForce(rolling);
        _body.AddTorque(rolling);
    }

    private void Update()
    {
        DiceVelocity = _body.velocity;
    }

}
