using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace My2D
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private GameObject arrow;
        [SerializeField] private Transform firePoint;

        private GameObject arrowObj;

        public void FireProjectile()
        {
            arrowObj = Instantiate(arrow, firePoint.position, Quaternion.identity);

            Vector3 originScale = arrowObj.transform.localScale;

            if(transform.localScale.x > 0)
            {
                arrowObj.transform.localScale = new Vector3(originScale.x * 1, originScale.y, originScale.z);
            }
            else
            {
                arrowObj.transform.localScale = new Vector3(originScale.x * -1, originScale.y, originScale.z);
            }

            if(arrowObj != null)
            {
                Destroy(arrowObj, 3f);
            }

        }

    }
}