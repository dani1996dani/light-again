namespace Assets.Scripts
{
    public class Settings
    {
        public static readonly int PlayerMaxHealth = 200;
        public static readonly int ArrowSpeed = 10;
        public static readonly float PlayerMovementSpeed = 15.0f;
        public static readonly float JumpForce = 40.0f;
        public static readonly float DistToGround = 2.1f;

        #region Owlman
        public static readonly float OwlmanSpeed = 5.0f;
        public static readonly float OwlmanChaseSpeed = 10.0f;
        public static readonly float OwlmanStartChasingVisionRange = 10.0f;
        public static readonly float OwlmanAttackRange = 1.0f;
        public static readonly float OwlmanAttackCooldownSeconds = 1.0f;
        public static readonly int OwlmanAttackDamage = 10;
        #endregion

        #region Tags
        public static readonly string TagPlayer = "Player";
        public static readonly string TagOwlmanRightEdgeDetection = "OwlmanRightEdgeDetection";
        public static readonly string TagOwlmanLeftEdgeDetection = "OwlmanLeftEdgeDetection";
        public static readonly string TagOwlmanLeftHand = "OwlmanLeftHand";
        public static readonly string TagOwlmanRightHand = "OwlmanRightHand";
        public static readonly string TagOwlmanAttackHitbox = "OwlmanAttackHitbox";
        #endregion
    }
}
