using Core.Constans;

namespace Web.ViewModels.Contact
{
    public class ContactIndexVM
    {
        public Core.Entities.ContactInfo? ContactInfo { get; set; }


        public string FullName { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public ContactStatus? Status { get; set; } = 0;
    }
}
