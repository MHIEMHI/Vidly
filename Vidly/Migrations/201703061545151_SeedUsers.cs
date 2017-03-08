namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6e497c19-d26f-4932-af2f-ff6fad480605', N'admin@vidly.com', 0, N'ADVwCjvRgCAcd4+yTUIh7YlKyL0+Nb+5Mrttaa9N/ANon5a1ezN3skDAKnH/amzT2A==', N'd6be5a3f-5742-475c-9c8c-258d20d994b5', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9ab6042a-e069-48db-a931-caf63af5c0bc', N'guest@vidly.com', 0, N'AILQb8Ah5qF2e74+tFbNcr30VkKSrLnkt1BWQby+paGQhfK4PrNk4QLsMmE0DmOLBw==', N'b2479a3e-bf7d-44d0-bb2b-1466f4bd9849', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
            ");
            Sql(@"
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3d5f26be-ba3e-4c63-92ae-461c7b89ab85', N'CanManageMovies')
            ");
            Sql(@"
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6e497c19-d26f-4932-af2f-ff6fad480605', N'3d5f26be-ba3e-4c63-92ae-461c7b89ab85')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
