namespace TalentsApi.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItemsCount { get; set; }
        public int CurrentPage { get; set; }

        public PagedResult(List<T> items, int totalCount, object pageSize, int pageNumber)
        {
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (TotalPages == 0)
            {
                TotalPages = 1;
            }

            if (TotalPages < pageNumber)
            {
                CurrentPage = TotalPages;
            }
            else
            {
                CurrentPage = pageNumber;
            }

            Items = items;
            TotalItemsCount = totalCount;
            ItemsFrom = (int)pageSize * (CurrentPage - 1) + 1;
            ItemsTo = ItemsFrom + (int)pageSize - 1;
        }
    }
}
