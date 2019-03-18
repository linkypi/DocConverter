using Aspose.Words.Saving;
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
    public class Word2ImageConverter : IImageConverter
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
        private void _ConvertToImage(string wordInputPath, string imageOutputDirPath, int startPageNum, int endPageNum, int resolution = 300)
        {
            try
            {
                Aspose.Words.Document doc = new Aspose.Words.Document(wordInputPath);

                if (doc == null)
                {
                    throw new Exception("Word文件无效或者Word文件已被加密！");
                }
                //if (imageOutputDirPath.Trim().Length == 0)
                //{
                //    imageOutputDirPath = Path.GetDirectoryName(wordInputPath);
                //}
                if (!Directory.Exists(imageOutputDirPath))
                {
                    Directory.CreateDirectory(imageOutputDirPath);
                }
                if (startPageNum <= 0)
                {
                    startPageNum = 1;
                }
                if (endPageNum > doc.PageCount || endPageNum <= 0)
                {
                    endPageNum = doc.PageCount;
                }
                if (startPageNum > endPageNum)
                {
                    int tempPageNum = startPageNum; startPageNum = endPageNum; endPageNum = startPageNum;
                }

                if (resolution <= 0)
                {
                    resolution = 300;
                }

                ImageSaveOptions saveOptions = new ImageSaveOptions(Aspose.Words.SaveFormat.Png);
                saveOptions.Resolution = resolution;
                saveOptions.PageCount = endPageNum - startPageNum + 1;
         
                for (int index = startPageNum; index <= endPageNum; index++)
                {
                    if (this._cancelled)
                    {
                        break;
                    }
                    saveOptions.PageIndex = index - 1;
                    string imgPath = Path.Combine(imageOutputDirPath, index.ToString("000") + ".png");
                    doc.Save(imgPath, saveOptions); //

                    System.Threading.Thread.Sleep(200);
                    if (this.OnProgressChanged != null && !_cancelled)
                    {
                        this.OnProgressChanged(index, endPageNum);
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
