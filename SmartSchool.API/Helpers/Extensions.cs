using Newtonsoft.Json;
namespace SmartSchool.API.Helpers;
public static class Extensions
{
    public static void AddPagination(this HttpResponse response, int TotalItems, int ItemPerPage, int CurrentPage, int TotalPages)
    {
        var paginationHeader = new PaginationHeader(TotalItems, ItemPerPage, CurrentPage, TotalPages);

        response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader));

        response.Headers.Add("Acess-Control-Expose-Header", "Pagination");
    }
}
