using Android.OS;

namespace EBook.Droid
{
  public class EBookServiceBinder: Binder
  {
    private EBookService _eBookService;

    public EBookServiceBinder (EBookService eBookService)
    {
      _eBookService = eBookService;
    }

    public EBookService GetEBookService ()
    {
      return _eBookService;
    }
  }
}