using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefSharpWinform.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var browser = new CefSharp.WinForms.ChromiumWebBrowser("http://www.google.com") { Dock = DockStyle.Fill } ;
            this.Controls.Add(browser);
        }
    }
}