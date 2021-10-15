using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{   
    [HideInInspector]public bool canControl;
    private bool hover;
    private float rollInput;
    private float startSpeed;
    [SerializeField]private float moveSpeed = 25;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;
    [SerializeField]private float lookRateSpeed = 90f;
    [SerializeField]private float rollSpeed = 90f, rollAcceleration = 3.5f;
    [SerializeField]private Vector2 lookInput, screenCenter, mouseDist;
    [SerializeField]private bool Controller;
    [SerializeField] private GameObject HoverUI;

    private void Awake() {
        startSpeed = moveSpeed;
        gameObject.SetActive(false);
    }
    private void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        screenCenter = new Vector2(Screen.width * .5f, Screen.height * .5f);
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) {
            //boost
            moveSpeed = 75;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1)) {
            //reset boost
            moveSpeed = startSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Cursor.visible = false;     
        }
        if(canControl){
            if(Input.GetKeyDown(KeyCode.LeftAlt)){
                hover = !hover;
                if(hover){
                    HoverUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.lockState = CursorLockMode.Confined;
                }
                else {
                    HoverUI.SetActive(false);
                }
            }
            lookInput = Input.mousePosition;
            mouseDist.x = (lookInput.x - screenCenter.x) / screenCenter.y;
            mouseDist.y = (lookInput.y - screenCenter.y) / screenCenter.y;
            mouseDist = Vector2.ClampMagnitude(mouseDist, 1f);
            if (!hover){
                if(Controller){
                    rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("RollController"), rollAcceleration * Time.deltaTime);
                    transform.Rotate(Input.GetAxisRaw("RollControllerHor") * lookRateSpeed * Time.deltaTime, Input.GetAxisRaw("RollControllerVer") * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);
                    activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("HoverController") * moveSpeed, hoverAcceleration * Time.deltaTime);
                }else{
                    rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
                    transform.Rotate(-mouseDist.y * lookRateSpeed * Time.deltaTime, mouseDist.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);
                    activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * moveSpeed, hoverAcceleration * Time.deltaTime);
                }
                activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * moveSpeed, forwardAcceleration * Time.deltaTime);
                activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * moveSpeed, strafeAcceleration * Time.deltaTime);

                transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
                transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);
            }
        }
    }
}
