using MimeKit;

namespace DressToImpress.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> To {  get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection Attachements { get; set; }

        public Mensagem(IEnumerable<string> to, string subject, string content,
            IFormFileCollection attachements)
        {

        }
    }
}
