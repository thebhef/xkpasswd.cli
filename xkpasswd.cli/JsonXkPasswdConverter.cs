using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using XkPassword;

namespace xkpasswd.cli
{
    public static class JsonXkPasswdConverter
    {
        public static Func<XkPasswd> FromJson(string jsonString)
        {
            var config = JsonConvert.DeserializeObject<XkPasswdConfiguration>(jsonString);
            var symbolAlphabet = new HashSet<char>(config.symbol_alphabet!);
            var separatorAlphabet = new HashSet<char>(config.separator_alphabet!);
            var randomSource = GetRandomSource();

            var transform = Enum.TryParse<CaseTransformation>(config.case_transform, true, out var ct)
                ? ct == CaseTransformation.Random ? CaseTransformation.RandomWord : ct
                : CaseTransformation.RandomWord;

            XkPasswd? generator = null;
            return GetGenerator;


            XkPasswd GetGenerator()
            {
                var rval = generator
                           ?? new()
                           {
                               CaseTransform = transform,
                               PaddingType = Enum.TryParse<Padding>(config.padding_type, true, out var pt) ? pt : Padding.Fixed,
                               RandomSource = randomSource,
                               SeparatorAlphabet = separatorAlphabet,
                               SymbolAlphabet = symbolAlphabet,
                               WordCount = config.num_words,
                               MaxWordLength = config.word_length_max,
                               MinWordLength = config.word_length_min,
                               PaddingCharactersAfter = config.padding_characters_after,
                               PaddingCharactersBefore = config.padding_characters_before,
                               PaddingDigitsAfter = config.padding_digits_after,
                               PaddingDigitsBefore = config.padding_digits_before
                           };

                rval.SeparatorCharacter = GetCharacterOrRandom(config.separator_character, separatorAlphabet, randomSource);
                rval.PaddingCharacter = GetCharacterOrRandom(config.padding_character, symbolAlphabet, randomSource);
                return rval;
            }
        }


        private static char? GetCharacterOrRandom(string? configPaddingCharacter,
            HashSet<char> rvalSymbolAlphabet,
            IRandomSource randomSource)
            => char.TryParse(configPaddingCharacter, out var c)
                ? c
                : rvalSymbolAlphabet.ToList().ElementAt(randomSource.Next(0, rvalSymbolAlphabet.Count));

        private static IRandomSource GetRandomSource() => new RandomSource();
    }
}