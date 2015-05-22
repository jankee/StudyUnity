using UnityEngine;
using System.Collections;

public class MousePoint : MonoBehaviour 
{
    float velocity = 1.0f;
    LayerMask layer;

    CharacterController _controller;

    bool _isMove = false;
    Vector3 _destination = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () 
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _isMove = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        RaycastHit _hit = new RaycastHit();

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, layer))
            {
                _destination = _hit.point;
                _isMove = true;
            }
        }

        Move();

        //if (_isMove)
        //{
        //    Move();
        //}
	}

    void Move()
    {
        if (Vector3.Distance(transform.position, _destination) == 0.0f)
        {
            _isMove = false;
            return;
        }

        Vector3 direction = _destination - this.transform.position;
        direction = Vector3.Normalize(direction);

        _controller.Move(direction * Time.deltaTime * velocity);
    }
}
