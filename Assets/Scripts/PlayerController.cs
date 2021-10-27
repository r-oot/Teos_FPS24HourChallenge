using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private GlobalVariables.GameStates gameStates;

    [Header("Movements")]
    [SerializeField, Range(50,100)]
    private float mouseSensitivity = 100;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float x = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float y = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        playerMovement.ChangeRotationLook(x, y);

    }
}
