namespace cmet_backend.Article
{
    public class ArticleRq
    {
        public string Title {  get; set; }
        public string Text { get; set; }
        public List<FileRq> Files { get; set; }
    }
}
