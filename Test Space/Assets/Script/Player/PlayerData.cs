namespace PlayerDan
{
    public struct PlayerData : ICharStats
    {
        public string Name;
        public float MaxTemp;
        public float CurrentTemp;
        public float MaxBacteria;
        public float CurrentBacteria;
        public float MaxFreshness;
        public float CurrentFreshness;
        
        //Character Restrictions
        public bool CanJump;
        public bool CanRun;
    }
}