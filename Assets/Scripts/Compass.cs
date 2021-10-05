using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    float compassUnit;
    List<CompassMarker> markers = new List<CompassMarker>();
    [SerializeField]private RawImage compassImage;
    [SerializeField]private RawImage markerImage;
    [SerializeField]private Transform drone;
    [SerializeField]private GameObject iconPrefab;
    [SerializeField]private GameObject iconPrefabBig;
    [SerializeField]private CompassMarker[] markersToAdd;
    [SerializeField]private CompassMarker Tower;

    private void Start() {
        compassUnit = compassImage.rectTransform.rect.width / 360f;
        for (int i = 0; i < markersToAdd.Length; i++)
        {
            AddCompassMarker(markersToAdd[i]);
        }
        AddTower(Tower);
    }
    // Update is called once per frame
    void Update()
    {   for (int i = 0; i < markersToAdd.Length; i++)
        {
            if(markersToAdd[i]==null){
                markerImage.gameObject.transform.GetChild(i).GetComponent<Image>().enabled = false;
            }
        }
        if(!gameObject.GetComponent<GameManager>().Towermode){
            compassImage.gameObject.SetActive(true);
            markerImage.gameObject.SetActive(true);
            compassImage.uvRect = new Rect(drone.localEulerAngles.y / 360f, 0f, 1f, 1f);
            foreach (CompassMarker marker in markers)
            {
                if(marker!= null){
                    marker.image.rectTransform.anchoredPosition = GetPosCompass(marker);  
                }
            }
        }
        else{
            compassImage.gameObject.SetActive(false);
            markerImage.gameObject.SetActive(false);
        }
    }
    public void AddCompassMarker(CompassMarker marker){
        GameObject newMarker = Instantiate(iconPrefab, markerImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.compassIcon;
        markers.Add(marker);
    }
    public void AddTower(CompassMarker marker){
        GameObject newMarker = Instantiate(iconPrefabBig, markerImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.compassIcon;
        markers.Add(marker);
    }

    private Vector2 GetPosCompass(CompassMarker marker){
        Vector2 result = Vector2.zero;
        Vector2 playerPos = new Vector2(drone.transform.position.x, drone.transform.position.z);
        Vector2 playerFrwd = new Vector2(drone.transform.forward.x, drone.transform.forward.z);
        try{
            float angle = Vector2.SignedAngle(marker.GetPosition() - playerPos, playerFrwd);
            result = new Vector2(compassUnit * angle, 0f);
        }
        catch{
            marker.image = null;
        }
        return result;
    }
}
