using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Volo.Abp;
using wrap_test.Serivces;

namespace MainWinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeAbpApplication();
        }

        private static IAbpApplicationWithInternalServiceProvider _application;
        private static void InitializeAbpApplication()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddAppSettingsSecretsJson()
                .Build();


            _application = AbpApplicationFactory.Create<WinformApiClientModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging();

            });
            _application.Initialize();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var service = _application.ServiceProvider.GetRequiredService<IBookAppService>();
            var dto = await service.GetText();
            textBox1.Text = dto.Text;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var service = _application.ServiceProvider.GetRequiredService<IBookAppService>();
            textBox1.Text = await service.GetTextString();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var service = _application.ServiceProvider.GetRequiredService<IBookAppService>();
            textBox1.Text = await service.GetTextEmpty();
        }
    }
}
