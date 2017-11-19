using Windows.UI.Xaml.Controls;

namespace Mupl
{
    public sealed partial class AppShell : Page
    {
        public AppShell()
        {
            InitializeComponent();
        }

        public void SetContentFrame(Frame frame)
        {
            rootSplitView.Content = frame;
        }
    }
}