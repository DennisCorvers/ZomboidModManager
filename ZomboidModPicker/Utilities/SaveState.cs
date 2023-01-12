namespace ZomboidModPicker.Utilities
{
    public struct SaveState
    {
        public bool Success { get; }

        public string FileName { get; }

        private SaveState(bool success, string name)
        {
            Success = success;
            FileName = name;
        }

        public static SaveState FromSuccess(string name)
            => new(true, name);

        public static SaveState FromFailure()
            => new(false, "No data");
    }
}
