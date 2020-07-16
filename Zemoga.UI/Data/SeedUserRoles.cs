using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zemoga.UI.Data
{
    internal struct UserData
    {
        internal string userName { get; }
        internal string userEmail { get; }
        internal string userPassword { get; }

        internal UserData(string name, string email, string password)
        {
            this.userName = name;
            this.userEmail = email;
            this.userPassword = password;
        }
    }
    public class SeedUserRoles
    {
        RoleManager<IdentityRole> RoleManager;
        UserManager<IdentityUser> UserManager;

        public SeedUserRoles(IServiceProvider serviceProvider)
        {
            this.RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            this.UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        }

        public async Task CreateUserRoles()
        {
            string[] roleNames = { "Writer", "Editor" };
            UserData[] editorUsers = { new UserData("editor@zemoga.com", "editor@zemoga.com", "Zemoga.1234") };
            UserData[] writerUsers = { new UserData("writer1@zemoga.com", "writer1@zemoga.com", "Zemoga.1234"),
                    new  UserData("writer2@zemoga.com", "writer2@zemoga.com", "Zemoga.1234") };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            foreach (UserData user in editorUsers)
            {
                await createUser(user, "Editor");
            }

            foreach (UserData user in writerUsers)
            {
                await createUser(user, "Writer");
            }
        }

        private async Task createUser(UserData data, string roleName)
        {
            var user = new IdentityUser
            {
                UserName = data.userName,
                Email = data.userEmail
            };
            string userPWD = data.userPassword;
            var _user = await UserManager.FindByEmailAsync(data.userEmail);

            if (_user == null)
            {
                var createUser = await UserManager.CreateAsync(user, userPWD);
                if (createUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, roleName);
                }
            }
            else
            {
                await UserManager.DeleteAsync(_user);
                var createPowerUser = await UserManager.CreateAsync(user, userPWD);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, roleName);
                }
            }
        }

    }
}
