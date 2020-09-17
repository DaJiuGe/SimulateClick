using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimulateClick
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }

        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);

        public static void MouseLefClickEvent(int dx, int dy, uint data)
        {
            SetCursorPos(dx, dy);
            mouse_event(MouseEventFlag.LeftDown, dx, dy, data, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, dx, dy, data, UIntPtr.Zero);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("User32.dll ")]
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childe, string strclass, string FrmText);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }

        const int WM_SETTEXT = 0x000C;
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_LBUTTONUP = 0x0202;
        const int WM_CLOSE = 0x0010;
        const int BM_CLICK = 0xF5;
        public MainWindow()
        {
            InitializeComponent();

            IntPtr maindHwnd = FindWindow(null, "One-Stop");
            if (maindHwnd != IntPtr.Zero)
            {
                RECT rect = new RECT();
                GetWindowRect(maindHwnd, ref rect);
                Console.WriteLine($"Left:{rect.Left},Top:{rect.Top},Right:{rect.Right},Bottom:{rect.Bottom}");
                SetCursorPos(rect.Right - 100, rect.Bottom - 100);
                mouse_event(MouseEventFlag.LeftDown, 100, 100, 0, UIntPtr.Zero);
                mouse_event(MouseEventFlag.LeftUp, 100, 100, 0, UIntPtr.Zero);
                //IntPtr childHwnd = FindWindowEx(maindHwnd, IntPtr.Zero, null, "登陆");   //获得按钮的句柄，wpf没有子窗口，空间不是原生的
                //if (childHwnd != IntPtr.Zero)
                //{
                //    SendMessage(childHwnd, WM_LBUTTONDOWN, IntPtr.Zero, null);     //发送点击按钮的消息
                //}
                //else
                //{
                //    MessageBox.Show("没有找到子窗口");
                //}
            }
            else
            {
                MessageBox.Show("没有找到窗口");
            }

            //SetCursorPos(100, 100);
            //mouse_event(MouseEventFlag.LeftDown, 100, 100, 0, UIntPtr.Zero);
            //mouse_event(MouseEventFlag.LeftUp, 100, 100, 0, UIntPtr.Zero);

        }
    }

    //https://blog.csdn.net/u012804387/article/details/22065157?utm_medium=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-1.channel_param&depth_1-utm_source=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-1.channel_param
    // Spy++
}
