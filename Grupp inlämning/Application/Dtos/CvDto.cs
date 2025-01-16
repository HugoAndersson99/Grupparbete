

namespace Application.Dtos
{
    public class CvDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public string PdfUrl { get; set; }
    }
}
