﻿using System;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.User.Dto;
using Scheduler.BL.User.Implementation;
using Scheduler.BL.User.Interface;

namespace Scheduler.Testing.IntegrationTests
{
    public class IntegrationBase
    {
        IUserService User;
        ITeamService Team;
        ILocationService Location;
        ICategoryService Category;

        public IntegrationBase()
        {
            User = new UserService();
            Team = new TeamService();
            Location = new LocationService();
            Category = new CategoryService();
        }

        public UserDto SeedUser()
        {
            UserDto dto = new UserDto()
            {
                UserId = Guid.NewGuid(),
                UserName = $"testing{Guid.NewGuid()}",
                FirstName = "Johnny",
                MiddleInitial = "J",
                LastName = "Appleseed",
                PrimaryEmail = "test@gmail.com",
                BackupEmail = "test2@gmail.com",
                PrimaryPhoneNumber = "9205551234",
                BackupPhoneNumber = "7155551234"
            };

            User.AddUser(dto);
            return dto;
        }

        public void DeleteSeededUser(Guid userId)
        {
            User.DeleteUser(userId);
        }


        public LocationDto SeedLocation()
        {
            LocationDto dto = new LocationDto()
            {
                LocationId = Guid.NewGuid(),
                LocationName = "Test Location"
            };

            Location.AddLocation(dto);
            return dto;
        }

        public void DeleteSeededLocation(Guid locationId)
        {
            Location.DeleteLocation(locationId);
        }


        public TeamDto SeedTeam()
        {
            var locationDto = SeedLocation();

            TeamDto dto = new TeamDto()
            {
                TeamId = Guid.NewGuid(),
                TeamName = "Test Team",
                TeamEmail = "test@gmail.com",
                TeamLeaderId = null,
                TeamDescription = "this isn't a real team",
                LocationId = locationDto.LocationId
            };

            Team.AddTeam(dto);
            return dto;
        }

        public void DeleteSeededTeam(Guid teamId)
        {
            var t = Team.GetTeam(teamId);
            Team.DeleteTeam(teamId);
            DeleteSeededLocation(t.LocationId);
        }


        public CategoryDto SeedCategory()
        {
            CategoryDto dto = new CategoryDto()
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = "Test Category"
            };

            Category.AddCategory(dto);
            return dto;
        }

        public void DeleteSeededCategory(Guid categoryId)
        {
            Category.DeleteCategory(categoryId);
        }
    }
}
