using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovingState : StateMachineBehaviour
{
    public static event UnityAction<float> returnSpeed;
}
