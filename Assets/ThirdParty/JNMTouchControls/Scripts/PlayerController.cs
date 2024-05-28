using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    #region Private Members

    private float Gravity = 20.0f;
    private Vector3 _moveDirection = Vector3.zero;

    #endregion

    #region Public Members

    public float Speed = 1.0f;
    public float RotationSpeed = 240.0f;
    public StickController MoveStick;

    #endregion

    void Awake()
    {
        if (MoveStick != null)
        {
            MoveStick.StickChanged += MoveStick_StickChanged;
        }
    }

    private Vector2 MoveStickPos = Vector2.zero;

    private void MoveStick_StickChanged(object sender, StickEventArgs e)
    {
        MoveStickPos = e.Position;
    }

    // Use this for initialization
    void Start()
    {
        // You can initialize anything here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Get Input for axis
        float h = MoveStickPos.x;
        float v = MoveStickPos.y;

        // Move the player forward
        transform.position += transform.forward * Speed * Time.deltaTime;

        // Rotate the player based on joystick input
        if (h != 0)
        {
            transform.Rotate(Vector3.up * h * RotationSpeed * Time.deltaTime);
        }

        // Apply gravity
        _moveDirection.y -= Gravity * Time.deltaTime;
    }
}
