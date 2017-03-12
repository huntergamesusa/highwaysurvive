using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ControlMovementDirection
{
	Horizontal = 0x1,
	Vertical = 0x2,
	Both = Horizontal | Vertical
}

public class NateJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler{

	public Camera CurrentEventCamera { get; set; }

	public ControlMovementDirection JoystickMoveAxis = ControlMovementDirection.Both;

	public bool SnapsToFinger = true;

	public float MovementRange = 50f;

	public bool HideOnRelease;
	public bool MoveBase = true;

	public Image JoystickBase;
	public Image Stick;
	public RectTransform TouchZone;

	private Vector2 _initialStickPosition;
	private Vector2 _intermediateStickPosition;
	private Vector2 _initialBasePosition;
	private RectTransform _baseTransform;
	private RectTransform _stickTransform;
	private float _oneOverMovementRange;

	// Use this for initialization
	void Awake () {
		_stickTransform = Stick.GetComponent<RectTransform>();
		_baseTransform = JoystickBase.GetComponent<RectTransform>();

		_initialStickPosition = _stickTransform.anchoredPosition;
		_intermediateStickPosition = _initialStickPosition;
		_initialBasePosition = _baseTransform.anchoredPosition;

		_stickTransform.anchoredPosition = _initialStickPosition;
		_baseTransform.anchoredPosition = _initialBasePosition;

		_oneOverMovementRange = 1f / MovementRange;
		if (HideOnRelease)
		{
			Hide(true);
		}
	}
	
	public virtual void OnDrag(PointerEventData eventData)
	{
		// Unity remote multitouch related thing
		// When we feed fake PointerEventData we can't really provide a camera, 
		// it has a lot of private setters via not created objects, so even the Reflection magic won't help a lot here
		// Instead, we just provide an actual event camera as a public property so we can easily set it in the Input Helper class
		CurrentEventCamera = eventData.pressEventCamera ?? CurrentEventCamera;

		// We get the local position of the joystick
		Vector3 worldJoystickPosition;
		RectTransformUtility.ScreenPointToWorldPointInRectangle(_stickTransform, eventData.position,
			CurrentEventCamera, out worldJoystickPosition);

		// Then we change it's actual position so it snaps to the user's finger
		_stickTransform.position = worldJoystickPosition;
		// We then query it's anchored position. It's calculated internally and quite tricky to do from scratch here in C#
		var stickAnchoredPosition = _stickTransform.anchoredPosition;

		// Some bitwise logic for constraining the joystick along one of the axis
		// If the "Both" option was selected, non of these two checks will yield "true"
		if ((JoystickMoveAxis & ControlMovementDirection.Horizontal) == 0)
		{
			stickAnchoredPosition.x = _intermediateStickPosition.x;
		}
		if ((JoystickMoveAxis & ControlMovementDirection.Vertical) == 0)
		{
			stickAnchoredPosition.y = _intermediateStickPosition.y;
		}

		_stickTransform.anchoredPosition = stickAnchoredPosition;

		// Find current difference between the previous central point of the joystick and it's current position
		Vector2 difference = new Vector2(stickAnchoredPosition.x, stickAnchoredPosition.y) - _intermediateStickPosition;

		// Normalisation stuff
		var diffMagnitude = difference.magnitude;
		var normalizedDifference = difference / diffMagnitude;

		// If the joystick is being dragged outside of it's range
		if (diffMagnitude > MovementRange)
		{
			if (MoveBase && SnapsToFinger)
			{
				// We move the base so it maps the new joystick center position
				var baseMovementDifference = difference.magnitude - MovementRange;
				var addition = normalizedDifference * baseMovementDifference;
				_baseTransform.anchoredPosition += addition;
				_intermediateStickPosition += addition;
			}
			else
			{
				_stickTransform.anchoredPosition = _intermediateStickPosition + normalizedDifference * MovementRange;
			}
		}

		// We should now calculate axis values based on final position and not on "virtual" one
		var finalStickAnchoredPosition = _stickTransform.anchoredPosition;
		// Sanity recalculation
		Vector2 finalDifference = new Vector2(finalStickAnchoredPosition.x, finalStickAnchoredPosition.y) - _intermediateStickPosition;
		// We don't need any values that are greater than 1 or less than -1
	

	}

	public void OnPointerUp(PointerEventData eventData)
	{
		// When we lift our finger, we reset everything to the initial state
		_baseTransform.anchoredPosition = _initialBasePosition;
		_stickTransform.anchoredPosition = _initialStickPosition;
		_intermediateStickPosition = _initialStickPosition;

		// We also hide it if we specified that behaviour
		if (HideOnRelease)
		{
			Hide(true);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		// When we press, we first want to snap the joystick to the user's finger
		if (SnapsToFinger)
		{
			CurrentEventCamera = eventData.pressEventCamera ?? CurrentEventCamera;

			Vector3 localStickPosition;
			Vector3 localBasePosition;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(_stickTransform, eventData.position,
				CurrentEventCamera, out localStickPosition);
			RectTransformUtility.ScreenPointToWorldPointInRectangle(_baseTransform, eventData.position,
				CurrentEventCamera, out localBasePosition);

			_baseTransform.position = localBasePosition;
			_stickTransform.position = localStickPosition;
			_intermediateStickPosition = _stickTransform.anchoredPosition;
		}
		else
		{
			OnDrag(eventData);
		}
		// We also want to show it if we specified that behaviour
		if (HideOnRelease)
		{
			Hide(false);
		}
	}

	private void Hide(bool isHidden)
	{
		JoystickBase.gameObject.SetActive(!isHidden);
		Stick.gameObject.SetActive(!isHidden);
	}

}
