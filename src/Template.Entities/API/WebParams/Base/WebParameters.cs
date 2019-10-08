namespace Template.Entities.API.WebParams.Base
{
    public class WebParameters : PagingParameters
    {
        public string Sort { get; set; } = "Id";
        public string SortDir { get; set; } = "DESC";

        public void StripPaging()
        {
            Page = null;
            PerPage = null;
        }
    }
}
