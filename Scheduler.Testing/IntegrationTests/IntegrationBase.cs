﻿using System;
using Scheduler.BL.Schedule.Dto;
using Scheduler.BL.Schedule.Implementation;
using Scheduler.BL.Schedule.Interface;
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
        IScheduleService Schedule;
        IWarrantyService Warranty;

        public IntegrationBase()
        {
            User = new UserService();
            Team = new TeamService();
            Location = new LocationService();
            Category = new CategoryService();
            Schedule = new ScheduleService();
            Warranty = new WarrantyService();
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
                LocationName = "Test Location 2"
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
            Guid locationId;

            var t = Team.GetTeam(teamId);
            locationId = t.LocationId;

            Team.DeleteTeam(teamId);
            DeleteSeededLocation(locationId);
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



        public ScheduleDto SeedSchedule()
        {
            var teamDto = SeedTeam();
            var userDto = SeedUser();

            ScheduleDto dto = new ScheduleDto()
            {
                ScheduleId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId,
                StartDate = Convert.ToDateTime("5/1/2000"),
                EndDate = Convert.ToDateTime("5/5/2000")
            };

            Schedule.SaveSchedule(dto);
            return dto;
        }

        public void DeleteSeededSchedule(Guid scheduleId)
        {
            var item = Schedule.GetSchedule(scheduleId);
            Schedule.DeleteSchedule(scheduleId);

            DeleteSeededTeam(item.TeamId);
            DeleteSeededUser(item.UserId);
        }


        public WarrantyDto SeedWarranty()
        {
            var teamDto = SeedTeam();
            var userDto = SeedUser();

            WarrantyDto dto = new WarrantyDto()
            {
                WarrantyId = Guid.NewGuid(),
                WarrantyName = "Test Warranty",
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId,
                WarrentyDescription = "Test Warranty Desc",
                StartDate = Convert.ToDateTime("1/1/2000"),
                EndDate = Convert.ToDateTime("2/7/2000")
            };

            Warranty.AddWarranty(dto);
            return dto;
        }

        public void DeleteSeededWarranty(Guid warrantyId)
        {
            var item = Warranty.GetWarranty(warrantyId);
            Warranty.DeleteWarranty(item.WarrantyId);

            DeleteSeededTeam(item.TeamId);
            DeleteSeededUser(item.UserId);
        }

    }
}
