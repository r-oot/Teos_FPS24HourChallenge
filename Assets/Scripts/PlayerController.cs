using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    private PlayerMovement playerMovement;
    private Gun gun;
    private ShootSign shootSign;

    [Header("Movements")]
    [SerializeField, Range(50, 100)]
    private float mouseSensitivity = 100;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        gun = GetComponentInChildren<Gun>();
        shootSign = FindObjectOfType<ShootSign>();    
    }

    private void Update()
    {
        if (GameManager.Instance.GameState.Equals(GlobalVariables.GameStates.InGame))
        {
            float rotateX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
            float rotateY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
            playerMovement.ChangeRotationLook(rotateX, rotateY);

            if (Input.GetMouseButtonDown(0))
            {
                gun.Fire();
                shootSign.ShootedBullet = 1;
            }
            MoveKeyControls();
        }
    }
    public void MoveKeyControls()
    {
        float moveX, moveZ;
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * moveX + transform.forward * moveZ;
        playerMovement.MoveCharacter(direction);
    }
}
