
using Android.App;
using Android.Content;
using Android.OS;
using EBook.Service;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EBook.Droid
{
  [Service]
  [IntentFilter (new string[] { "com.xamarin.ebook_droid" })]
  public class EBookService : IntentService
  {
    public const string HTMLAction = "HTMLtext";

    private Intent _broadcastIntent;
    private EBookWebReader _eBookWebReader;

    public override IBinder OnBind (Intent intent)
    {
      return new EBookServiceBinder (this);
    }

    public override void OnCreate ()
    {
      base.OnCreate ();
      _broadcastIntent = new Intent (HTMLAction);
      _eBookWebReader = new EBookWebReader ();
      IdHtml = new IdHtml { Id = 0, Html = "" };
    }

    protected override void OnHandleIntent (Intent intent)
    {
      //CodeNumber = GetHTML ();
      for (var i = 1; i <= 1; i++) {
        var html = _eBookWebReader.GetHTML (i);
        while (IdHtml.isLock) {
          Thread.Sleep (50);
          Console.WriteLine ("Процесс блокирован");
        }
        IdHtml.Id = i;
        IdHtml.Html = html;
        SendOrderedBroadcast (_broadcastIntent, null);
//        Thread.Sleep (5000);
      }
      //StopSelf ();
    }

    public IdHtml IdHtml { get; set; }

    public override void OnDestroy ()
    {
      base.OnDestroy ();
      Console.WriteLine ("Сервис закрыт");
    }

  }
}