using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemeGeneratorProject;

namespace MemeGenerator.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["FbuserToken"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {

            ViewBag.UrlFb = GetFacebookLoginUrl();
            return View();
        }

        public string GetFacebookLoginUrl()
        {
            dynamic parameters = new ExpandoObject();
            parameters.client_id = "1428364047191112";
            parameters.redirect_uri = Request.Url + "/home/retornofb";
            parameters.response_type = "code";
            parameters.display = "page";

            var extendedPermissions = "user_about_me,publish_actions";
            parameters.scope = extendedPermissions;

            var _fb = new FacebookClient();
            var url = _fb.GetLoginUrl(parameters);

            return url.ToString();
        }


        public ActionResult PublicarFoto(string text)
        {

            var pirqui = new MemeGeneratorProject.MemeGenerator();
            var obj = pirqui.PirquiGenerator(text, Server.MapPath("~/Content/img/pirqui/meme.jpg"));
            obj.Save(Server.MapPath("~/Content/resultado.jpg"));

            if (Session["FbuserToken"] != null)
            {
                var _fb = new FacebookClient(Session["FbuserToken"].ToString());
                //upload de imagem
                FacebookMediaObject media = new FacebookMediaObject
                {
                    FileName = "Nome da foto",
                    ContentType = "image/jpeg"
                };

                byte[] img = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/resultado.jpg"));
                media.SetValue(img);

                dynamic parameters = new ExpandoObject();

                parameters.source = media;
                try
                {
                    dynamic result = _fb.Post("/me/photos", parameters);

                }
                catch (Exception ex)
                {

                    Session.Remove("FbuserToken");
        
                    return this.RedirectToAction("ErrorPage");

                }
            }

            Session.Remove("FbuserToken");
            return Redirect("http://www.facebook.com/");

        }
        public ActionResult RetornoFb()
        {
            var _fb = new FacebookClient();
            FacebookOAuthResult oauthResult;

            _fb.TryParseOAuthCallbackUrl(Request.Url, out oauthResult);

            if (oauthResult.IsSuccess)
            {
                //Pega o Access Token "permanente"
                dynamic parameters = new ExpandoObject();
                parameters.client_id = "1428364047191112";
                parameters.redirect_uri = Request.Url + "/home/retornofb";
                parameters.client_secret = "27c5071280201e202ae7d2f2abb520ea";
                parameters.code = oauthResult.Code;

                dynamic result = _fb.Get("/oauth/access_token", parameters);

                var accessToken = result.access_token;

                //TODO: Guardar no banco
                Session.Add("FbUserToken", accessToken);
            }
            else
            {

                // tratar
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult ErrorPage()
        {


            return View();
        }
    }
}
