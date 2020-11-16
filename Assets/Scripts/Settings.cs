﻿
namespace Assets.Scripts
{
    public class Settings
    {
        public static readonly int PlayerMaxHealth = 20;
        public static readonly float ArrowSpeed = 30.0f;
        public static readonly float ArrowTimeToLive = 0.75f;
        public static readonly float PlayerMovementSpeed = 10.0f;
        public static readonly float JumpForce = 40.0f;
        public static readonly float DistToGround = 2.1f;
        public static readonly string PlayerHorizontalSpeed = "PlayerHorizontalSpeed";

        #region Owlman
        public static readonly int OwlmanMaxHealth = 20;
        public static readonly float OwlmanSpeed = 5.0f;
        public static readonly float OwlmanChaseSpeed = 10.0f;
        public static readonly float OwlmanStartChasingVisionRange = 10.0f;
        public static readonly float OwlmanAttackRange = 1.0f;
        public static readonly float OwlmanAttackCooldownSeconds = 1.0f;
        public static readonly int OwlmanAttackDamage = 10;
        public static readonly float PatrolEdgeOffset = 1.5f;
        #endregion

        #region
        public static readonly float TimeTillMoonDustCollectionTriggerIsActive = 0.3f;
        #endregion

        #region Fade
        public static readonly float FadeInTime = 0.3f;
        #endregion

        #region Tags
        public static readonly string TagPlayer = "Player";
        public static readonly string TagOwlman = "Owlman";
        public static readonly string TagOwlmanRightEdgeDetection = "OwlmanRightEdgeDetection";
        public static readonly string TagOwlmanLeftEdgeDetection = "OwlmanLeftEdgeDetection";
        public static readonly string TagOwlmanLeftHand = "OwlmanLeftHand";
        public static readonly string TagOwlmanRightHand = "OwlmanRightHand";
        public static readonly string TagMoonDust = "MoonDust";
        #endregion

        #region Layers
        public static readonly string LayerMoonDust = "Moon Dust";
        #endregion

        #region Camera
        public static readonly float CameraHorizontalMaxOffsetPercents = 0.4f;
        public static readonly float CameraVerticalDownMaxOffsetPercents = 0.33f;
        public static readonly float CameraVerticalUpMaxOffsetPercents = 0.45f;
        public static readonly float CameraTimeToAdjust = 0.2f;
        #endregion

        public static bool isGamePaused = false;
    }
}
