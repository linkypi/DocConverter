using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocConverter
{
    public delegate void Success();
    public delegate void ProgressChanged(int a, int b);
    public delegate void Faild(string msg);  

    public interface IImageConverter
    {
        
        event ProgressChanged OnProgressChanged;
        event Success OnConvertSucceed;
        event Faild OnConvertFailed;

         void Cancel();

         void ConvertToImage(string filepath, string output, int startpage, int endpage);
    }
}
