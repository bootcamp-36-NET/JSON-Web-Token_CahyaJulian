#pragma checksum "C:\Users\User\source\repos\LearnNetCore\JSON-Web-Token_CahyaJulian\LearnNetCore\WebApp\Views\AccountAuth\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f035ebde5e267abedc8ecba93f052997b62ab60f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_AccountAuth_Profile), @"mvc.1.0.view", @"/Views/AccountAuth/Profile.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/AccountAuth/Profile.cshtml", typeof(AspNetCore.Views_AccountAuth_Profile))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\User\source\repos\LearnNetCore\JSON-Web-Token_CahyaJulian\LearnNetCore\WebApp\Views\_ViewImports.cshtml"
using WebApp;

#line default
#line hidden
#line 2 "C:\Users\User\source\repos\LearnNetCore\JSON-Web-Token_CahyaJulian\LearnNetCore\WebApp\Views\_ViewImports.cshtml"
using WebApp.Models;

#line default
#line hidden
#line 1 "C:\Users\User\source\repos\LearnNetCore\JSON-Web-Token_CahyaJulian\LearnNetCore\WebApp\Views\AccountAuth\Profile.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f035ebde5e267abedc8ecba93f052997b62ab60f", @"/Views/AccountAuth/Profile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc48f17eb9bac3476d8060730298bf398eb2fa5e", @"/Views/_ViewImports.cshtml")]
    public class Views_AccountAuth_Profile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("form-division"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\User\source\repos\LearnNetCore\JSON-Web-Token_CahyaJulian\LearnNetCore\WebApp\Views\AccountAuth\Profile.cshtml"
  
    ViewData["Title"] = "Profile";
    Layout = "~/Views/Layout/Layout.cshtml";
    var userId = Context.Session.GetString("id");
    var uname = Context.Session.GetString("uname");
    var mail = Context.Session.GetString("email");
    var level = Context.Session.GetString("lvl");
    if (mail == null)
    {
        Context.Response.Redirect("/login");
    }

#line default
#line hidden
            BeginContext(413, 50, true);
            WriteLiteral("\r\n<h1>Profile</h1>\r\n<div class=\"modal-body\">\r\n    ");
            EndContext();
            BeginContext(463, 635, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b3345c5d9c4d4634972ff52524187001", async() => {
                BeginContext(500, 145, true);
                WriteLiteral("\r\n        <input name=\"id\" type=\"hidden\" />\r\n        <div class=\"form-group\">\r\n            <label>Id : </label>\r\n            <span class=\"table\">");
                EndContext();
                BeginContext(646, 6, false);
#line 21 "C:\Users\User\source\repos\LearnNetCore\JSON-Web-Token_CahyaJulian\LearnNetCore\WebApp\Views\AccountAuth\Profile.cshtml"
                           Write(userId);

#line default
#line hidden
                EndContext();
                BeginContext(652, 132, true);
                WriteLiteral("</span>\r\n        </div>\r\n        <div class=\"form-group\">\r\n            <label> Username : </label>\r\n            <span class=\"table\">");
                EndContext();
                BeginContext(785, 5, false);
#line 25 "C:\Users\User\source\repos\LearnNetCore\JSON-Web-Token_CahyaJulian\LearnNetCore\WebApp\Views\AccountAuth\Profile.cshtml"
                           Write(uname);

#line default
#line hidden
                EndContext();
                BeginContext(790, 129, true);
                WriteLiteral("</span>\r\n        </div>\r\n        <div class=\"form-group\">\r\n            <label> Email : </label>\r\n            <span class=\"table\">");
                EndContext();
                BeginContext(920, 4, false);
#line 29 "C:\Users\User\source\repos\LearnNetCore\JSON-Web-Token_CahyaJulian\LearnNetCore\WebApp\Views\AccountAuth\Profile.cshtml"
                           Write(mail);

#line default
#line hidden
                EndContext();
                BeginContext(924, 130, true);
                WriteLiteral("</span>\r\n        </div>\r\n        <div class=\"form-group\">\r\n            <label>Jabatan : </label>\r\n            <span class=\"table\">");
                EndContext();
                BeginContext(1055, 5, false);
#line 33 "C:\Users\User\source\repos\LearnNetCore\JSON-Web-Token_CahyaJulian\LearnNetCore\WebApp\Views\AccountAuth\Profile.cshtml"
                           Write(level);

#line default
#line hidden
                EndContext();
                BeginContext(1060, 31, true);
                WriteLiteral("</span>\r\n        </div>\r\n\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1098, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
            DefineSection("scriptdbAdmin", async() => {
                BeginContext(1131, 50, true);
                WriteLiteral("\r\n\r\n    <script type=\"text/javascript\"></script>\r\n");
                EndContext();
            }
            );
            BeginContext(1184, 2, true);
            WriteLiteral("\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
