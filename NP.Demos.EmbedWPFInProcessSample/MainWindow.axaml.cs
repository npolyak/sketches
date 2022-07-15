using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using static NP.Demos.EmbedWPFSample.Win32Exports;

using static NP.Demos.EmbedWPFSample.WindowLongFlags;
using static NP.Demos.EmbedWPFSample.WindowStyles;

namespace NP.Demos.EmbedWinFormsSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.Opened += MainWindow_Opened;
        }

        private async void MainWindow_Opened(object sender, System.EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo(@"Plugins\ViewPlugins\WpfApp\WpfApp.exe");
            psi.WindowStyle = ProcessWindowStyle.Normal;

            Process p = Process.Start(psi);

            await Task.Delay(2000);

            IntPtr wpfWinHandle = p.MainWindowHandle;

            SetParent(wpfWinHandle, this.PlatformImpl.Handle.Handle);

            long style = (long)GetWindowLongPtr(wpfWinHandle, (int) GWL_STYLE);

            style = (style & ~((uint)WS_POPUP | (uint) WS_CAPTION | (uint) WS_THICKFRAME | (uint)WS_MINIMIZEBOX | (uint)WS_MAXIMIZEBOX | (uint) WS_SYSMENU)) | (uint) WS_CHILD ;

            SetWindowLongPtr(new HandleRef(null, wpfWinHandle), (int) GWL_STYLE, (IntPtr) style);

            EmbeddedNativeControls embeddedNativeControls = new EmbeddedNativeControls(wpfWinHandle);

            // not needed since Avalonia takes care of resizing.
            //MoveWindow(_process.MainWindowHandle, 0, 0, this.Width, this.Height, true);

            this.Content = embeddedNativeControls;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
