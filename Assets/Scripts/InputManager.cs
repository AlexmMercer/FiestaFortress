using UnityEngine;

public class InputManager : MonoBehaviour
{
    public void Init()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector3 targetPosition = GetTargetPosition();
            GameManager.Instance.MissileLauncher.LaunchMissile(targetPosition);
        }
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition;
        if (Input.touchCount > 0)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        targetPosition.x = -8;
        return targetPosition;
    }
}
