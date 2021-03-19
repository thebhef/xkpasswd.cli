using System.Collections.Generic;

namespace xkpasswd.cli
{
    public class XkPasswdConfiguration
    {
        public int num_words { get; set; }

        public int word_length_min { get; set; }

        public int word_length_max { get; set; }

        public string case_transform { get; set; }

        public string? separator_character { get; set; }

        public List<char> separator_alphabet { get; set; }

        public int padding_digits_before { get; set; }

        public int padding_digits_after { get; set; }

        public string padding_type { get; set; }

        public string? padding_character { get; set; }

        public List<char> symbol_alphabet { get; set; }

        public int padding_characters_before { get; set; }

        public int padding_characters_after { get; set; }
    }
}