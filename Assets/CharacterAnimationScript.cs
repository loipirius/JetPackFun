using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationScript : MonoBehaviour
{
 public Animator animator;
 public AvatarMove characterMovement;
 public bool isGrounded;
 private void Start()
 {
  isGrounded = characterMovement.Grounded;
 }

 private void Update()
 {
  if (characterMovement.Grounded == false)
  {
   animator.SetBool("IsGrounded", false);
  }
  else
  {
   animator.SetBool("IsGrounded", true);
  }
 }
}
