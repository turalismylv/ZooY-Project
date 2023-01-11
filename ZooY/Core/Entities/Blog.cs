using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string DescriptionOne { get; set; }
        public string DescriptionTwo { get; set; }
        public string DescriptionThree { get; set; }

        public string DetailTitle { get; set; }

        public string MainPhotoName { get; set; }

        public string PhotoName { get; set; }

        public DateTime Time { get; set; }

        public string Paragraph { get; set; }

    }
}
