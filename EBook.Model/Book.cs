using System.Collections.Generic;

namespace EBook.Model
{
  public class Book
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public string Publisher { get; set; }
    public string By { get; set; }
    public string ISBN { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    public string Language { get; set; }
    public string FileSize { get; set; }
    public string FileFormat { get; set; }
    public string DownloadLinr { get; set; }
    public List<string> TagNameList { get; set; }
  }
}
