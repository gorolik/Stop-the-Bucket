namespace Sources.Behaviour.UI.ChooseLevelMenu
{
    public struct LevelButtonParameters
    {
        private int _levelId;
        private int _stars;
        private bool _opened;

        public LevelButtonParameters(int levelId, int stars, bool opened)
        {
            _levelId = levelId;
            _stars = stars;
            _opened = opened;
        }

        public int LevelId => _levelId;
        public int Stars => _stars;
        public bool Opened => _opened;
    }
}