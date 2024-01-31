using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWinformApp.Internal
{
    internal static class Utils
    {
        public static Task RunOnSTA(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

        public static Task RunOnSTA<T>(Action<T> action, T param)
        {
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    action(param);
                    tcs.SetResult(null);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

        public static Task<T> RunOnSTA<T>(Func<T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            var thread = new Thread(() =>
            {
                try
                {
                    tcs.SetResult(func());
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

        public static Task<T2> RunOnSTA<T1, T2>(Func<T1, T2> func, T1 param)
        {
            var tcs = new TaskCompletionSource<T2>();
            var thread = new Thread(() =>
            {
                try
                {
                    tcs.SetResult(func(param));
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

        public static string GetClipboardText()
        {
            return GetClipboardTextAsync().Result;
        }

        public static async Task<string> GetClipboardTextAsync()
        {
            try
            {
                var tcs = new TaskCompletionSource<string>();
                var thread = new Thread(() =>
                {
                    try
                    {
                        tcs.SetResult(Clipboard.GetText(TextDataFormat.Text));
                    }
                    catch (Exception tex)
                    {
                        tcs.SetException(tex);
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                return await tcs.Task;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
