using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    public class ToSteal : GOAction
    {

        [SerializeField] public GameObject target;

        
        public float closeDistance;

        /// <summary>
        /// Checks whether a target is close depending on a given distance,
        /// calculates the magnitude between the gameobject and the target and then compares with the given distance.
        /// </summary>
        /// <returns>True if the magnitude between the gameobject and de target is lower that the given distance.</returns>
        
        public void Start()
        {
            target = GameObject.Find("Treasure");
        }
        public void Steal()
        {
            
        }
    }
}