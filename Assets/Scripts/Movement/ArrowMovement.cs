using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class ArrowMovement : MonoBehaviour
    {
        private int speed = 20;

        private void Update()
        {
            gameObject.transform.position += Vector3.right * transform.localScale.x * speed * Time.deltaTime;
        }
    }
}
