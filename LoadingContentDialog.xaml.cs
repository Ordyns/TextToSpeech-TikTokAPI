using ModernWpf.Controls;
using ModernWpf.SampleApp.ThreadedUI;
using System;
using System.Windows.Data;

namespace TikTokTTS
{
    /// <summary>
    /// Interaction logic for LoadingContentDialog.xaml
    /// </summary>
    public partial class LoadingContentDialog : ContentDialog
    {
        public LoadingContentDialog()
        {
            InitializeComponent();
        }

        public bool isBusy { get => true; }

        private void ProgressControlHost_ChildChanged(object sender, EventArgs e)
        {
            ThreadedVisualHost host = (ThreadedVisualHost)sender;

            if (host.Child is ThreadedProgressBar progressBar)
                progressBar.SetBinding(ThreadedProgressBar.IsIndeterminateProperty, new Binding(nameof(isBusy)) { Source = this });
            else if (host.Child is ThreadedProgressRing progressRing)
                progressRing.SetBinding(ThreadedProgressRing.IsActiveProperty, new Binding(nameof(isBusy)) { Source = this });
        }
    }
}
