using UnityEngine;

public class PlinkoBallController : MonoBehaviour
{
    private bool _isDragging = false;
    private Rigidbody2D _rb;
    private Vector2 _touchStartPos, _touchEndPos, _dragDirection;
    public float dragForce = 10;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ControllBall();
    }

    private void ControllBall()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _isDragging = true;
                    _touchStartPos = Camera.main.ScreenToWorldPoint(touch.position);
                    break;

                case TouchPhase.Moved:
                    _touchEndPos = Camera.main.ScreenToWorldPoint(touch.position);
                    _dragDirection = (_touchEndPos - _touchStartPos).normalized;
                    break;

                case TouchPhase.Ended:
                    _isDragging = false;
                    _rb.velocity = _dragDirection * dragForce;
                    break;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _touchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            _rb.WakeUp();
            _rb.velocity = _dragDirection * dragForce;
        }
        else if (_isDragging)
        {
            _touchEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _dragDirection = (_touchEndPos - _touchStartPos).normalized;
        }

        if (_isDragging)
        {
            _rb.Sleep();
        }
    }
}
