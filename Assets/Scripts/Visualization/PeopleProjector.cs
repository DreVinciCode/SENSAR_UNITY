using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PeopleProjector : MonoBehaviour
    {
        public Vector3 Offset;
        public GameObject PersonObject;
        public Transform _parent;

        private bool _isMessageReceived;
        private MessageTypes.People.PositionMeasurement[] _people;
        private int _totalCount;
        private float[] _coocurrence;        

        private void Update()
        {
            if (_isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.People.PositionMeasurementArray message)
        {
            _totalCount = message.people.Length;
            _people = message.people;
            _coocurrence = message.cooccurrence;
            _isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            DestroyChildren(_parent);

            for (int i = 0; i < _totalCount; i++)
            {
                var person = _people[i];
                var point = person.pos;
                var reliability = person.reliability;
                var covariance = person.covariance;
                var position = new Vector3((float)point.x, (float)point.y, (float)point.z).Ros2Unity();                                    
                //var NewPerson =  Instantiate(PersonObject, position + Offset, Quaternion.identity, _parent.transform);
                var NewPerson = Instantiate(PersonObject, _parent.transform);
                NewPerson.transform.localPosition = position;
                NewPerson.name = person.name;
            }

            _isMessageReceived = false;
        }

        private void DestroyChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}