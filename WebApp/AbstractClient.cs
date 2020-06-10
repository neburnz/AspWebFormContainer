using CJF.Common.Services;

using Common.Security.SISEG;

using Newtonsoft.Json;

using System;

using System.Collections.Generic;

using System.IO;

using System.Linq;

using System.Net;

using System.Web;

using Polly;



namespace siavi.UI.Clases.WebApiClient

{

    public abstract class AbstractClient

    {

        protected string urlBase;

        protected AbstractClient(string urlBase)

        {

            this.urlBase = urlBase;

        }



        protected T Get<T>(string relativeUrl)

        {

            return Call<T>(relativeUrl, "GET", null);

        }



        protected T Post<T>(string relativeUrl, object param)

        {

            return Call<T>(relativeUrl, "POST", param);

        }



        protected T Post<T>(string relativeUrl)

        {

            return Call<T>(relativeUrl, "POST", null);

        }



        protected T Put<T>(string relativeUrl, object param)

        {

            return Call<T>(relativeUrl, "PUT", param);

        }



        protected T Put<T>(string relativeUrl)

        {

            return Call<T>(relativeUrl, "PUT", null);

        }



        protected T Delete<T>(string relativeUrl, object param)

        {

            return Call<T>(relativeUrl, "DELETE", param);

        }



        protected T Delete<T>(string relativeUrl)

        {

            return Call<T>(relativeUrl, "DELETE", null);

        }



        protected T Call<T>(string relativeUrl, string method, object param)

        {

            ServiceResponse<T> response = null;

            var request = WebRequest.Create(urlBase + relativeUrl);

            request.Method = method;

            request.ContentType = "application/json; charset=utf-8";

            request.Headers["Authorization"] = "Bearer " + SecurityContext.GetJwt();

            // Configurar Policy para manejar excepciones lanzadas por WebRequest
            var retryPolicy = Policy.Handle<NotSupportedException>().Or<NotImplementedException>()
            // Realiza un número especificado de intentos calculando la duración en segundos entre cada uno de ellos
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            // Ejecuta Policy agrupando las llamadas de WebRequest
            retryPolicy.Execute(() =>
            {
                if (param != null)
                {
                    using (var stream = new StreamWriter(request.GetRequestStream()))
                    {
                        stream.Write(JsonConvert.SerializeObject(param));
                        stream.Flush();
                    }
                }

                var webResponse = request.GetResponse();

                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            response = JsonConvert.DeserializeObject<ServiceResponse<T>>(responseReader.ReadToEnd());
                        }
                    }
                }
            });

            if (response == null)

            {

                throw new Exception("Problemas para obtener respuesta del servicio");

            }



            if (!response.Status)

            {

                throw new Exception(response.Error);

            }



            return response.DataResponse;

        }



    }

}