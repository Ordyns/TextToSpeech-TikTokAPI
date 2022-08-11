namespace TikTokTTS
{
    public static class History
    {
        public static readonly HistoryViewModel ViewModel;

        static History()
        {
            ViewModel = new HistoryViewModel(HistorySaveSystem.LoadOperations());
        }
    }
}
