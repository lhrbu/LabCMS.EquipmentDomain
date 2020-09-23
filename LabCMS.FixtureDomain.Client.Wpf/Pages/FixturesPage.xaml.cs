using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabCMS.FixtureDomain.Client.Wpf.Pages
{
    /// <summary>
    /// FixturesPage.xaml 的交互逻辑
    /// </summary>
    public partial class FixturesPage : Window
    {
        public FixturesPage()
        {
            InitializeComponent();
            List<A> items = new()
            {
                new A(),
                new A(),
                new A()
            };
            DG.ItemsSource = items;
        }
    }

    public class A
    {
        public int Key => 1;
        public string Value => "Value";
    }
}
