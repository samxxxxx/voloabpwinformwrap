using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using wrap_test.Serivces;

namespace wrap_test.Services
{

    public class BookAppService : ApplicationService, IBookAppService
    {
        public BookAppService() { }

        public async Task<BookDto> GetText()
        {
            var dto = new BookDto() { Text = "hello world" };
            return dto;
        }

        public async Task<string> GetTextString()
        {
            return "string";
        }

        public async Task<string> GetTextEmpty()
        {
            return string.Empty;
        }
    }
}
