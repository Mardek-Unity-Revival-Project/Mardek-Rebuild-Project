using System.Collections.Generic;
using UnityEngine;

namespace MURP.StatusSystem
{
    public class StatusSet : MonoBehaviour
    {
        [SerializeField] List<StatusHolder<VitalStatus, int>> vitalStatuses;
    }
}