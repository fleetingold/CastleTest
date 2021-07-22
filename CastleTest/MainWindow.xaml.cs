using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CastleTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var test = CastleInterceptor.CreateProxy<TestB>();

            //Tips:Castle动态生成的Assembly -> DynamicProxyGenAssembly2
            //“CastleTest.exe”(CoreCLR: clrhost): 已加载“DynamicProxyGenAssembly2”。

            //当前类型: Castle.Proxies.TestBProxy, 父类型: CastleTest.TestB
            Trace.WriteLine($"当前类型: {test.GetType()}, 父类型: {test.GetType().BaseType}");

            test.GetResult();
        }

        private void buttonAsync_Click(object sender, RoutedEventArgs e)
        {
            ProxyGenerator generator = new ProxyGenerator();

            
        }
    }
}
