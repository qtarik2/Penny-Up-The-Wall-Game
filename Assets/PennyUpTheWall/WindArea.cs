using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace MySpace
{

    public class WindArea : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 windDir;
        public Transform directionObject;
        private float windForce;
        private float angle;
        public int Every = 5;
        [Header("UI Element")]
        public Transform directionImage;
        public Text forceTxt;
        public bool isStop = true;
        private void Start()
        {
            StartCoroutine(SetWindDirection());
        }

        public IEnumerator SetWindDirection()
        {
            while (isStop)
            {
                windForce = Random.Range(1f, 3);
                forceTxt.text = windForce.ToString("0.0");
                var v = Random.Range(0, 360);
                directionObject.eulerAngles = Vector3.up * v;
                directionImage.eulerAngles = Vector3.forward * (-v + 90);
                yield return new WaitForSeconds(Every);
            }
        }
        private void Update()
        {
            angle = directionObject.eulerAngles.y;
            windDir = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                other.GetComponent<Rigidbody>().velocity += (windDir / 4);
            }
        }
    }
}
