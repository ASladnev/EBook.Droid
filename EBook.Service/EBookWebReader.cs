using System;
using System.Net;

namespace EBook.Service
{
  public class EBookWebReader
  {

    public string GetHTML (int id)
    {
      const string URL = "http://it-ebooks.info/book/";
      var url = URL + id;
      try {
        string text = new WebClient ().DownloadString (url);
        return text;
      } catch (Exception ex) {
        return "Error";
      }
    }

  }
}
