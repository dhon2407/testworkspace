using System;
using System.Collections.Generic;
using Movement;
using PlayerDan;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class Hitbox : MonoBehaviour
    {
        private ICharacter _character;
        private List<IEffector> _effectors;

        private void Awake()
        {
            _effectors = new List<IEffector>();
            _character = GetComponentInParent<ICharacter>();
        }

        private void Update()
        {
            UpdateEffectors();
        }

        private void UpdateEffectors()
        {
            if (_effectors.Count > 0)
            {
                foreach (var effector in _effectors)
                    effector.TakeEffect(_character);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag($"EnvironmentEffector"))
            {
                var effector = other.GetComponent<IEffector>();
                if (effector != null && !_effectors.Contains(effector))
                    _effectors.Add(effector);
            }

            if (other.CompareTag($"DeathArea"))
            {
                SceneManager.LoadScene(0);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag($"EnvironmentEffector"))
            {
                var effector = other.GetComponent<IEffector>();
                if (effector != null && _effectors.Contains(effector))
                    _effectors.Remove(effector);
            }
        }
    }
}