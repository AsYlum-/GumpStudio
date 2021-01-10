using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Ultima
{
    public static class NativeMethods
    {
        [DllImport("User32")]
        public static extern int IsWindow(ClientWindowHandle window);

        [DllImport("User32")]
        public static extern int GetWindowThreadProcessId(ClientWindowHandle window, ref ClientProcessHandle processID);

        [DllImport("Kernel32")]
        public static extern unsafe int _lread(SafeFileHandle hFile, void* lpBuffer, int wBytes);

        [DllImport("Kernel32")]
        public static extern ClientProcessHandle OpenProcess(int desiredAccess, int inheritClientHandle,
            ClientProcessHandle processID);

        [DllImport("Kernel32")]
        public static extern int CloseHandle(ClientProcessHandle handle);

        [DllImport("Kernel32")]
        public static extern int CloseHandle(ClientWindowHandle handle);

        [DllImport("Kernel32")]
        public static extern unsafe int ReadProcessMemory(ClientProcessHandle process, int baseAddress, void* buffer,
            int size, ref int op);

        [DllImport("Kernel32")]
        public static extern unsafe int WriteProcessMemory(ClientProcessHandle process, int baseAddress, void* buffer,
            int size, int nullMe);

        [DllImport("User32")]
        public static extern int SetForegroundWindow(ClientWindowHandle hWnd);

        [DllImport("User32")]
        public static extern int SendMessage(ClientWindowHandle hWnd, int wMsg, int wParam, int lParam);

        [DllImport("User32")]
        public static extern int OemKeyScan(int wOemChar);

        [DllImport("user32")]
        public static extern ClientWindowHandle FindWindowA(string lpClassName, string lpWindowName);
    }
}