using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestChecker : MonoBehaviour
{
    [SerializeField] Transform submarine;
    [SerializeField] List<Transform> pointsOfInterest;
    [SerializeField] float positionErrorAllowance = 1f;
    [SerializeField] float rotationErrorAllowance = 1f;

    public void CheckIfGoodPhoto()
    {
        Transform removePoint = null;
        foreach (var point in pointsOfInterest)
        {
            if (Vector3.Distance(submarine.position, point.position) < positionErrorAllowance)
            {
                if (Mathf.Abs(submarine.eulerAngles.y - point.eulerAngles.y) < rotationErrorAllowance)
                {
                    removePoint = point;
                    QuestManager.Instance.GoToNextQuestObjective(QuestEnum.MainQuest);
                    break;
                }
            }
        }
        if (removePoint != null)
        {
            pointsOfInterest.Remove(removePoint);
        }
    }
}
