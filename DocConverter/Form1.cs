
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Aspose.Pdf;
using Aspose.Words;
using WordDocument = Aspose.Words.Document;
using PdfDocument = Aspose.Pdf.Document;
using Aspose.Pdf.Devices;
using System.IO;
using Aspose.Cells.Rendering;
using Aspose.Cells;
using System.Drawing.Imaging;
using System.Threading;

namespace DocConverter
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel_pager.Visible = false;
            progressBar1.Visible = false;

            //_thread = new Thread(new ThreadStart(new Convert(convert)));
        }

        private string _file_path = "";

        private string _save_path = "";
        private string _extension = "";
        private string _file_name = "";
        private int _start_page = 0;
        private int _end_page = 0;
        private Thread _thread = null;
        private bool _cancelled = false;
        private bool _convert_all = true;
        private delegate void Convert();
        private delegate void UpdateProgress(int current, int max);
        private delegate void UpdateConvertBtn(bool flag);
        private IImageConverter converter = null;

        private void btn_filepath_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "请选择需要转换的文件（PDF,Word或Excel）";
                fdlg.InitialDirectory = @"c:\";   //@是取消转义字符的意思
                fdlg.Filter = "文档文件|*.doc;*.docx;*.xls;*.xlsx;*.pdf";
                /*
                 * FilterIndex 属性用于选择了何种文件类型,缺省设置为0,系统取Filter属性设置第一项
                 * ,相当于FilterIndex 属性设置为1.如果你编了3个文件类型，当FilterIndex ＝2时是指第2个.
                 */
                fdlg.FilterIndex = 0;
                /*
                 *如果值为false，那么下一次选择文件的初始目录是上一次你选择的那个目录，
                 *不固定；如果值为true，每次打开这个对话框初始目录不随你的选择而改变，是固定的  
                 */
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    var extension = System.IO.Path.GetExtension(fdlg.FileName);

                    if (!ImageConverterFactory.Support(extension))
                    {
                        MessageBox.Show("转换器不支持该类型文件"); return;
                    }
                    _extension = extension;
                    _file_path = fdlg.FileName;
                    txt_filepath.Text = fdlg.FileName;

                    var arr = System.IO.Path.GetFileNameWithoutExtension(fdlg.FileName).Split('/').ToArray();
                    _file_name = arr[0];

                    _save_path = fdlg.FileName.Substring(0, fdlg.FileName.LastIndexOf(".")) + "\\";
                    txt_savepath.Text = _save_path;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("内部出错");
            }
        }

        private void btn_savepath_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fdlg = new FolderBrowserDialog();
            fdlg.Description = "请选择图片需要存放的文件位置";

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txt_savepath.Text = fdlg.SelectedPath;
                _save_path = fdlg.SelectedPath + "\\" + _file_name + "\\";
            }
        }
        private void convert()
        {
            converter = ImageConverterFactory.CreateImageConverter(_extension);
            converter.OnConvertFailed += converter_OnConvertFailed;
            converter.OnConvertSucceed += converter_OnConvertSucceed;
            converter.OnProgressChanged += converter_OnProgressChanged;
            converter.ConvertToImage(_file_path, _save_path, _start_page, _end_page);
        }
        private void btn_convert_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = 0;
                if (txt_filepath.Text == "")
                {
                    MessageBox.Show("请选择需要转换的文件！"); return;
                }
                if (txt_savepath.Text == "")
                {
                    MessageBox.Show("请选择需要保存的目录！"); return;
                }
                if (_convert_all)
                {
                    _start_page = 0;
                    _end_page = 0;
                }
                else
                {
                    if (_end_page < _start_page)
                    {
                        MessageBox.Show("您输入的页码数有误");
                        return;
                    }
                }

                progressBar1.Visible = true;
                btn_convert.Enabled = false;

                _thread = new Thread(new ThreadStart(new Convert(convert)));
                if (_thread.ThreadState == ThreadState.Unstarted)
                {
                    _thread.Start();
                }
                //if (_thread.ThreadState == ThreadState.Suspended)
                //{
                //    _thread.Resume();
                //}
                //this.Invoke(new Convert(convert), new object[] { });

                //if (_extension == "doc" || _extension == "docx")
                //{
                //    convertWord2Png(_file_path, _save_path, _start_page, _end_page);
                //    //(new Word2ImageConverter()).ConvertToImage(_file_path, _save_path);
                //}
                //else if (_extension == "xls" || _extension == "xlsx")
                //{
                //    convertExcel2Png(_file_path, _save_path, _start_page, _end_page);
                //}
                //else if (_extension == "pdf")
                //{
                //    convertPdf2Png(_file_path, _save_path, _start_page, _end_page);
                //}

                //MessageBox.Show("文件转换完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show("转换失败，请稍后重试！");
            }
        }

        void converter_OnProgressChanged(int a, int b)
        {
            Console.WriteLine(a);
            BeginInvoke(new UpdateProgress(updateText), new object[] { a, b });

        }

        private void updateText(int current, int max)
        {
            this.progressBar1.Value = current;
            progressBar1.Maximum = max;
        }

        void converter_OnConvertSucceed()
        {
            BeginInvoke(new UpdateConvertBtn(updateConvertBtn), new object[] { true });
            MessageBox.Show("文件转换完成");
        }

        private void updateConvertBtn(bool flag)
        {
            btn_convert.Enabled = flag;
        }

        void converter_OnConvertFailed(string msg)
        {
            BeginInvoke(new UpdateConvertBtn(updateConvertBtn), new object[] { true });
            MessageBox.Show("文件转换失败");
        }

        private void uud_endpage_ValueChanged(object sender, EventArgs e)
        {
            var uud = (NumericUpDown)sender;
            _end_page = (int)uud.Value;
        }

        private void uud_startpage_ValueChanged(object sender, EventArgs e)
        {
            var uud = (NumericUpDown)sender;
            _start_page = (int)uud.Value;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var control = (ComboBox)sender;
            if (control.SelectedItem.ToString() != "全部转换")
            {
                _convert_all = false;
                panel_pager.Visible = true;
            }
            else
            {
                _convert_all = true;
                panel_pager.Visible = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            if (_thread.ThreadState == ThreadState.Running)
            {
                DialogResult dr = MessageBox.Show("确定取消文档转换吗?", "取消转换", MessageBoxButtons.OKCancel);

                if (dr == DialogResult.OK)//如果点击“确定”按钮
                {
                    converter.Cancel();
                    btn_convert.Enabled = true;
                    progressBar1.Value = 0;
                }
            }
            else if(MessageBox.Show("确认退出吗？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                _thread = null;
                converter = null;
                e.Cancel = false;
            }
        }



    }

}
