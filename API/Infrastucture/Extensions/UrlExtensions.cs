namespace API.Infrastucture.Extensions
{
    public static class UrlExtensions
    {
        public static string GetAbsoluteUrl(this string relativeUrl, IConfiguration configuration) {

            string apiUrl = configuration["ApiUrl"];
            string result = null;

            if (!string.IsNullOrEmpty(relativeUrl))
            {
                result = $"{apiUrl}/{relativeUrl}";
            }

            return result;
        }
    }
}
