namespace Web.Data.ViewModels;
public class PagedListViewModel<T>
{
    
    public List<T> Items { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalItem { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalItem / PageSize);

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

}