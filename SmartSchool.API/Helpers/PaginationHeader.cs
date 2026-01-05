namespace SmartSchool.API.Helpers;
public class PaginationHeader
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int ItemPerPage { get; set; }
    public int TotalItems { get; set; }

    public PaginationHeader(int currentPage, int totalPages, int itemPerPage, int totalItems)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        ItemPerPage = itemPerPage;
        TotalItems = totalItems;
    }
}
