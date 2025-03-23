using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public float bobSpeed = 5f;
    public float bobAmount = 0.05f;
    public float swayAmount = 0.05f;
    public float smoothDamping = 5f;

    private float timer = 0f;
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.localPosition;
    }

    public void ApplyHeadBob(float speed)
    {
        if (speed > 0.1f)
        {
            timer += Time.deltaTime * bobSpeed * speed;
            float yOffset = Mathf.Sin(timer) * bobAmount * speed;
            float xOffset = Mathf.Cos(timer / 2) * swayAmount * speed;

            Vector3 targetPosition = startPosition + new Vector3(xOffset, yOffset, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothDamping);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * smoothDamping);
            timer = 0;
        }
    }
}
