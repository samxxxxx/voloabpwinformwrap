using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace wrap_test.Serivces
{
    public interface IBookAppService : IApplicationService
    {
        Task<BookDto> GetText();
        Task<string> GetTextString();

        Task<string> GetTextEmpty();
    }
}
