using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR

#endif

[RequireComponent(typeof(RectTransform))]
public class MSACCJoystick : UIBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Vector2 _axis;
    private bool _isDragging;

    public RectTransform _joystickGraphic;

    private RectTransform _rectTransform;

    [HideInInspector] public float joystickX;

    [HideInInspector] public float joystickY;

    public RectTransform rectTransform
    {
        get
        {
            if (!_rectTransform) _rectTransform = transform as RectTransform;
            return _rectTransform;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsActive()) return;
        EventSystem.current.SetSelectedGameObject(gameObject, eventData);
        Vector2 newAxis = transform.InverseTransformPoint(eventData.position);
        newAxis.x /= rectTransform.sizeDelta.x * 0.5f;
        newAxis.y /= rectTransform.sizeDelta.y * 0.5f;
        SetAxisMS(newAxis);
        _isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position,
            eventData.pressEventCamera, out _axis);
        _axis.x /= rectTransform.sizeDelta.x * 0.5f;
        _axis.y /= rectTransform.sizeDelta.y * 0.5f;
        SetAxisMS(_axis);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }

    private void OnDeselect()
    {
        _isDragging = false;
    }

    private void LateUpdate()
    {
        if (!_isDragging)
            if (_axis != Vector2.zero)
            {
                var newAxis = _axis - _axis * Time.deltaTime * 25.0f;
                if (newAxis.sqrMagnitude <= 0.1f) newAxis = Vector2.zero;
                SetAxisMS(newAxis);
            }
    }

    public void SetAxisMS(Vector2 axis)
    {
        _axis = Vector2.ClampMagnitude(axis, 1);
        UpdateJoystickGraphicMS();
        joystickY = _axis.y;
        joystickX = _axis.x;
    }

    private void UpdateJoystickGraphicMS()
    {
        if (_joystickGraphic)
            _joystickGraphic.localPosition =
                _axis * Mathf.Max(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y) * 0.5f;
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        UpdateJoystickGraphicMS();
    }
#endif
}