using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Threading;

namespace EBook.Droid
{
  [Activity (Label = "EBook Droid", MainLauncher = true, Icon = "@drawable/icon")]
  public class TagActivity : Activity
  {
    public EBookServiceBinder Binder { get; set; }
    public bool IsBound { get; set; }

    private EBookServiceConnection _connection;
    private EBookReceiver _receiver;
    private Intent _serviceIntent;
    private TextView _textViewResult;

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);
      SetContentView (Resource.Layout.Main);

      _serviceIntent = new Intent ("com.xamarin.ebook_droid");
      _receiver = new EBookReceiver ();

      _textViewResult = FindViewById<TextView> (Resource.Id.textViewResult);
      _textViewResult.Text = "";

      var buttonUI = FindViewById<Button> (Resource.Id.buttonUI);
      buttonUI.Click += (s, e) => {
        RunOnUiThread (() => {
//          Toast.MakeText (this, $"Сообщение кол-во {Binder.GetEBookService ().CodeNumber}", ToastLength.Short).Show ();
        });
      };


      _connection = new EBookServiceConnection (this);
      BindService (_serviceIntent, _connection, Bind.AutoCreate);

      var intentFilter = new IntentFilter (EBookService.HTMLAction) { Priority = (int)IntentFilterPriority.HighPriority };
      RegisterReceiver (_receiver, intentFilter);

      ShceduleEBookUpdate ();

    }

    protected override void OnStart ()
    {
      base.OnStart ();
    }

    private void ShceduleEBookUpdate ()
    {
      if (!IsAlarmSet ()) {
        var alarm = (AlarmManager) GetSystemService (AlarmService);
        var pendingServiceIntent = PendingIntent.GetService (this, 0, _serviceIntent, PendingIntentFlags.CancelCurrent);
        alarm.Set (AlarmType.Rtc, 0, pendingServiceIntent);
        //alarm.SetRepeating (AlarmType.Rtc, 0, 2000, pendingServiceIntent);
      } else {
        Console.WriteLine ("alarm already set");
      }
    }

    private bool IsAlarmSet ()
    {
      return PendingIntent.GetBroadcast (this, 0, _serviceIntent, PendingIntentFlags.NoCreate) != null;
    }

    protected override void OnDestroy ()
    {
      base.OnStop ();

      UnregisterReceiver (_receiver);
      if (!IsBound) return;  
      UnbindService (_connection);
      IsBound = false;
    }


    public void GetEBook ()
    {
      if (!IsBound) return;
      RunOnUiThread (()=> {
        //var html = 
        var service = Binder.GetEBookService ();
        var IdHtml = service.IdHtml;
        while (IdHtml.isLock) Thread.Sleep (50);
        Console.WriteLine ($"№ фала {service.IdHtml.Id}");
        IdHtml.isLock = true;
        var html = string.Copy (IdHtml.Html);
        IdHtml.isLock = false;
      });
    }

  }
}

