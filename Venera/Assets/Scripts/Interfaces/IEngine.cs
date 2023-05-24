using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Venera
{
    public interface IEngine 
    {
        void InitEngine();
        void UpdateEngine(Rigidbody rb);
    }
}