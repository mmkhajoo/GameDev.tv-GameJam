using System;
using DefaultNamespace;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
   [SerializeField] private Animator animator;
   
   private static readonly int Jump = Animator.StringToHash("Jump");
   private static readonly int Walk = Animator.StringToHash("Walk");
   private static readonly int Idle = Animator.StringToHash("Idle");
   private static readonly int Land = Animator.StringToHash("Land");
   
   public void OnPlayerStateChange(PlayerStateType state)
   {
      Debug.Log(state);
      switch (state)
      {
         case PlayerStateType.None:
            DoIdle();
            break;
         case PlayerStateType.Idle:
            DoIdle();
            break;
         case PlayerStateType.Walking:
            DoWalk();
            break;
         case PlayerStateType.Die:
            DoDie();
            break;
         default:
            DoIdle();
            break;
      }
   }

   public void OnPlayerJump()
   {
      Debug.Log("Jump");
      DoJump();
   }
   
   public void OnPlayerLand()
   {
      Debug.Log("Land");
      DoLand();
   }

   private void DoWalk()
   {
      animator.SetTrigger(Walk);
   }
   private void DoJump()
   {
      animator.SetTrigger(Jump);
   }
   private void DoIdle()
   {
      animator.SetTrigger(Idle);
   }
   private void DoLand()
   {
      animator.SetTrigger(Land);
   }
   
   private void DoDie()
   {
      
   }
}
