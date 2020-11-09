namespace Assets.Scripts
{
    public class Settings
    {
        public static readonly int PlayerMaxHealth = 200;
        public static readonly int ArrowSpeed = 10;
        public static readonly float PlayerMovementSpeed = 15.0f;
        public static readonly float JumpForce = 40.0f;
        public static readonly float DistToGround = 2.1f;
        public static readonly float OwlmanSpeed = 5.0f;
        public static readonly float OwlmanChaseSpeed = 10.0f;
        public static readonly float OwlmanStartChasingVisionRange = 10.0f;

        #region Tags
        public static readonly string TagOwlmanRightEdgeDetection = "OwlmanRightEdgeDetection";
        public static readonly string TagOwlmanLeftEdgeDetection = "OwlmanLeftEdgeDetection";
        public static readonly string TagOwlmanLeftHand = "OwlmanLeftHand";
        public static readonly string TagOwlmanRightHand = "OwlmanRightHand";
        #endregion
    }
}
