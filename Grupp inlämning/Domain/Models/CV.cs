

namespace Domain.Models
{
    public class CV
    {
        public CV()
        {
        }

        public CV(string title, Guid userId, string pdfUrl, User user)
        {
            Title = title;
            UserId = userId;
            PdfUrl = pdfUrl;
            User = user;
        }

        public CV(Guid id, string title, Guid userId, string pdfUrl, User user)
        {
            Id = id;
            Title = title;
            UserId = userId;
            PdfUrl = pdfUrl;
            User = user;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public string PdfUrl { get; set; }

        public User User { get; set; }
    }
}
