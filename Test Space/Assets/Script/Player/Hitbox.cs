using System;
using System.Collections.Generic;
using Movement;
using Movement.Pushbacks;
using PlayerDan;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class Hitbox : MonoBehaviour
    {
        private ICharacter<PlayerData> _character;
        private List<IEffector<PlayerData>> _effectors;

        private void Awake()
        {
            _effectors = new List<IEffector<PlayerData>>();
            _character = GetComponentInParent<ICharacter<PlayerData>>();
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
                var effector = other.GetComponent<IEffector<PlayerData>>();
                if (effector != null && !_effectors.Contains(effector))
                    _effectors.Add(effector);
            }

            if (other.CompareTag($"DeathArea"))
            {
                SceneManager.LoadScene(0);
            }

            if (other.CompareTag($"PushBack"))
                ScriptableObject.CreateInstance<KnockBack>().TakeEffect(_character);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag($"EnvironmentEffector"))
            {
                var effector = other.GetComponent<IEffector<PlayerData>>();
                if (effector != null && _effectors.Contains(effector))
                    _effectors.Remove(effector);
            }
        }
    }
}