using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool LockWorkStation();

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool LogonUser(
        string lpszUsername,
        string lpszDomain,
        string lpszPassword,
        int dwLogonType,
        int dwLogonProvider,
        out IntPtr phToken);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);

    private const int LOGON32_LOGON_INTERACTIVE = 2;
    private const int LOGON32_PROVIDER_DEFAULT = 0;

    static void Main()
    {
        LockWorkStation();

        System.Threading.Thread.Sleep(1000);

        string username = "OtherUsername"; // hedef kullanıcı adı
        string domain = "DomainName"; // domain ismi de gerekiyormuş sistem verilerinden alınabilir eğer yoksa burası 'local' olarak ayarlanacak
        string password = "UserPassword"; // hedef kullanıcı şifresi

        IntPtr userToken;
        bool success = LogonUser(username, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out userToken);
    }
}
