using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocConverter
{
    public class ImageConverterFactory
    {
        public static IImageConverter CreateImageConverter(string extendName)
        {
            if (extendName == ".xls" || extendName == ".xlsx")
            {
                return new Excel2ImageConverter();
            }
            if (extendName == ".doc" || extendName == ".docx")
            {
                return new Word2ImageConverter();
            }

            if (extendName == ".pdf")
            {
                return new Pdf2ImageConverter();
            }

            //if (extendName == ".ppt" || extendName == ".pptx")
            //{
            //    return new Ppt2ImageConverter();
            //}

            //if (extendName == ".rar")
            //{
            //    return new Rar2ImageConverter();
            //}

            return null;
        }

        public static bool Support(string extendName)
        {
            return extendName == ".doc" || extendName == ".docx" || extendName == ".pdf" || extendName == ".xls" || extendName == ".xlsx";
        }
    } 
}
