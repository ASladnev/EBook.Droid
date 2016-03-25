using System;
using System.Collections.Generic;

namespace EBook.Model
{
  public class Tag
  {
    public string TagName { get; set; }
    public List<Book> BookList { get; set; }  
    public Tag SelectorTag { get; set; }
    public List<Tag> SubListTagCash { get; set; }

    public List<Tag> GetSubTagList ()
    {
      throw new NotImplementedException ();
    }

  }
}
