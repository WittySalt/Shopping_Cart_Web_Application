using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Shopping_Cart_Web_Application_V1._0.Data;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;

namespace Shopping_Cart_Web_Application_V1._0.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                   serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Product.Any())
                {
                    return;   // DB has been seeded
                }
                context.User.AddRange(
                    new User
                    {
                        Id = new Guid("4F6D6864-EC3B-4519-AD32-28F7A5F4364D"),
                        UserName = "John",
                        Password = "123456",
                        Email = null,
                        PhoneNumber = null,
                        isAdmin = false

                    },
                    new User
                    {
                        Id = new Guid("C7B4D3B5-E0EC-49E3-8D5F-97D6A42E7BE7"),
                        UserName = "Admin",
                        Password = "admin",
                        Email = null,
                        PhoneNumber = null,
                        isAdmin = true
                    }
                );
                context.Product.AddRange(
                    new Product
                    {
                        ProductName= "Introduction to ASP.NET",   
                        ProductDescription = "Dive into the essentials of ASP.NET, understanding its architecture, versions,and setting up the development environment.Perfect for those starting their journey in web development.",
                        Image = "NET1.png",
                        Price = 648.99
                    },
                    new Product
                    {
                        ProductName = "Web Layouts and Controls with ASP.NET",
                        ProductDescription = "Learn to create intuitive page layouts using Web Forms controls, master the page life cycle, and interact seamlessly with both the UI and server-side code.",
                        Image = "NET2.png",
                        Price = 159.99
                    },
                    new Product
                    {
                        ProductName = "State Management in ASP.NET",
                        ProductDescription = "Explore various techniques for managing state in web applications, from view state to query strings, and learn to implement them in an e-commerce shopping cart scenario.",
                        Image = "NET3.png",
                        Price = 199.99
                    },
                    new Product
                    {
                        ProductName = "Authentication and Security in ASP.NET",
                        ProductDescription = "Understand the implementation of authentication and authorization, protect sensitive data, and prevent common vulnerabilities like SQL injection and XSS in your web applications.",
                        Image = "NET4.png",
                        Price = 109.99
                    },
                    new Product
                    {
                        ProductName = "Data Access with Entity Framework",
                        ProductDescription = "Master data operations using Entity Framework, execute CRUD operations, and query data efficiently using LINQ within your ASP.NET applications.",
                        Image = "NET5.png",
                        Price = 69.49
                    },
                    new Product
                    {
                        ProductName = "Mastering ASP.NET MVC",
                        ProductDescription = "Delve into the principles of the MVC architecture, create robust models, design interactive views, and handle complex controller logic, streamlining your web application development.",
                        Image = "NET6.png",
                        Price = 101.99
                    },
                    new Product
                    {
                        ProductName = "Web APIs and RESTful Services in ASP.NETC",
                        ProductDescription = "Design and develop RESTful services using ASP.NET Web API. Understand the intricacies of HTTP, and create a solid backend for cross-platform applications.",
                        Image = "NET7.png",
                        Price = 64.99
                    },
                    new Product
                    {
                        ProductName = "Middleware and Dependency Injection in ASP.NET Core",
                        ProductDescription = "Configure and utilize middleware and understand the power of dependency injection to write modular, maintainable, and testable code in ASP.NET Core applications.",
                        Image = "NET8.png",
                        Price = 29.99
                    },
                    new Product
                    {
                        ProductName = "Unit and Integration Testing in ASP.NET",
                        ProductDescription = "Implement reliable unit and integration testing practices, use mock objects for testing, ensuring your ASP.NET application is robust and error-free before deployment.",
                        Image = "NET9.png",
                        Price = 89.99
                    },
                    new Product
                    {
                        ProductName = "Deploying ASP.NET Applications",
                        ProductDescription = "Master the deployment of ASP.NET applications in various environments, including configuring IIS, and containerizing applications with Docker for real-world production scenarios.",
                        Image = "NET10.png",
                        Price = 459.99
                    },
                    new Product
                    {
                        ProductName = "Advanced Topics in ASP.NET Core",
                        ProductDescription = "Explore advanced features and capabilities of ASP.NET Core, diving deep into areas such as global exception handling, advanced middleware, custom filters, and performance optimization. Ideal for developers who want to leverage ASP.NET Core to its fullest potential.",
                        Image = "NET11.png",
                        Price = 699.59
                    },
                    new Product
                    {
                        ProductName = "Building Real-Time Applications with SignalR",
                        ProductDescription = "Learn to develop real-time web applications using ASP.NET Core SignalR. This course covers the concepts of real-time data updates, live communication, and seamless client-server messaging, essential for applications like live chats, instant notifications, and collaborative platforms.",
                        Image = "NET12.png",
                        Price = 599.99
                    }
                );
                context.Cart.AddRange(
                    new Cart
                    {
                        UserId = "c7b4d3b5-e0ec-49e3-8d5f-97d6a42e7be7"
                    },
                    new Cart
                    {
                        UserId = "4f6d6864-ec3b-4519-ad32-28f7a5f4364d"
                    }
                );
                context.CartDetail.AddRange(
                    new CartDetail
                    {
                        CartId = 1,
                        ProductId = 12,
                        Quantity = 1,
                        UnitPrice = 599.99
                    }
                );
                context.Order.AddRange(
                    new Order
                    {
                        UserId = "c7b4d3b5-e0ec-49e3-8d5f-97d6a42e7be7",
                        CreateDate = DateTime.Parse("2021-05-20 12:35:27.6614047"),
                        IsDeleted = false,
                    },
                    new Order
                    {
                        UserId = "c7b4d3b5-e0ec-49e3-8d5f-97d6a42e7be7",
                        CreateDate = DateTime.Parse("2021-09-16 13:09:39.9067394"),
                        IsDeleted = false,
                    },
                    new Order
                    {
                        UserId = "c7b4d3b5-e0ec-49e3-8d5f-97d6a42e7be7",
                        CreateDate = DateTime.Parse("2022-08-31 13:29:07.9874774"),
                        IsDeleted = false,
                    },
                    new Order
                    {
                        UserId = "c7b4d3b5-e0ec-49e3-8d5f-97d6a42e7be7",
                        CreateDate = DateTime.Parse("2023-10-25 15:32:01.2197073"),
                        IsDeleted = false,
                    }
                );
                context.OrderDetail.AddRange(
                    new OrderDetail
                    {
                        OrderId = 1,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("0E146599-72B1-4A77-8975-4661C4886FF2")
                    },
                    new OrderDetail
                    {
                        OrderId = 1,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("1179E61F-1D5B-42A9-BE6C-770B76A5CE8E")
                    },
                    new OrderDetail
                    {
                        OrderId = 1,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("6AA4F734-5C64-492C-94B2-5B0D365A9B60")
                    },
                    new OrderDetail
                    {
                        OrderId = 1,
                        ProductId = 7,
                        UnitPrice = 64.99,
                        ActivationCode = new Guid("0477B2E2-518C-4C2C-BBA4-31E672A8863C")
                    },
                    new OrderDetail
                    {
                        OrderId = 1,
                        ProductId = 7,
                        UnitPrice = 64.99,
                        ActivationCode = new Guid("722078AD-D4E7-425F-A11D-AE401FA3B029")
                    },
                    new OrderDetail
                    {
                        OrderId = 1,
                        ProductId = 7,
                        UnitPrice = 64.99,
                        ActivationCode = new Guid("D245B98B-1B4D-4C08-BE52-115A5E2FA78F")
                    },
                    new OrderDetail
                    {
                        OrderId = 2,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("5417D1DC-7AE5-41B7-9BEB-9F68387F5F6A")
                    },
                    new OrderDetail
                    {
                        OrderId = 2,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("BF9FED9C-3463-4746-BAB0-57596F8F536D")
                    },
                    new OrderDetail
                    {
                        OrderId = 2,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("9F33E761-5D67-4C6B-AC28-7815779AD31E")
                    },
                    new OrderDetail
                    {
                        OrderId = 2,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("9D3EBD68-09E4-4E0A-B819-CE377DE7EC9F")
                    },
                    new OrderDetail
                    {
                        OrderId = 2,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("61D5BA1C-82F5-4BB3-86D3-E9428F78DF14")
                    },
                    new OrderDetail
                    {
                        OrderId = 2,
                        ProductId = 5,
                        UnitPrice = 69.49,
                        ActivationCode = new Guid("E56345C5-77D3-4B3A-95E8-03C9968B6535")
                    },
                    new OrderDetail
                    {
                        OrderId = 2,
                        ProductId = 5,
                        UnitPrice = 69.49,
                        ActivationCode = new Guid("D5BE560D-4E4F-4BE5-B311-43F435293F0B")
                    },
                    new OrderDetail
                    {
                        OrderId = 2,
                        ProductId = 5,
                        UnitPrice = 69.49,
                        ActivationCode = new Guid("AB44E300-96ED-4224-83E4-3A780966DD41")
                    },
                    new OrderDetail
                    {
                        OrderId = 2,
                        ProductId = 1,
                        UnitPrice = 648.99,
                        ActivationCode = new Guid("06D505B5-C9F9-4182-91D3-F172B9BECEE1")
                    },
                    new OrderDetail
                    {
                        OrderId = 3,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("E661CEE5-EC1C-4F04-A978-B369F2FF618F")
                    },
                    new OrderDetail
                    {
                        OrderId = 3,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("CB1AC9D1-E9F6-45F8-8EA1-033A89F1DA41")
                    },
                    new OrderDetail
                    {
                        OrderId = 3,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("54E812DB-397F-4341-BCA1-F13F31FCA6F8")
                    },
                    new OrderDetail
                    {
                        OrderId = 4,
                        ProductId = 3,
                        UnitPrice = 199.99,
                        ActivationCode = new Guid("DF6FC906-ACD8-467F-8F96-412ABD4B6A9C")
                    },
                    new OrderDetail
                    {
                        OrderId = 4,
                        ProductId = 2,
                        UnitPrice = 159.99,
                        ActivationCode = new Guid("28B55FA9-AED7-4338-882F-A0BD70528C32")
                    },
                    new OrderDetail
                    {
                        OrderId = 4,
                        ProductId = 1,
                        UnitPrice = 648.99,
                        ActivationCode = new Guid("20136DF8-44B5-46A9-BA52-18B96B3D931C")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
