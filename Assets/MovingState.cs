using UnityEngine;
using System.Collections;

public class MovingState : MonoBehaviour {

    void Moving() { }
    void Update() { }
}

public class MovingUpState : MovingState
{
    void Moving()
    {
        
    }

    void Update()
    {
        
    }
}

public class MovingSlerpState : MovingState
{ }


public class MovingDownState : MovingState
{ }