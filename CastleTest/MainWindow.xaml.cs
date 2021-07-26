using AsyncInterceptor;
using Castle.DynamicProxy;
using Interceptor;
using Prism.Ioc;
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
        private readonly ProxyGenerator generator = new ProxyGenerator();
        private readonly TestB test;

        public MainWindow(IContainerExtension container)
        {
            var logInterceptor = container.Resolve<LogInterceptor>();
            test = generator.CreateClassProxy<TestB>(logInterceptor);

            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var test = CastleInterceptor.CreateProxy<TestB>();

            //Tips:Castle动态生成的Assembly -> DynamicProxyGenAssembly2
            //“CastleTest.exe”(CoreCLR: clrhost): 已加载“DynamicProxyGenAssembly2”。

            //当前类型: Castle.Proxies.TestBProxy, 父类型: CastleTest.TestB
            Trace.WriteLine($"当前类型: {test.GetType()}, 父类型: {test.GetType().BaseType}");

            await test.GetResultAsync();
        }

        private void buttonSynchronous_Click(object sender, RoutedEventArgs e)
        {
            test.GetResultWithArgument(DateTime.Now);
        }

        private async void buttonAction_Click(object sender, RoutedEventArgs e)
        {
            await test.TestActionAsync();
        }

        private async void buttonAsync_Click(object sender, RoutedEventArgs e)
        {
            await test.GetResultAsync();
        }
    }
}
