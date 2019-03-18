
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocConverter
{
    public class Pdf2ImageConverter : IImageConverter
    {
        private bool _cancelled = false;

        public void Cancel()
        {
            if (this._cancelled)
            {
                return;
            }

            this._cancelled = true;
        }

        public void ConvertToImage(string filepath, string output, int startpage, int endpage)
        {
            this._cancelled = false;
            _ConvertToImage(filepath, output, startpage, endpage);
        }

        /**/
        /// <summary>
        /// 将Word文档转换为图片的方法      
        /// </summary>
        /// <param name="wordInputPath">Word文件路径</param>
        /// <param name="imageOutputDirPath">图片输出路径，如果为空，默认值为Word所在路径</param>      
        /// <param name="startPageNum">从PDF文档的第几页开始转换，如果为0，默认值为1</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换，如果为0，默认值为Word总页数</param>
        /// <param name="resolution">设置图片的像素，数字越大越清晰，如果为0，默认值为128，建议最大值不要超过1024</param>
        private void _ConvertToImage(string filepath, string outpath, int startPage, int endPage, int resolution = 300)
        {
            try
            {
                Document pdfDocument = new Document(filepath);
                if (pdfDocument == null)
                {
                    throw new Exception("PDF文件无效或者PDF文件已被加密！");
                }
                if (startPage <= 0)
                {
                    startPage = 1;
                }
                if (endPage > pdfDocument.Pages.Count || endPage <= 0)
                {
                    endPage = pdfDocument.Pages.Count;
                }
                if (!Directory.Exists(outpath))
                {
                    Directory.CreateDirectory(outpath);
                }

                for (int page = startPage; page <= pdfDocument.Pages.Count; page++)
                {

                    if (page > endPage)
                    {
                        break;
                    }
                    if (this._cancelled)
                    {
                        break;
                    }
                    using (FileStream imageStream = new FileStream(outpath + page.ToString("000") + ".png", FileMode.Create))
                    {
                        // Create PNG device with specified attributes
                        // Width, Height, Resolution, Quality
                        // Quality [0-100], 100 is Maximum
                        // Create Resolution object
                        Resolution res = new Resolution(300);
                        PngDevice pngDevice = new PngDevice(res);

                        // Convert a particular page and save the image to stream
                        pngDevice.Process(pdfDocument.Pages[page], imageStream);

                        // Close stream
                        imageStream.Close();
                    }

                    System.Threading.Thread.Sleep(200);
                    if (this.OnProgressChanged != null && !_cancelled)
                    {
                        this.OnProgressChanged(page, endPage);
                    }
                }

                if (this._cancelled)
                {
                    return;
                }

                if (this.OnConvertSucceed != null)
                {
                    this.OnConvertSucceed();
                }
              
            }
            catch (Exception ex)
            {
                if (this.OnConvertFailed != null)
                {
                    this.OnConvertFailed(ex.Message);
                }
            }
        }

        public event ProgressChanged OnProgressChanged;

        public event Success OnConvertSucceed;

        public event Faild OnConvertFailed;
    } 
}
