using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Newtonsoft.Json;

namespace BookStore.Order.Service;

public class BookService : IBookServices
{
    private IConfiguration configuration;

    public BookService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public async Task<BookEntity> GetBookDetails(int id)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(configuration["BaseURL:Book"] +"get-BybookId?bookID=" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                ResponseModel<BookEntity> responseModel = JsonConvert.DeserializeObject<ResponseModel<BookEntity>>(content);

                return responseModel.Data;
            }
            return null;
        }
    }
}
