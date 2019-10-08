namespace Template.Entities.API.WebParams.Base
{
    public class PagingParameters
    {
        public int? Page { get; set; } = 1;
        public int? PerPage { get; set; } = 25;
    }
}
