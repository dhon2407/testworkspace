using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target = null;


        private void Update()
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(0);
        }
    }
}