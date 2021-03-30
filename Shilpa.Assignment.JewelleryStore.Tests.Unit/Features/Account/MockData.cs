using Microsoft.EntityFrameworkCore;
using Moq;
using Shilpa.Assignment.Database.Models;
using System;
using System.Collections.Generic;

namespace Shilpa.Assignment.JewelleryStore.Tests.Unit.Features.Account
{
    internal class MockData
    {
        internal Mock<DbSet<Tenant>> GetTestTenantSet()
        {
            return new List<Tenant>
            {
                new Tenant
                {
                TenantId = Guid.Parse("970CFB54-EE3D-4CA5-8BFA-E8CEFE94DE8E"),
                TenantName = "Siemens"
                }
            }.ToAsyncDbSetMock();
        }
        internal Mock<DbSet<Role>> GetTestRoleSet()
        {
            return new List<Role>
            {
                new Role
                {
                  RoleId=Guid.Parse("3797614A-40FF-45D6-8B2B-F6241F1E7CF4"),
                  RoleName="Regular"
                },
                new Role
                {
                  RoleId=Guid.Parse("8108F84C-2BC5-4683-8CC8-390B0E876946"),
                  RoleName="Privileged"
                }
            }.ToAsyncDbSetMock();
        }

        internal Mock<DbSet<User>> GetTestUsersSet()
        {
            return new List<User>
            {
                new User
                {
                 FirstName="shilpa",
                 LastName="Aggarwal",
                 CreatedOn=DateTime.UtcNow,
                 IsActive=true,
                 Password= "VMGmW9IrXy+UX4woG3jIGs7AiJ+vSHreDCWQmHv533I=",
                 RoleId= Guid.Parse("8108F84C-2BC5-4683-8CC8-390B0E876946"),
                 TenantId=Guid.Parse("970CFB54-EE3D-4CA5-8BFA-E8CEFE94DE8E"),
                 UserName="shilpagarg.raikot",
                 Token="",
                 UserId=Guid.Parse("164531AC-E888-411B-BD77-25DC205A5E43")
                },
                new User
                {
                 FirstName="priya",
                 LastName="Aggarwal",
                 CreatedOn=DateTime.UtcNow,
                 IsActive=true,
                 Password= "VMGmW9IrXy+UX4woG3jIGs7AiJ+vSHreDCWQmHv533I=",
                 RoleId= Guid.Parse("3797614A-40FF-45D6-8B2B-F6241F1E7CF4"),
                 TenantId=Guid.Parse("970CFB54-EE3D-4CA5-8BFA-E8CEFE94DE8E"),
                 UserName="shilpagarg.raikot",
                 Token="",
                 UserId=Guid.Parse("164531AC-E888-411B-BD77-25DC205A5E43")
                }
            }.ToAsyncDbSetMock();
        }

    }
}
