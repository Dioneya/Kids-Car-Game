using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropStateRobber
{
     void DropInInteractive(FillingRobber fillingRobber);
     void DropState(FillingRobber fillingRobber);
     void DropAllRobber(FillingRobber fillingRobber);
     void CathingState(FillingRobber fillingRobber);
}

