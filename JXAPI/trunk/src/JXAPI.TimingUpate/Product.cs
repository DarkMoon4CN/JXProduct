using JXAPI.Component.BLL;
using JXAPI.Component.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace JXAPI.TimingUpate
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }


        private void Product_Load(object sender, EventArgs e)
        {
            NotifyInit();
            // 异步执行
            Thread mythread = new Thread(ProductUpdateInit);
            mythread.Start();
        }
        
        public void ProductUpdateInit()
        {
            ProductMySqlBLL prdouctMysqlBLL = ProductMySqlBLL.Instance;
            lblTableName.Text = "Product";
            lblAction.Text = "等待状态";
            lblProduct.Text = "等待数据";
            lblUpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ProductBLL productBLL = ProductBLL.Instance;
            int pageCount = 0;
            int pageSize=1000;
            //向mysql 获取最大的商品Id
            int  insertMaxId= prdouctMysqlBLL.GetInsetMaxPId();
            //返回sql server中product数据总数
            int rowCount=productBLL.Product_Count();
            if (insertMaxId == -1 || rowCount == -1)
            {
                return;
            }
            //分页批处理
            pageCount = rowCount / pageSize;
            if (rowCount % pageSize != 0) 
            {
                pageCount++;
            }
            for (int i = 0; i < pageCount; i++)
            {
                int sRow = i * pageSize;
                int eRow = (i + 1) * pageSize;
                List<ProductInfo> productInfoList = productBLL.Product_Page(sRow,eRow);
                for (int j = 0; j < productInfoList.Count; j++)
                {
                    int fromProduct = productInfoList[j].ProductID;
                    lblProduct.Text =fromProduct+"-->"+ productInfoList[j].CADN;
                    lblUpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    if (productInfoList[j].Comments == -1)
                    {
                        productInfoList[j].Comments = 0;
                    }
                    try
                    {
                        if (insertMaxId >= fromProduct)
                        {
                            lblAction.Text = "更新";
                            prdouctMysqlBLL.UpdateProductMySql(productInfoList[j]);
                        }
                        else
                        {
                            lblAction.Text = "插入";
                            prdouctMysqlBLL.InsertProductMySql(productInfoList[j]);
                        }
                    }
                    catch(Exception ex) 
                    {
                        //数据库连接失败数据 游标移动至操作失败的数据
                        j = j - 1;
                        this.notifyIcon1.Visible = true;
                        this.notifyIcon1.ShowBalloonTip(5, "提示",
                                "更新后台时,MySql数据库连接失败，将于"+DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd hh:mm:ss")+"恢复执行！", ToolTipIcon.Info);
                        //System.Threading.Thread.Sleep(3600 * 5);
                        continue;
                    }
                   
                }
            }
        }
        
        #region 界面性效果
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }
        private void NotifyInit() 
        {
            NotifyHide();
        }
        private void NotifyHide() 
        {
            this.Hide();
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(3, "提示", "正在执行Product表更新动作...", ToolTipIcon.Info);
            this.Visible = false;
            this.notifyIcon1.Text = "商品更新后台程序";
        }
        private void NotifyShow() 
        {

        }
        private void OutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Dispose();
                this.Close();
            }
        }
        private void Product_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                NotifyHide();
            }
        }

        private void Product_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }
        #endregion

      
    }
}
