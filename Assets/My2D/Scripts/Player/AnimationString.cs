using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    // �ִϸ��̼� �Ķ���� �̸� �����ϴ� Ŭ����
    public class AnimationString
    {
        [Header("Player")]
        public static string IsWalk = "isWalk";
        public static string IsRun = "isRun";
        public static string IsGround = "isGround";
        public static string IsWall = "isWall";
        public static string IsCeiling = "isCeiling";
        public static string JumpTrigger = "JumpTrigger";
        public static string AttackTrigger = "AttackTrigger";
        public static string YVelocity = "YVelocity";
        public static string CanMove = "CanMove";

        [Header("Enemy")]
        public static string HasTarget = "HasTarget";

    }
}