using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace SCC
{
    public class ClipboardSender
    {
        private readonly IDictionary<string, string> charDictionary = new Dictionary<string, string>();

        public ClipboardSender()
        {
            charDictionary.Add("{", "$1$");
            charDictionary.Add("}", "$2$");
            charDictionary.Add("$1$", "{{}");
            charDictionary.Add("$2$", "{}}");
            charDictionary.Add("(", "{(}");
            charDictionary.Add("%", "{%}");
            charDictionary.Add("+", "{+}");
            charDictionary.Add("^", "{^}");
            charDictionary.Add("~", "{~}");
            charDictionary.Add(")", "{)}");
            charDictionary.Add("[", "{[}");
            charDictionary.Add("]", "{]}");
            charDictionary.Add("\t", "{TAB}");
            charDictionary.Add("\r", string.Empty);
            charDictionary.Add("\n", "{ENTER}");
        }
        
        public bool TrySendClipboardText()
        {
            try {
                if (!Clipboard.ContainsText())
                    return false;

                var buffer = new StringBuilder(Clipboard.GetText());

                foreach (var pair in charDictionary)
                    buffer.Replace(pair.Key, pair.Value);

                SimpleSendKeys(buffer.ToString());
                return true;
            }
            catch(Exception exception) {
                System.Diagnostics.Debug.WriteLine(exception);
            }

            return false;
        }

        private static void SimpleSendKeys(string input)
        {
            SetFocusOnPreviousWindow();
            SendKeys.SendWait(input);
        }

        private static void SetFocusOnPreviousWindow()
        {
            var targetHwnd = NativeMethods.GetWindow(Process.GetCurrentProcess().MainWindowHandle, (uint)NativeMethods.GetWindow_Cmd.GW_HWNDNEXT);

            while (true)
            {
                var temp = NativeMethods.GetParent(targetHwnd);
                if (temp.Equals(IntPtr.Zero))
                    break;
                targetHwnd = temp;
            }

            NativeMethods.SetForegroundWindow(targetHwnd);
        }
    }
}
