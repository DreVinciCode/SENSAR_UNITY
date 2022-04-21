using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class ExtrapolationManager : MonoBehaviour
    {
        public GameObject ExtrapolationTarget;
        public PoseCovarianceStampedProcessor message;

        private DefaultObserverEventHandler _targetCubeEventHandler;

        private Vector3 _defaultPos;
        private Quaternion _defaultOri;
        private Vector3 _lastTrackedWorldPos;
        private Quaternion _lastTrackedWorldOri;
        private Vector3 _latestWorldPos;
        private Quaternion _latestWorldOri;
        private Vector3 _lastAddedWorldPos;

        private void Start()
        {
            _targetCubeEventHandler = GetComponent<DefaultObserverEventHandler>();
            _defaultPos = ExtrapolationTarget.transform.localPosition;
            _defaultOri = ExtrapolationTarget.transform.localRotation;
        }

        private void Update()
        {
            if (_targetCubeEventHandler.StatusFilter == DefaultObserverEventHandler.TrackingStatusFilter.Tracked && _targetCubeEventHandler.StatusFilter != DefaultObserverEventHandler.TrackingStatusFilter.Tracked_ExtendedTracked)
            {
                ExtrapolationTarget.transform.localPosition = _defaultPos;
                ExtrapolationTarget.transform.localRotation = _defaultOri;

                _lastTrackedWorldPos = _latestWorldPos;
                _lastTrackedWorldOri = _latestWorldOri;

                _lastAddedWorldPos = _lastTrackedWorldPos;
            }

            if (_targetCubeEventHandler.StatusFilter == DefaultObserverEventHandler.TrackingStatusFilter.Tracked_ExtendedTracked)
            {
                ExtrapolationTarget.transform.localRotation = Quaternion.Euler(_defaultOri.eulerAngles + (_lastTrackedWorldOri.eulerAngles - _latestWorldOri.eulerAngles));

                var distance_travelled = (_lastAddedWorldPos - _latestWorldPos).magnitude;
                Vector3 local_forward = ExtrapolationTarget.transform.InverseTransformVector(ExtrapolationTarget.transform.forward);
                ExtrapolationTarget.transform.Translate(distance_travelled * local_forward);

                _lastAddedWorldPos = _latestWorldPos;
            }
        }


        private void updateLatest()
        {
            _latestWorldPos = message._position;
            _latestWorldOri = message._rotation;
        }
    }
}
