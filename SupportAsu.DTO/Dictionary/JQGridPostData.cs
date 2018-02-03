
namespace SupportAsu.DTO.Dictionary
{
    public class JQGridPostData
    {
        public int Page { get; set; }
        public int Rows { get; set; }
        public string Sidx { get; set; }
        public string Sord { get; set; }


        
        public void SetCorrectPage(int totalRecords)
        {
            while ((Page - 1) * Rows >= totalRecords && Page != 1)
                Page = Page - 1;
        }
    }
}
