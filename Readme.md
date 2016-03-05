#HQF.Tutorial.WebAPI.Indentity2

## Part1
[Configure ASP.NET Identity with ASP.NET Web API (Accounts Management)](http://bitoftech.net/2015/01/21/asp-net-identity-2-with-asp-net-web-api-2-accounts-management/)


```
Install-Package Microsoft.AspNet.Identity.Owin -Version 2.1.0
Install-Package Microsoft.AspNet.Identity.EntityFramework -Version 2.1.0
Install-Package Microsoft.Owin.Host.SystemWeb -Version 3.0.0
Install-Package Microsoft.AspNet.WebApi.Owin -Version 5.2.2
Install-Package Microsoft.Owin.Security.OAuth -Version 3.0.0
Install-Package Microsoft.Owin.Cors -Version 3.0.0

```
### More Resources
 - [Implementing ASP.NET Identity](http://odetocode.com/blogs/scott/archive/2014/01/20/implementing-asp-net-identity.aspx)
 
## Part2
[ASP.NET Identity 2.1 Accounts Confirmation, and Password Policy Configuration  Part 2](http://bitoftech.net/2015/02/03/asp-net-identity-2-accounts-confirmation-password-user-policy-configuration/)  



### Install Send Grid
 Send Grid which is service provider for sending emails, but you can use any other service provider or your exchange change server to do this. 


```
install-package Sendgrid

```

## Part3
[Implement OAuth JSON Web Tokens Authentication in ASP.NET Web API and Identity 2.1  Part 3](http://bitoftech.net/2015/02/16/implement-oauth-json-web-tokens-authentication-in-asp-net-web-api-and-identity-2/)   

we want to configure our API to issue JWT tokens instead of default access tokens, to understand what is JWT and why it is better to use it, you can refer back to this [post](http://bitoftech.net/2014/10/27/json-web-token-asp-net-web-api-2-jwt-owin-authorization-server/).   
```
Install-package System.IdentityModel.Tokens.Jwt -Version 4.0.1
Install-package Thinktecture.IdentityModel.Core -Version 1.3.0
```
There are two more packages that you need to install. One of them is `Microsoft.AspNet.Identity.Owin`. This package provides several useful extensions you will use while working with ASP.NET Identity on top of OWIN. The other one is `Microsoft.Owin.Host.SystemWeb` package which enables OWIN-based applications to run on IIS using the ASP.NET request pipeline.   
>The packages we just installed (`Microsoft.AspNet.Identity.Owin`) also brought down some other packages as its dependencies. One of those dependency packages is `Microsoft.Owin.Security.OAuth` and this is the core package that includes the components to support any standard OAuth 2.0 authentication workflow. Just wanted to highlight this fact as this is an important part of the project.

###Problem 1
The `StartUp` class is nor fired. 
- [OwinStartup not firing](https://stackoverflow.com/questions/20203982/owinstartup-not-firing)


###More ReSources
 - [Simple OAuth Server: Implementing a Simple OAuth Server with Katana OAuth Authorization Server Components (Part 1)](http://www.tugberkugurlu.com/archive/simple-oauth-server-implementing-a-simple-oauth-server-with-katana-oauth-authorization-server-components-part-1)  
 - [Here's a great resource on OWIN Startup Class Detection.](http://www.asp.net/aspnet/overview/owin-and-katana/owin-startup-class-detection)
 
