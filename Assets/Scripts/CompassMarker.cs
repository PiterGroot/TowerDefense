using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassMarker : MonoBehaviour
{
    [SerializeField]public Sprite compassIcon;
    [SerializeField]public Image image;
    public Vector2 GetPosition(){
        Vector2 result = Vector2.zero;
        if(gameObject == null){
            result = new Vector2(6666, 6666);
        }
        else{
            result = new Vector2(transform.position.x, transform.position.z);
        }
        return result;
    }
}
