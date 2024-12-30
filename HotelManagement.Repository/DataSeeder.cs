﻿using HotelManagement.Core.Entities;
using HotelManagement.Core.Enums;

namespace HotelManagement.Repository;
public static class DataSeeder
{
    public static async Task SeedUsers(AppDbContext appDbContext)
    {
        if (!appDbContext.Users.Any())
        {
            var users = new List<User> {
                new User{Username="ahmed",Email="ahmed@yahoo.com",Password="ahmed!@#111",Role=Role.User,Country="Cairo",Phone="01527032578",CreatedAt=DateTime.Now,CreatedBy=0},
                new User{Username="alaa",Email="alaa@gmail.com",Password="alaa!@#111",Role=Role.User,Country="Alex",Phone="01113072179",CreatedAt=DateTime.Now,CreatedBy=0},
                new User{Username="abdalla",Email="abdalla@hotmail.com",Password="abdalla!@#111",Role=Role.User,Country="Giza",Phone="01021832132",CreatedAt=DateTime.Now,CreatedBy=0}
            };

            await appDbContext.Users.AddRangeAsync(users);
            await appDbContext.SaveChangesAsync();
        }
    }


}
