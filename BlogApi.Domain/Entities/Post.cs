namespace BlogApi.Domain.Entities
{
    public record Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public void Update(string title, string content)
        {
            if(string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException("title cannot be empty");
            Title = title;
            Content = content;
        }
    }
}
