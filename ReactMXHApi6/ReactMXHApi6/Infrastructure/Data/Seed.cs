using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReactMXHApi6.Core.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace ReactMXHApi6.Infrastructure.Data
{
    public class Seed
    {
        // Preenche o banco de dados com usuários e posts iniciais
        public static async Task SeedUsers(UserManager<AppUser> userManager, DataContext context)
        {
            // Verifica se não há posts no banco de dados
            if (!await context.Posts.AnyAsync())
            {
                // Verifica se não há usuários no banco de dados
                if (!await userManager.Users.AnyAsync())
                {
                    // Cria uma lista de usuários iniciais
                    var users = new List<AppUser> {
                        new AppUser { 
                            UserName = "arnon00", 
                            DisplayName = "Arnon Nascimento",
                            ImageUrl = "images/user2.jpg"
                        },
                        new AppUser{ UserName="gian00", DisplayName = "Gian das P4r4d4s", ImageUrl = "images/user0.jpg"},
                        new AppUser{UserName="gabriel", DisplayName = "Gabriel das 0 Horas", ImageUrl = "images/user7.jpg" },
                        new AppUser{UserName="diego00", DisplayName = "Diego Caroninha", ImageUrl = "images/user3.jpg" },
                        new AppUser{UserName="sandrim", DisplayName = "Sandrim das mOlIeReS", ImageUrl = "images/user4.jpg" },
                        new AppUser{UserName="flavim", DisplayName = "Fravim da Revoada", ImageUrl = "images/user6.jpg" },
                        new AppUser{UserName="paulo00", DisplayName = "Paulo Madeiras", ImageUrl = "images/user5.jpg" },
                        new AppUser{UserName="brennao", DisplayName = "Brennao Pirigoso", ImageUrl = "images/user8.jpg" },
                        new AppUser{UserName="itallo", DisplayName = "Itallo Mohamed", ImageUrl = "images/user1.jpg" },
                        new AppUser{UserName="dudu00", DisplayName = "Dudu da Task Certeira", ImageUrl = "images/user.jpg" },
                    };
                    // Cria cada usuário na base de dados usando o UserManager
                    foreach (var user in users)
                    {
                        await userManager.CreateAsync(user, "123456");
                    }
                }
                // Cria uma lista de posts iniciais
                var posts = new List<Post>
                {
                    new Post{
                        NoiDung="Não é falta de cafe, Nem tequila. " +
                        "É meu código que não compila. ",
                        UserId = userManager.Users.FirstOrDefault().Id,
                    },
                    new Post{
                        NoiDung="!false " +
                        "É engraçado porque é verdadeiro",
                        UserId = userManager.Users.FirstOrDefault().Id,
                    },
                    new Post{
                        NoiDung="Proficional de TI não tem filhos gêmeos. " +
                        "Na verdade ele tem um filho e uma cópia de segurança.",
                        UserId = userManager.Users.FirstOrDefault().Id,
                    }
                };
                // Adiciona os posts à tabela de posts no contexto de dados
                context.Posts.AddRange(posts);
                // Salva as alterações no banco de dados
                await context.SaveChangesAsync();
            }                      
        }
    }
}
