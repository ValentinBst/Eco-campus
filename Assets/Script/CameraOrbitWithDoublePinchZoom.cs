using UnityEngine;
using Leap;
using Leap.Unity; // Ces namespaces sont nécessaires pour les fonctionnalités Ultraleap

public class CameraOrbitWithDoublePinchZoom : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float rotationSpeed = 50.0f;
    public float pinchZoomSensitivity = 0.1f; // La sensibilité du zoom de pincement
    public float distanceMin = 2f;
    public float distanceMax = 15f;

    private LeapServiceProvider leapServiceProvider;
    private float x = 0.0f;
    private float y = 0.0f;
    private float cameraRotation = 0.0f;
    private float lastPinchDistance = 0f;
    private bool isDoublePinching = false;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        cameraRotation = angles.z;

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        leapServiceProvider = FindObjectOfType<LeapServiceProvider>();
    }

    void Update()
    {
        Frame frame = leapServiceProvider.CurrentFrame;
        if (frame.Hands.Count == 2)
        {
            Hand firstHand = frame.Hands[0];
            Hand secondHand = frame.Hands[1];

            if (firstHand.IsPinching() && secondHand.IsPinching())
            {
                // Calculez la distance moyenne entre les doigts pour les deux mains
                float currentPinchDistance = (firstHand.PinchDistance + secondHand.PinchDistance) / 2;

                if (!isDoublePinching)
                {
                    lastPinchDistance = currentPinchDistance;
                    isDoublePinching = true;
                }
                else
                {
                    float pinchAmount = currentPinchDistance - lastPinchDistance;
                    distance -= pinchAmount * pinchZoomSensitivity;
                    distance = Mathf.Clamp(distance, distanceMin, distanceMax);
                    lastPinchDistance = currentPinchDistance;
                }
            }
            else
            {
                isDoublePinching = false;
            }
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            x += Input.GetAxis("Horizontal") * xSpeed * distance * Time.deltaTime;
            y -= Input.GetAxis("Vertical") * ySpeed * Time.deltaTime;

            y = ClampAngle(y, -20f, 80f);

            Quaternion rotation = Quaternion.Euler(y, x, cameraRotation);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
