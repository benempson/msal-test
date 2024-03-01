This repo demonstrates the bug|error I am experiencing with MSAL (Microsoft.Identity.Client) not caching authentication tokens between application restarts on Windows. It works as expected on Android.

In order to run this code, you will need to add a local (git ignored) file called appSettings.json (as Embedded Resource) in the project root folder, with contents like this:

{
    "Settings": {
        "ClientId": ""
    }
}

Obviously you will need to provide a ClientId value taken from an Azure App Registration.
