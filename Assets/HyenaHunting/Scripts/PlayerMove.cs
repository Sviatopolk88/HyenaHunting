using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 5;
    private Transform _target;
    private bool _isStay = true;
    public void MovePawn(Transform target)
    {
        _target = target;
        _isStay = false;
    }

    private void Update()
    {
        if (!_isStay && _target != null)
            Move(_target);
    }

    private void Move(Transform target)
    {
        var step = speed * Time.deltaTime;
        var _target = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _target, step);

    }
}
