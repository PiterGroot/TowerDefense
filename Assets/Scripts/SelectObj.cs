using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectObj : MonoBehaviour
{
    [HideInInspector]public GameObject CurrentState;
    [HideInInspector] public int State;
    private bool UpgradeUI;
    [SerializeField]private LayerMask layerMask;
    private bool hasSelectedObj;
    public bool canSelect;
    [SerializeField]private Camera mainCamera;
    [SerializeField] private Animator UpgradeAnim;
    [Header("UI refrences")]
    [SerializeField] private TextMeshProUGUI Kills;
    [SerializeField] private TextMeshProUGUI killsShadow;
    [SerializeField] private TextMeshProUGUI Sellprice;
    [SerializeField] private TextMeshProUGUI SellpriceShadow;
    [SerializeField] private Image Icon;
    [SerializeField] private Sprite[] icons;

    // Start is called before the first frame update
    void Start()
    {
        canSelect = true;
        layerMask = gameObject.GetComponent<Building>().ignoredLayer;
    }

    // Update is called once per frame
    void Update()
    {
        if(canSelect){
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                if(hasSelectedObj){
                    foreach (GameObject turret in GameObject.FindGameObjectsWithTag("Turret"))
                    {
                        turret.GetComponent<Turret>().DisableSelect();
                    }
                    hasSelectedObj = false;
                }
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ~layerMask)) {
                    if(raycastHit.collider.gameObject.CompareTag("Turret")){
                        State = 0;
                        raycastHit.collider.gameObject.GetComponent<Turret>().SelectTurret();
                        SetUpUI(raycastHit.collider.gameObject, 0);
                        UpgradeAnim.SetTrigger("Appear");
                        FindObjectOfType<UpgradeUILogic>().EnableUI();
                        UpgradeUI = true;
                        hasSelectedObj = true;
                    }
                    else if (raycastHit.collider.gameObject.name == "AOWCollider") {
                        State = 1;
                        raycastHit.collider.gameObject.GetComponentInParent<AOWTurret>().SelectTurret();
                        SetUpUI(raycastHit.collider.gameObject, 1);
                        UpgradeAnim.SetTrigger("Appear");
                        FindObjectOfType<UpgradeUILogic>().EnableUI();
                        UpgradeUI = true;
                        hasSelectedObj = true;
                    }
                    else if (raycastHit.collider.gameObject.CompareTag("GoldMine")) {
                        State = 2;
                        raycastHit.collider.gameObject.GetComponent<GoldMine>().SelectTurret();
                        SetUpUI(raycastHit.collider.gameObject, 2);
                        UpgradeAnim.SetTrigger("Appear");
                        FindObjectOfType<UpgradeUILogic>().EnableUI();
                        UpgradeUI = true;
                        hasSelectedObj = true;
                    }
                    else {
                        if (UpgradeUI) {
                            UpgradeAnim.SetTrigger("Disappear");
                            FindObjectOfType<UpgradeUILogic>().isActive = false;
                            UpgradeUI = false;
                        }
                    }
                }
            }
        }
    }
    public void DeselectAll(){
        foreach (GameObject turret in GameObject.FindGameObjectsWithTag("Turret"))
        {
            turret.GetComponent<Turret>().DisableSelect();
        }
        if (UpgradeUI) {
            UpgradeAnim.SetTrigger("Disappear");
            FindObjectOfType<UpgradeUILogic>().isActive = false;
            UpgradeUI = false;
        }
    }

    public void SetUpUI(GameObject obj, int type) {
        switch (type) {
            case 0:
                CurrentState = obj;
                Icon.sprite = icons[0];
                Icon.SetNativeSize();
                Sellprice.text = "$75";
                SellpriceShadow.text = "$75";

                break;
            case 1:
                CurrentState = obj.transform.parent.gameObject;
                Icon.sprite = icons[1];
                Icon.SetNativeSize();
                Sellprice.text = "$150";
                SellpriceShadow.text = "$150";
                break;
            case 2:
                CurrentState = obj;
                Icon.sprite = icons[2];
                Icon.SetNativeSize();
                Sellprice.text = "$225";
                SellpriceShadow.text = "$225";
                break;
        }
    }
}
