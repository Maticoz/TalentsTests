using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TalentsApi.Entities.BaseEntities
{
    [Index(nameof(Slug), IsUnique = true)]
    public abstract class SluggableEntity : BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string Slug { get; set; }

        public abstract string GetPhrase();

        public string GenerateSlug()
        {
            string phrase = GetPhrase();
            string str = RemoveAccent(phrase.ToLower());

            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
}

        public string RemoveAccent(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
                return txt;

            txt = txt.Normalize(NormalizationForm.FormD);
            char[] chars = txt
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
                != UnicodeCategory.NonSpacingMark).ToArray();

            return new string(chars).Normalize(NormalizationForm.FormC);
        }
    }
}
