using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    private Managers managers;
    private PlayerMovement playerMovement;
    private Gun gun;
    private ShootSign shootSign;

    [Header("Movements")]
    [SerializeField, Range(50, 100)]
    private float mouseSensitivity = 100;
    private void Awake()
    {
        managers = FindObjectOfType<Managers>();
        playerMovement = GetComponent<PlayerMovement>();
        gun = GetComponentInChildren<Gun>();
        shootSign = FindObjectOfType<ShootSign>();    
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (managers.GameManager.GameState.Equals(GlobalVariables.GameStates.InGame))
        {
            float x = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
            float y = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
            playerMovement.ChangeRotationLook(x, y);

            if (Input.GetMouseButtonDown(0))
            {
                gun.Fire();
                shootSign.ShootedBullet = 1;
            }
        }
    }
}
