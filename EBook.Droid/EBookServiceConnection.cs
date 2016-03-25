using Android.Content;
using Android.OS;

namespace EBook.Droid
{
  public class EBookServiceConnection : Java.Lang.Object, IServiceConnection
  {
    private TagActivity _tagActivity;

    public EBookServiceConnection (TagActivity tagActivity)
    {
      _tagActivity = tagActivity;
    }

    public void OnServiceConnected (ComponentName name, IBinder service)
    {
      var eBookServiceBinder = service as EBookServiceBinder;
      if (eBookServiceBinder == null) return;
      var binder = (EBookServiceBinder) service;
      _tagActivity.Binder = binder;
      _tagActivity.IsBound = true;
    }

    public void OnServiceDisconnected (ComponentName name)
    {
      _tagActivity.IsBound = false;
    }
  }
}