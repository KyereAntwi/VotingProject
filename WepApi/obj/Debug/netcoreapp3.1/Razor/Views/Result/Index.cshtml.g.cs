#pragma checksum "/Users/kyere/Desktop/VotingProject/WepApi/Views/Result/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "92d86f6d137257c07b95f0d74f937d39625394cc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Result_Index), @"mvc.1.0.view", @"/Views/Result/Index.cshtml")]
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
#nullable restore
#line 1 "/Users/kyere/Desktop/VotingProject/WepApi/Views/_ViewImports.cshtml"
using WepApi.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/kyere/Desktop/VotingProject/WepApi/Views/_ViewImports.cshtml"
using DTOs.DTOs;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"92d86f6d137257c07b95f0d74f937d39625394cc", @"/Views/Result/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d0cc6305c17fad177bcea740da7f65d1e972366c", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Result_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PollDto>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<div class=\"container\">\n");
#nullable restore
#line 4 "/Users/kyere/Desktop/VotingProject/WepApi/Views/Result/Index.cshtml"
     if (Model.CategoryDtos.Count > 0)
    {
        foreach (var cat in Model.CategoryDtos) 
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/kyere/Desktop/VotingProject/WepApi/Views/Result/Index.cshtml"
       Write(await Component.InvokeAsync("CategoryResults", new { CategoryId = cat.Id }));

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/kyere/Desktop/VotingProject/WepApi/Views/Result/Index.cshtml"
                                                                                        
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PollDto> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
