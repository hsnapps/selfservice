namespace SelfService.Models
{
    class Plan
    {
        public Plan(string screen, string button, string to_screen = null, string to_url = null, string buttonLabel = null) {
            Screen = screen;
            Buttoon = button;
            ToScreen = to_screen;
            ToUrl = to_url;
            ButtonLabel = buttonLabel;
        }

        public string Screen { get; private set; }
        public string Buttoon { get; private set; }
        public string ToScreen { get; private set; }
        public string ToUrl { get; private set; }
        public string ButtonLabel { get; private set; }
    }
}
