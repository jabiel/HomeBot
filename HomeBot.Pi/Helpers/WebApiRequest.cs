using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HomeBot.Pi
{
    [Serializable]
    public class WebApiException : Exception
    {

        public class ExceptionModel
        {
            public string Message { get; set; }
            public string ExceptionMessage { get; set; }
            public string ExceptionType { get; set; }
            public string StackTrace { get; set; }
            public ExceptionModel InnerException { get; set; }
        }


        public ExceptionModel Json { get; set; }
        public string Html { get; set; }
        public WebApiException() : base() {}
        public WebApiException(string message) : base(message) { }
        public WebApiException(string message, Exception ex) : base(message, ex) {

            if (message.StartsWith("{") || message.StartsWith("["))
            {
                Json = JsonConvert.DeserializeObject<ExceptionModel>(message);
                if (!string.IsNullOrWhiteSpace(Json.ExceptionMessage))
                {
                    message = Json.ExceptionMessage;
                }
                else if (!string.IsNullOrWhiteSpace(Json.Message))
                {
                    message = Json.Message;
                }
            }

            if (message.StartsWith("<") || message.ToLower().Contains("<html"))
            {
                Html = StripHTML(message);
                Html = Html.Replace("&nbsp;", " ");

                for (int i = 0; i < 9; i++)
                    Html = Html.Replace("  ", " ");

                Html = Regex.Replace(Html, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            }
        }

        private string StripHTML(string htmlString)
        {

            string pattern = @"<(.|\n)*?>";

            return Regex.Replace(htmlString, pattern, string.Empty);
        }
    }

    public static class WebApiRequest
    {
        enum Method
        {
            POST,
            GET
        }

        public static string GetToken(string url, string body)
        {
            var result = RequestResult(url, Method.POST, null, body);
            var owin = JsonConvert.DeserializeObject<OwinModel>(result);
            return owin.access_token;
        }

        public static string GetToken(string url, object model)
        {
            var body = JsonConvert.SerializeObject(model);
            return GetToken(url, body);
        }

        ///////// GET ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///
        public static T Get<T>(string url, string token, string body = null) where T : class
        {
            var result = RequestResult(url, Method.GET, token, null);
            return JsonConvert.DeserializeObject<T>(result);
        }

        public static string Get(string url, string token, string body = null, IDictionary<string, string> headers = null)
        {
            var result = RequestResult(url, Method.GET, token, null, headers);
            return result;
        }

        ///////// POST ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///
        public static string Post(string url, string token, object model, IDictionary<string, string> headers)
        {
            var body = JsonConvert.SerializeObject(model);
            var result = RequestResult(url, Method.POST, token, body, headers);
            return result;
        }

        public static T Post<T>(string url, string token, object model, IDictionary<string, string> headers)
        {
            var body = JsonConvert.SerializeObject(model);
            var result = RequestResult(url, Method.POST, token, body, headers);
            return JsonConvert.DeserializeObject<T>(result);
        }

        public static string Post(string url, string token, object model)
        {
            return Post(url, token, model, null);
        }

        public static T Post<T>(string url, string token, object model) where T : class
        {
            var body = JsonConvert.SerializeObject(model);
            var result = RequestResult(url, Method.POST, token, body);
            return JsonConvert.DeserializeObject<T>(result);
        }

        private static string RequestResult(string url, Method method, string token, string body, IDictionary<string, string> headers = null)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method.ToString(); // POST, GET

            if (!string.IsNullOrEmpty(token))
            {
                httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
            }

            if (headers != null)
            {
                foreach (var h in headers)
                {
                    httpWebRequest.Headers.Add(h.Key, h.Value);
                }
            }


            var result = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(body))
                {
                    var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                    streamWriter.Write(body);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                var streamReader = new StreamReader(httpResponse.GetResponseStream());

                result = streamReader.ReadToEnd();
                streamReader.Dispose();
            }
            // catch BadRequest messages
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var response = wex.Response)
                    {
                        var httpResponse = (HttpWebResponse)response;
                        var data = response.GetResponseStream();
                        using (var reader = new StreamReader(data))
                        {
                            var text = reader.ReadToEnd();
                            throw new WebApiException(text, wex);
                        }
                    }
                }
                else
                {
                    throw new WebApiException("See inner exception", wex);
                }

            }
            catch (Exception ex)
            {
                throw new WebApiException("See inner exception", ex);
            }
            return result;
        }
    }

    public class OwinModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string client_id { get; set; }
        public string userName { get; set; }
    }
}
