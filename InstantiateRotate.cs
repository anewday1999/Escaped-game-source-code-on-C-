using UnityEngine;
using UnityEngine.EventSystems;

public class InstantiateRotate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public Vector2 TouchDist;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;
    private Vector2 tmp;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                tmp = Input.touches[PointerId].position - PointerOld;
                TouchDist = Vector2.Lerp(new Vector2(0, 0), tmp, 0.7f);
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                tmp = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                TouchDist = Vector2.Lerp(new Vector2(0, 0), tmp, 0.7f);
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            tmp = new Vector2(0, 0);
            TouchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

}