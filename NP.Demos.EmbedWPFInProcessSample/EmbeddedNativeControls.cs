using Avalonia.Controls;
using Avalonia.Platform;
using System;

namespace NP.Demos.EmbedWinFormsSample
{
    public class EmbeddedNativeControls : NativeControlHost
    {
        IntPtr _windowHandle;

        public EmbeddedNativeControls(IntPtr windowHandle)
        {
            _windowHandle = windowHandle;
        }

        protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
        {
#if WINDOWS
            return new PlatformHandle(_windowHandle, "CTRL");
#else
            return base.CreateNativeControlCore(parent);
#endif
        }

        protected override void DestroyNativeControlCore(IPlatformHandle control)
        {
            base.DestroyNativeControlCore(control);
        }
    }
}
