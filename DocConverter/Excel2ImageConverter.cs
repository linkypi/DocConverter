using Aspose.Cells;
using Aspose.Cells.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocConverter
{
    public class Excel2ImageConverter : IImageConverter
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

        public void ConvertToImage(string filepath, string output,int startpage ,int endpage)
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
                var workBook = new Workbook(filepath);

                if (workBook == null)
                {
                    throw new Exception("Excel文件无效或者Excel文件已被加密！");
                }
                var iop = new ImageOrPrintOptions();
                iop.ImageFormat = ImageFormat.Png;
                //iop.AllColumnsInOnePagePerSheet = true;
                iop.ChartImageType = ImageFormat.Png;
                iop.OnePagePerSheet = true;
                
                iop.HorizontalResolution = 400;
                iop.VerticalResolution = 400;

                if (!Directory.Exists(outpath))
                {
                    Directory.CreateDirectory(outpath);
                }

                if (startPage <= 0)
                {
                    startPage = 0;
                }
                if (endPage > workBook.Worksheets.Count || endPage <= 0)
                {
                    endPage = workBook.Worksheets.Count;
                }

                if (resolution <= 0)
                {
                    resolution = 300;
                }
             
                for (int index = startPage; index < endPage; index++) 
                {
                    if (this._cancelled)
                    {
                        break;
                    }
                    Worksheet item = workBook.Worksheets[index];
                    SheetRender sr = new SheetRender(item, iop);

                    for (int kindex = 0; kindex < sr.PageCount; kindex++)
                    {
                        sr.ToImage(kindex, outpath + item.Name + ".png");
                        System.Threading.Thread.Sleep(200);
                    }
                    if (!_cancelled && this.OnProgressChanged != null)
                    {
                        this.OnProgressChanged(index + 1, endPage);
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
