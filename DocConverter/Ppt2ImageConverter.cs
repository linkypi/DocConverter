using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocConverter
{
    class Ppt2ImageConverter : IImageConverter
    {
        private Pdf2ImageConverter pdf2ImageConverter;
        public event CbGeneric<int, int> ProgressChanged;
        public event CbGeneric ConvertSucceed;
        public event CbGeneric<string> ConvertFailed;

        public void Cancel()
        {
            if (this.pdf2ImageConverter != null)
            {
                this.pdf2ImageConverter.Cancel();
            }
        }

        public void ConvertToImage(string originFilePath, string imageOutputDirPath)
        {
            ConvertToImage(originFilePath, imageOutputDirPath, 0, 0, 200);
        }

        /**/
        /// <summary>
        /// 将pdf文档转换为图片的方法      
        /// </summary>
        /// <param name="originFilePath">ppt文件路径</param>
        /// <param name="imageOutputDirPath">图片输出路径，如果为空，默认值为pdf所在路径</param>       
        /// <param name="startPageNum">从PDF文档的第几页开始转换，如果为0，默认值为1</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换，如果为0，默认值为pdf总页数</param>       
        /// <param name="resolution">设置图片的像素，数字越大越清晰，如果为0，默认值为128，建议最大值不要超过1024</param>
        private void ConvertToImage(string originFilePath, string imageOutputDirPath, int startPageNum, int endPageNum, int resolution)
        {
            try
            {
                Aspose.Slides.Presentation doc = new Aspose.Slides.Presentation(originFilePath);

                if (doc == null)
                {
                    throw new Exception("ppt文件无效或者ppt文件被加密！");
                }

                if (imageOutputDirPath.Trim().Length == 0)
                {
                    imageOutputDirPath = Path.GetDirectoryName(originFilePath);
                }

                if (!Directory.Exists(imageOutputDirPath))
                {
                    Directory.CreateDirectory(imageOutputDirPath);
                }

                if (startPageNum <= 0)
                {
                    startPageNum = 1;
                }

                if (endPageNum > doc.Slides.Count || endPageNum <= 0)
                {
                    endPageNum = doc.Slides.Count;
                }

                if (startPageNum > endPageNum)
                {
                    int tempPageNum = startPageNum; startPageNum = endPageNum; endPageNum = startPageNum;
                }

                if (resolution <= 0)
                {
                    resolution = 128;
                }

                //先将ppt转换为pdf临时文件
                string tmpPdfPath = originFilePath + ".pdf";
                doc.Save(tmpPdfPath, Aspose.Slides.Export.SaveFormat.Pdf);

                //再将pdf转换为图片
                Pdf2ImageConverter converter = new Pdf2ImageConverter();
                converter.ConvertFailed += new CbGeneric<string>(converter_ConvertFailed);
                converter.ConvertSucceed += new CbGeneric(converter_ConvertSucceed);
                converter.ProgressChanged += new CbGeneric<int, int>(converter_ProgressChanged);
                converter.ConvertToImage(tmpPdfPath, imageOutputDirPath);

                //删除pdf临时文件
                File.Delete(tmpPdfPath);

                if (this.ConvertSucceed != null)
                {
                    this.ConvertSucceed();
                }
            }
            catch (Exception ex)
            {
                if (this.ConvertFailed != null)
                {
                    this.ConvertFailed(ex.Message);
                }
            }

            this.pdf2ImageConverter = null;
        }

        void converter_ProgressChanged(int done, int total)
        {
            if (this.ProgressChanged != null)
            {
                this.ProgressChanged(done, total);
            }
        }

        void converter_ConvertSucceed()
        {
            if (this.ConvertSucceed != null)
            {
                this.ConvertSucceed();
            }
        }

        void converter_ConvertFailed(string msg)
        {
            if (this.ConvertFailed != null)
            {
                this.ConvertFailed(msg);
            }
        }
    }
}
