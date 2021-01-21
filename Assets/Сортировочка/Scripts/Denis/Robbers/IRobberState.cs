using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRobberState : IDropStateRobber
{
    new void CathingState(FillingRobber fillingRobber);
    void FindState(FillingRobber fillingRobber);
    void ReleaseState(FillingRobber fillingRobber);
    new void DropState(FillingRobber fillingRobber);
    new void DropInInteractive(FillingRobber fillingRobber);
}
