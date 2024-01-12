using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Utils.HelperFuncs;

public class StringHelper
{
    private static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }
        
        var result = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        // Manually replace 'đ' with 'd'
        result = result.Replace("đ", "d");

        return result;
    }
    
    public static string GenerateSlug(string phrase)
    {
        var str = RemoveDiacritics(phrase).ToLower();
        // invalid chars           
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        // convert multiple spaces into one space   
        str = Regex.Replace(str, @"\s+", " ").Trim();
        // cut and trim 
        str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
        str = Regex.Replace(str, @"\s", "-"); // hyphens   
        return str;
    }
    
    public static bool DoesStringContainKeyword(string str, string keyword)
    {
        return RemoveDiacritics(str.ToLower()).Contains(RemoveDiacritics(keyword.ToLower()));
    }
}