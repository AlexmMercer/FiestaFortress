using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab; // ?????? ??????
    [SerializeField] private List<Transform> launchPoints; // ?????? ????? ??????? ??? ???????? ?????????
    [SerializeField] private float missileSpeed = 10f; // ???????? ??????
    [SerializeField] private float reloadTime = 5f; // ????? ??????????? ???????? ?????????

    private List<GameObject> missiles = new List<GameObject>(); // ?????? ?????????? ?????
    private List<bool> launchPointsStatus = new List<bool>(); // ????????? ???????? ????????? (?????????/???????)

    private void Start()
    {
        InitializeLaunchPoints();
    }

    public void Init()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector3 targetPosition = GetTargetPosition();
            LaunchMissile(targetPosition);
        }
    }

    private void InitializeLaunchPoints()
    {
        foreach (Transform launchPoint in launchPoints)
        {
            GameObject missile = Instantiate(missilePrefab, launchPoint.position, missilePrefab.transform.rotation);
            //missiles.Add(missile);
            missile.transform.SetParent(launchPoint);
            launchPointsStatus.Add(true);
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

    public void LaunchMissile(Vector3 targetPosition)
    {
        int freeIndex = GetFreeLaunchPointIndex();

        if (freeIndex != -1)
        {
            Transform selectedLaunchPoint = launchPoints[freeIndex];

            if (selectedLaunchPoint.childCount > 0)
            {
                GameObject missile = selectedLaunchPoint.GetChild(0).gameObject; // ???????? ?????? ???????? ??????
                missile.transform.SetParent(null);

                IMissile missileScript = missile.GetComponent<Missile>();
                if (missileScript != null)
                {
                    switch(freeIndex)
                    {
                        case 0:
                            if(targetPosition.z <= - 1.208f)
                            {
                                missileScript.SetTarget(targetPosition, missileSpeed, 1);
                            } else
                            {
                                missileScript.SetTarget(targetPosition, missileSpeed, -1);
                            }
                            break;
                        case 1:
                            if (targetPosition.z <= 0.52f)
                            {
                                missileScript.SetTarget(targetPosition, missileSpeed, 1);
                            }
                            else
                            {
                                missileScript.SetTarget(targetPosition, missileSpeed, -1);
                            }
                            break;
                        case 2:
                            if (targetPosition.z <= 2.496f)
                            {
                                missileScript.SetTarget(targetPosition, missileSpeed, 1);
                            }
                            else
                            {
                                missileScript.SetTarget(targetPosition, missileSpeed, -1);
                            }
                            break;

                    }
                    //missileScript.SetTarget(targetPosition, missileSpeed); // ????????????? ???? ? ????????

                    launchPointsStatus[freeIndex] = false;
                    StartCoroutine(ReloadLaunchPoint(freeIndex));
                }
                else
                {
                    Debug.LogError("Missile does not implement IMissile interface.");
                }
            }
            else
            {
                Debug.LogError("No child missile found at launch point index " + freeIndex);
            }
        }
        else
        {
            Debug.LogError("No free launch points available.");
        }
    }



    private int GetFreeLaunchPointIndex()
    {
        List<int> freeIndices = new List<int>();

        for (int i = 0; i < launchPointsStatus.Count; i++)
        {
            if (launchPointsStatus[i])
            {
                freeIndices.Add(i);
            }
        }

        if (freeIndices.Count > 0)
        {
            int randomIndex = Random.Range(0, freeIndices.Count);
            return freeIndices[randomIndex];
        }

        return -1;
    }


    private IEnumerator ReloadLaunchPoint(int index)
    {
        yield return new WaitForSeconds(reloadTime);
        launchPointsStatus[index] = true;
        GameObject newMissile = Instantiate(missilePrefab, launchPoints[index].position, missilePrefab.transform.rotation);
        newMissile.transform.SetParent(launchPoints[index]);
    }
}
