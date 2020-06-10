
//The MIT License(MIT)

//Copyright(c) 2014 Fred Peters

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    /// <summary>
    /// A Website Health Check Page <see cref="http://www.fredwebs.com/2015/01/a-website-health-check-page/"/>
    /// </summary>
    public partial class HealthCheck : System.Web.UI.Page
    {
        protected bool ItemFailed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            // EntityFramework
            try
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    ItemFailed = !context.Database.Exists();
                }
            }
            catch (Exception)
            {
                // Log exception
                ItemFailed = true;
            }

            // Directorio
            try
            {
                var path = "";
                if (!Directory.Exists(path))
                {
                    ItemFailed = true;
                }
            }
            catch (IOException)
            {
                // Log exception
                ItemFailed = true;
            }

            // Se genera el código de respuesta
            if (ItemFailed)
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            else
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

            Response.End();
        }
    }
}