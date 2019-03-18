using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocConverter
{
    /// <summary>
    /// 将图片压缩包解压。（如果课件本身就是多张图片，那么可以将它们压缩成一个rar，作为一个课件）
    /// </summary>
    class Rar2ImageConverter : IImageConverter
    {
        private bool cancelled = false;
        public event CbGeneric<string> ConvertFailed;
        public event CbGeneric<int, int> ProgressChanged;
        public event CbGeneric ConvertSucceed;

        public void Cancel()
        {
            this.cancelled = true;
        }


        public void ConvertToImage(string rarPath, string imageOutputDirPath)
        {
            try
            {
                Unrar tmp = new Unrar(rarPath);
                tmp.Open(Unrar.OpenMode.List);
                string[] files = tmp.ListFiles();
                tmp.Close();

                int total = files.Length;
                int done = 0;

                Unrar unrar = new Unrar(rarPath);
                unrar.Open(Unrar.OpenMode.Extract);
                unrar.DestinationPath = imageOutputDirPath;

                while (unrar.ReadHeader() && !cancelled)
                {
                    if (unrar.CurrentFile.IsDirectory)
                    {
                        unrar.Skip();
                    }
                    else
                    {
                        unrar.Extract();
                        ++done;

                        if (this.ProgressChanged != null)
                        {
                            this.ProgressChanged(done, total);
                        }
                    }
                }
                unrar.Close();

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
        }


    } 
}
