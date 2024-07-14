[33mcommit a7f0325e485b817214b78ad3337283dd0ec90ab4[m[33m ([m[1;36mHEAD[m[33m -> [m[1;32mdev[m[33m)[m
Author: Ermek <spacermek@gmail.com>
Date:   Sun Jul 14 21:17:22 2024 +0600

    added serilog logging

[1mdiff --git a/.idea/.idea.online-store/.idea/cody_history.xml b/.idea/.idea.online-store/.idea/cody_history.xml[m
[1mindex 030a62d..76024fd 100644[m
[1m--- a/.idea/.idea.online-store/.idea/cody_history.xml[m
[1m+++ b/.idea/.idea.online-store/.idea/cody_history.xml[m
[36m@@ -47,6 +47,26 @@[m
                   </llm>[m
                 </llm>[m
               </chat>[m
[32m+[m[32m              <chat>[m
[32m+[m[32m                <internalId value="54d7ead3-e4bc-4c19-a0b1-672e20d59eed" />[m
[32m+[m[32m                <llm>[m
[32m+[m[32m                  <llm>[m
[32m+[m[32m                    <model value="anthropic/claude-3-sonnet-20240229" />[m
[32m+[m[32m                    <provider value="Anthropic" />[m
[32m+[m[32m                    <title value="Claude 3 Sonnet" />[m
[32m+[m[32m                  </llm>[m
[32m+[m[32m                </llm>[m
[32m+[m[32m              </chat>[m
[32m+[m[32m              <chat>[m
[32m+[m[32m                <internalId value="ac527980-2fce-4614-acfa-10e31692c872" />[m
[32m+[m[32m                <llm>[m
[32m+[m[32m                  <llm>[m
[32m+[m[32m                    <model value="anthropic/claude-3-sonnet-20240229" />[m
[32m+[m[32m                    <provider value="Anthropic" />[m
[32m+[m[32m                    <title value="Claude 3 Sonnet" />[m
[32m+[m[32m                  </llm>[m
[32m+[m[32m                </llm>[m
[32m+[m[32m              </chat>[m
             </list>[m
           </chats>[m
           <defaultLlm>[m
[1mdiff --git a/API/API.csproj b/API/API.csproj[m
[1mindex 60f2b28..1421046 100644[m
[1m--- a/API/API.csproj[m
[1m+++ b/API/API.csproj[m
[36m@@ -14,6 +14,10 @@[m
           <PrivateAssets>all</PrivateAssets>[m
           <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>[m
         </PackageReference>[m
[32m+[m[32m        <PackageReference Include="Serilog" Version="4.0.1-dev-02205" />[m
[32m+[m[32m        <PackageReference Include="Serilog.AspNetCore" Version="8.0.2-dev-00341" />[m
[32m+[m[32m        <PackageReference Include="Serilog.Sinks.Debug" Version="3.0.0" />[m
[32m+[m[32m        <PackageReference Include="Serilog.Sinks.MSSqlServer" Version="6.6.2-dev-00080" />[m
         <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0"/>[m
     </ItemGroup>[m
 [m
[1mdiff --git a/API/Controllers/UserController.cs b/API/Controllers/UserController.cs[m
[1mindex bc19a43..f735d6f 100644[m
[1m--- a/API/Controllers/UserController.cs[m
[1m+++ b/API/Controllers/UserController.cs[m
[36m@@ -1,34 +1,78 @@[m
 ﻿using System.Net;[m
 using Abstraction.Interfaces.Services;[m
 using Microsoft.AspNetCore.Mvc;[m
[31m-using Model.Dtos;[m
[32m+[m[32musing Model.Dtos.User;[m
 [m
 namespace API.Controllers;[m
 [m
 [ApiController][m
 [Route("api/[controller]")][m
[31m-public class UserController[m
[32m+[m[32mpublic class UserController : ControllerBase[m
 {[m
     private readonly IUserService _userService;[m
[31m-[m
     public UserController(IUserService userService)[m
     {[m
         _userService = userService;[m
     }[m
 [m
[31m-    [HttpGet("authors")][m
[32m+[m[32m    [HttpGet("get-users")][m
     [ProducesResponseType((int)HttpStatusCode.OK)][m
     [ProducesResponseType((int)HttpStatusCode.NoContent)][m
[31m-    public IQueryable<UserDto> GetUsers()[m
[32m+[m[32m    public async Task<ActionResult<IQueryable<GetUserDto>>> GetUsers()[m
     {[m
[31m-        return _userService.GetUsers();[m
[32m+[m[32m        var users = await Task.FromResult(_userService.GetUsers());[m
[32m+[m[32m        if (!users.Any()) return NoContent();[m
[32m+[m[32m        return Ok(users);[m
     }[m
 [m
[31m-    [HttpPost("add-user")][m
[32m+[m[32m    [HttpGet("get-tracked-users")][m
     [ProducesResponseType((int)HttpStatusCode.OK)][m
     [ProducesResponseType((int)HttpStatusCode.NoContent)][m
[31m-    public async Task<int> AddUser(UserDto userDto)[m
[32m+[m[32m    public async Task<ActionResult<IQueryable<GetUserDto>>> GetTrackedUsers()[m
[32m+[m[32m    {[m
[32m+[m[32m        var users = await Task.FromResult(_userService.GetTrackedUsers());[m
[32m+[m[32m        if (!users.Any()) return NoContent();[m
[32m+[m[32m        return Ok(users);[m
[32m+[m[32m    }[m
[32m+[m
[32m+[m[32m    [HttpGet("get-user/{id}")][m
[32m+[m[32m    [ProducesResponseType((int)HttpStatusCode.OK)][m
[32m+[m[32m    public async Task<ActionResult<GetUserDto>> GetUser(int id)[m
[32m+[m[32m    {[m
[32m+[m[32m        var user = await _userService.GetUser(id);[m
[32m+[m[32m        return Ok(user);[m
[32m+[m[32m    }[m
[32m+[m
[32m+[m[32m    [HttpGet("get-tracked-user/{id}")][m
[32m+[m[32m    [ProducesResponseType((int)HttpStatusCode.OK)][m
[32m+[m[32m    [ProducesResponseType((int)HttpStatusCode.NotFound)][m
[32m+[m[32m    public async Task<ActionResult<GetUserDto>> GetTrackedUser(int id)[m
[32m+[m[32m    {[m
[32m+[m[32m        var user = await _userService.GetTrackedUser(id);[m
[32m+[m[32m        return Ok(user);[m
[32m+[m[32m    }[m
[32m+[m[41m    [m
[32m+[m[32m    [HttpPost("add-user")][m
[32m+[m[32m    [ProducesResponseType((int)HttpStatusCode.OK)][m
[32m+[m[32m    [ProducesResponseType((int)HttpStatusCode.BadRequest)][m
[32m+[m[32m    public async Task<ActionResult<int>> AddUser(AddUserDto addUserDto)[m
[32m+[m[32m    {[m
[32m+[m[32m       return Ok(await _userService.AddUser(addUserDto));[m
[32m+[m[32m    }[m
[32m+[m
[32m+[m[32m    [HttpPut("update-user")][m
[32m+[m[32m    [ProducesResponseType((int)HttpStatusCode.OK)][m
[32m+[m[32m    public async Task<ActionResult<int>> UpdateUser(UpdateUserDto updateUserDto)[m
[32m+[m[32m    {[m
[32m+[m[32m        var userId = await _userService.UpdateUser(updateUserDto);[m
[32m+[m[32m        return Ok(userId);[m
[32m+[m[32m    }[m
[32m+[m
[32m+[m[32m    [HttpDelete("delete-user/{id}")][m
[32m+[m[32m    [ProducesResponseType((int)HttpStatusCode.OK)][m
[32m+[m[32m    public async Task<ActionResult<int>> DeleteUser(int id)[m
     {[m
[31m-        return await _userService.AddUser(userDto);[m
[32m+[m[32m        var userId = await Task.FromResult(_userService.DeleteUser(id));[m
[32m+[m[32m        return Ok(userId);[m
     }[m
 }[m
\ No newline at end of file[m
[1mdiff --git a/API/Extensions/BllServicesServiceExtension.cs b/API/Extensions/BllServicesServiceExtension.cs[m
[1mindex d8a2148..48f4762 100644[m
[1m--- a/API/Extensions/BllServicesServiceExtension.cs[m
[1m+++ b/API/Extensions/BllServicesServiceExtension.cs[m
[36m@@ -1,7 +1,7 @@[m
 ﻿using Abstraction.Interfaces.Services;[m
 using BLL.Services;[m
 [m
[31m-namespace online_store.Extensions;[m
[32m+[m[32mnamespace API.Extensions;[m
 [m
 public static class BllServicesServiceExtension[m
 {[m
[1mdiff --git a/API/Extensions/EntityFrameworkExtension.cs b/API/Extensions/EntityFrameworkExtension.cs[m
[1mindex 385614a..9c3d5c1 100644[m
[1m--- a/API/Extensions/EntityFrameworkExtension.cs[m
[1m+++ b/API/Extensions/EntityFrameworkExtension.cs[m
[36m@@ -1,7 +1,7 @@[m
 ﻿using DAL.EntityFramework;[m
 using Microsoft.EntityFrameworkCore;[m
 [m
[31m-namespace online_store.Extensions;[m
[32m+[m[32mnamespace API.Extensions;[m
 [m
 public static class EntityFrameworkExtension[m
 {[m
[1mdiff --git a/API/Extensions/MapsterExstensions.cs b/API/Extensions/MapsterExstensions.cs[m
[1mindex 5304f8c..1c0be72 100644[m
[1m--- a/API/Extensions/MapsterExstensions.cs[m
[1m+++ b/API/Extensions/MapsterExstensions.cs[m
[36m@@ -1,10 +1,10 @@[m
 ﻿using Mapster;[m
[31m-using Model.Dtos;[m
[32m+[m[32musing Model.Dtos.User;[m
 using Model.Entities;[m
 [m
[31m-namespace online_store.Extensions;[m
[32m+[m[32mnamespace API.Extensions;[m
 [m
[31m-public static class MapsterExstensions[m
[32m+[m[32mpublic static class MapsterExtensions[m
 {[m
     public static void InitMapping(this IApplicationBuilder app)[m
     {[m
[36m@@ -13,6 +13,6 @@[m [mpublic static class MapsterExstensions[m
 [m
     private static void UserMapping(this IApplicationBuilder app)[m
     {[m
[31m-        TypeAdapterConfig<User, UserDto>.NewConfig();[m
[32m+[m[32m        TypeAdapterConfig<User, GetUserDto>.NewConfig();[m
     }[m
 }[m
\ No newline at end of file[m
[1mdiff --git a/API/Extensions/RepositoryServicesServiceExtension.cs b/API/Extensions/RepositoryServicesServiceExtension.cs[m
[1mindex f7bdcd1..b2d09fa 100644[m
[1m--- a/API/Extensions/RepositoryServicesServiceExtension.cs[m
[1m+++ b/API/Extensions/RepositoryServicesServiceExtension.cs[m
[36m@@ -1,7 +1,7 @@[m
 ﻿using Abstraction.Interfaces.Repositories;[m
 using DAL.Repositories;[m
 [m
[31m-namespace online_store.Extensions;[m
[32m+[m[32mnamespace API.Extensions;[m
 [m
 public static class RepositoryServicesServiceExtension[m
 {[m
[1mdiff --git a/API/Program.cs b/API/Program.cs[m
[1mindex b84b41e..ce3f5d1 100644[m
[1m--- a/API/Program.cs[m
[1m+++ b/API/Program.cs[m
[36m@@ -1,9 +1,13 @@[m
[31m-[m
[31m-[m
[31m-using online_store.Extensions;[m
[32m+[m[32musing API.Extensions;[m
[32m+[m[32musing Serilog;[m
 [m
 var builder = WebApplication.CreateBuilder(args);[m
[31m-[m
[32m+[m[32mbuilder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>[m[41m [m
[32m+[m[32m{[m
[32m+[m[32m    loggerConfiguration[m
[32m+[m[32m        .ReadFrom.Configuration(context.Configuration)[m[41m [m
[32m+[m[32m        .ReadFrom.Services(services);[m[41m [m
[32m+[m[32m});[m
 [m
 builder.Services.AddEndpointsApiExplorer();[m
 builder.Services.AddSwaggerGen();[m
[36m@@ -19,6 +23,7 @@[m [mif (app.Environment.IsDevelopment())[m
     app.UseSwaggerUI();[m
 }[m
 [m
[32m+[m[32mapp.UseSerilogRequestLogging();[m
 app.UseHttpsRedirection();[m
 app.MapControllers();[m
 [m
[1mdiff --git a/API/appsettings.json b/API/appsettings.json[m
[1mindex 9af1d52..5378b5e 100644[m
[1m--- a/API/appsettings.json[m
[1m+++ b/API/appsettings.json[m
[36m@@ -8,5 +8,29 @@[m
       "Microsoft.AspNetCore": "Warning"[m
     }[m
   },[m
[31m-  "AllowedHosts": "*"[m
[32m+[m[32m  "AllowedHosts": "*",[m
[32m+[m[32m  "Serilog": {[m
[32m+[m[32m    "MinimumLevel": "Information",[m
[32m+[m[32m    "Using":[m
[32m+[m[32m    [[m
[32m+[m[32m      "Serilog.Sinks.Console",[m
[32m+[m[32m      "Serilog.Sinks.MSSqlServer"[m
[32m+[m[32m    ],[m
[32m+[m[32m    "WriteTo":[m
[32m+[m[32m    [[m
[32m+[m[32m      {[m
[32m+[m[32m        "Name": "Console"[m
[32m+[m[32m      },[m
[32m+[m[32m      {[m
[32m+[m[32m        "Name": "MSSqlServer",[m
[32m+[m[32m        "Args":[m
[32m+[m[32m        {[m
[32m+[m[32m          "connectionString": "Server=MSI\\SQLEXPRESS;Database=CRUDLogs; Trusted_Connection=True;TrustServerCertificate=True;",[m
[32m+[m[32m          "tableName": "Logs",[m
[32m+[m[32m          "autoCreateSqlTable": true[m
[32m+[m[32m        }[m
[32m+[m[32m      }[m
[32m+[m[32m    ][m
[32m+[m[32m  }[m
 }[m
[41m+ [m
\ No newline at end of file[m
[1mdiff --git a/API/bin/Debug/net8.0/API.deps.json b/API/bin/Debug/net8.0/API.deps.json[m
[1mindex 573d991..7423b1c 100644[m
[1m--- a/API/bin/Debug/net8.0/API.deps.json[m
[1m+++ b/API/bin/Debug/net8.0/API.deps.json[m
[36m@@ -15,6 +15,10 @@[m
           "Microsoft.AspNetCore.OpenApi": "8.0.6",[m
           "Microsoft.EntityFrameworkCore.Design": "8.0.0",[m
           "Model": "1.0.0",[m
[32m+[m[32m          "Serilog": "4.0.1-dev-02205",[m
[32m+[m[32m          "Serilog.AspNetCore": "8.0.2-dev-00341",[m
[32m+[m[32m          "Serilog.Sinks.Debug": "3.0.0",[m
[32m+[m[32m          "Serilog.Sinks.MSSqlServer": "6.6.2-dev-00080",[m
           "Swashbuckle.AspNetCore": "6.4.0"[m
         },[m
         "runtime": {[m
[36m@@ -35,11 +39,11 @@[m
       "Azure.Core/1.35.0": {[m
         "dependencies": {[m
           "Microsoft.Bcl.AsyncInterfaces": "6.0.0",[m
[31m-          "System.Diagnostics.DiagnosticSource": "6.0.1",[m
[32m+[m[32m          "System.Diagnostics.DiagnosticSource": "8.0.0",[m
           "System.Memory.Data": "1.0.2",[m
           "System.Numerics.Vectors": "4.5.0",[m
           "System.Text.Encodings.Web": "8.0.0",[m
[31m-          "System.Text.Json": "8.0.0",[m
[32m+[m[32m          "System.Text.Json": "8.0.4",[m
           "System.Threading.Tasks.Extensions": "4.5.4"[m
         },[m
         "runtime": {[m
[36m@@ -56,7 +60,7 @@[m
           "Microsoft.Identity.Client.Extensions.Msal": "4.56.0",[m
           "System.Memory": "4.5.4",[m
           "System.Security.Cryptography.ProtectedData": "6.0.0",[m
[31m-          "System.Text.Json": "8.0.0",[m
[32m+[m[32m          "System.Text.Json": "8.0.4",[m
           "System.Threading.Tasks.Extensions": "4.5.4"[m
         },[m
         "runtime": {[m
[36m@@ -343,7 +347,7 @@[m
           "Microsoft.IdentityModel.Protocols.OpenIdConnect": "6.35.0",[m
           "Microsoft.SqlServer.Server": "1.0.0",[m
           "System.Configuration.ConfigurationManager": "6.0.1",[m
[31m-          "System.Diagnostics.DiagnosticSource": "6.0.1",[m
[32m+[m[32m          "System.Diagnostics.DiagnosticSource": "8.0.0",[m
           "System.Runtime.Caching": "6.0.0",[m
           "System.Security.Cryptography.Cng": "5.0.0",[m
           "System.Security.Principal.Windows": "5.0.0",[m
[36m@@ -423,7 +427,7 @@[m
           "Humanizer.Core": "2.14.1",[m
           "Microsoft.CodeAnalysis.CSharp.Workspaces": "4.5.0",[m
           "Microsoft.EntityFrameworkCore.Relational": "8.0.7",[m
[31m-          "Microsoft.Extensions.DependencyModel": "8.0.0",[m
[32m+[m[32m          "Microsoft.Extensions.DependencyModel": "8.0.1",[m
           "Mono.TextTemplating": "2.2.1"[m
         },[m
         "runtime": {[m
[36m@@ -472,29 +476,61 @@[m
           "Microsoft.Extensions.Primitives": "8.0.0"[m
         }[m
       },[m
[32m+[m[32m      "Microsoft.Extensions.Configuration/6.0.1": {[m
[32m+[m[32m        "dependencies": {[m
[32m+[m[32m          "Microsoft.Extensions.Configuration.Abstractions": "8.0.0",[m
[32m+[m[32m          "Microsoft.Extensions.Primitives": "8.0.0"[m
[32m+[m[32m        }[m
[32m+[m[32m      },[m
       "Microsoft.Extensions.Configuration.Abstractions/8.0.0": {[m
         "dependencies": {[m
           "Microsoft.Extensions.Primitives": "8.0.0"[m
         }[m
       },[m
[32m+[m[32m      "Microsoft.Extensions.Configuration.Binder/8.0.0": {[m
[32m+[m[32m        "dependencies": {[m
[32m+[m[32m          "Microsoft.Extensions.Configuration.Abstractions": "8.0.0"[m
[32m+[m[32m        }[m
[32m+[m[32m      },[m
       "Microsoft.Extensions.DependencyInjection/8.0.0": {[m
         "dependencies": {[m
           "Microsoft.Extensions.DependencyInjection.Abstractions": "8.0.0"[m
         }[m
       },[m
       "Microsoft.Extensions.DependencyInjection.Abstractions/8.0.0": {},[m
[31m-      "Microsoft.Extensions.DependencyModel/8.0.0": {[m
[32m+[m[32m      "Microsoft.Extensions.DependencyModel/8.0.1": {[m
         "dependencies": {[m
           "System.Text.Encodings.Web": "8.0.0",[m
[31m-          "System.Text.Json": "8.0.0"[m
[32m+[m[32m          "System.Text.Json": "8.0.4"[m
         },[m
         "runtime": {[m
           "lib/net8.0/Microsoft.Extensions.DependencyModel.dll": {[m
[31m-            "assemblyVersion": "8.0.0.0",[m
[31m-            "fileVersion": "8.0.23.53103"[m
[32m+[m[32m            "assemblyVersion": "8.0.0.1",[m
[32m+[m[32m            "fileVersion": "8.0.724.31311"[m
           }[m
         }[m
       },[m
[32m+[m[32m      "Microsoft.Extensions.Diagnostics.Abstractions/8.0.0": {[m
[32m+[m[32m        "dependencies": {[m
[32m+[m[32m          "Microsoft.Extensions.DependencyInjection.Abstractions": "8.0.0",[m
[32m+[m[32m          "Microsoft.Extensions.Options": "8.0.0",[m
[32m+[m[32m          "System.Diagnostics.DiagnosticSource": "8.0.0"[m
[32m+[m[32m        }[m
[32m+[m[32m      },[m
[32m+[m[32m      "Microsoft.Extensions.FileProviders.Abstractions/8.0.0": {[m
[32m+[m[32m        "dependencies": {[m
[32m+[m[32m          "Microsoft.Extensions.Primitives": "8.0.0"[m
[32m+[m[32m        }[m
[32m+[m[32m      },[m
[32m+[m[32m      "Microsoft.Extensions.Hosting.Abstractions/8.0.0": {[m
[32m+[m[32m        "dependencies": {[m
[32m+[m[32m          "Microsoft.Extensions.Configuration.Abstractions": "8.0.0",[m
[32m+[m[32m          "Microsoft.Extensions.DependencyInjection.Abstractions": "8.0.0",[m
[32m+[m[32m          "Microsoft.Extensions.Diagnostics.Abstractions": "8.0.0",[m
[32m+[m[32m          "Microsoft.Extensions.FileProviders.Abstractions": "8.0.0",[m
[32m+[m[32m          "Microsoft.Extensions.Logging.Abstractions": "8.0.0"[m
[32m+[m[32m        }[m
[32m+[m[32m      },[m
       "Microsoft.Extensions.Logging/8.0.0": {[m
         "dependencies": {[m
           "Microsoft.Extensions.DependencyInjection": "8.0.0",[m
[36m@@ -513,6 +549,15 @@[m
           "Microsoft.Extensions.Primitives": "8.0.0"[m
         }[m
       },[m
[32m+[m[32m      "Microsoft.Extensions.Options.ConfigurationExtensions/6.0.0": {[m
[32m+[m[32m        "dependencies": {[m
[32m+[m[32m          "Microsoft.Extensions.Configuration.Abstractions": "8.0.0",[m
[32m+[m[32m          "Microsoft.Extensions.Configuration.Binder": "8.0.0",[m
[32m+[m[32m          "Microsoft.Extensions.DependencyInjection.Abstractions": "8.0.0",[m
[32m+[m[32m          "Microsoft.Extensions.Options": "8.0.0",[m
[32m+[m[32m          "Microsoft.Extensions.Primitives": "8.0.0"[m
[32m+[m[32m        }[m
[32m+[m[32m      },[m
       "Microsoft.Extensions.Primitives/8.0.0": {},[m
       "Microsoft.Identity.Client/4.56.0": {[m
         "dependencies": {[m
[36m@@ -551,7 +596,7 @@[m
           "Microsoft.IdentityModel.Tokens": "6.35.0",[m
           "System.Text.Encoding": "4.3.0",[m
           "System.Text.Encodings.Web": "8.0.0",[m
[31m-          "System.Text.Json": "8.0.0"[m
[32m+[m[32m          "System.Text.Json": "8.0.4"[m
         },[m
         "runtime": {[m
           "lib/net6.0/Microsoft.IdentityModel.JsonWebTokens.dll": {[m
[36m@@ -653,6 +698,143 @@[m
           }[m
         }[m
       },[m
[32m+[m[32m      "Serilog/4.0.1-dev-02205": {[m
[32m+[m[32m        "runtime": {[m
[32m+[m[32m          "lib/net8.0/Serilog.dll": {[m
[32m+[m[32m            "assemblyVersion": "4.0.0.0",[m
[32m+[m[32m            "fileVersion": "4.0.0.0"[m
[32m+[m[32m          }[m
[32m+[m[32m        }[m
[32m+[m[32m      },[m
[32m+[m[32m      "Serilog.AspNetCore/8.0.2-dev-00341": {[m
[32m+[m[32m        "dependencies": {[m
[32m+[m[32m          "Microsoft.Extensions.Logging": "8.0.0",[m
[32m+[m[32m          "Serilog": "4.0.1-dev-02205",[m
[32m+[m[32m          "Serilog.Extensions.Hosting": "8.0.0",[m
[32m+[m[32m          "Serilog.Formatting.Compact": "2.0.0",[m
[32m+[m[32m          "Serilog.Settings.Configuration": "8.0.2",[m
[32m+[m[32m          "Serilog.Sinks.Console": "5.0.0",[m
[32m+[m[32m          "Serilog.Sinks.Debug": "3.0.0",[m
[32m+[m[32m          "Serilog.Sinks.File": "5.0.0"[m
[32m+[m[32m        },[m
[32m+[m[32m        "runtime": {[m
[32m+[m[32m          "lib/net8.0/Serilog.AspNetCore.dll": {[m
[32m+[m[32m            "assemblyVersion": "8.0.2.0",[m
[32m+[m[32m            "fileVersion": "8.0.2.0"[m
[32m+[m[32m          }[m
[32m+[m[32m        }[m
[32m+[m[32m      },[m
[32m+[m[32m      "Serilog.Extensions.Hosting/8.0.0": {[m
[32m+[m[32m        "dependencies": {[m
[32m+[m[32m          "Microsoft.Extensions.DependencyInjection.Abstractions": "8.0.0",[m
[32m+[m[32m          