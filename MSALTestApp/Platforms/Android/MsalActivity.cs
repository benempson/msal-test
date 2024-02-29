using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace MSALTestApp.Platforms.Android;

[Activity(Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
    DataHost = "auth",
    DataScheme = "msaled4ec0db-38a5-47c3-8e5a-d7f821a71b09")]
public class MsalActivity : BrowserTabActivity
{
}