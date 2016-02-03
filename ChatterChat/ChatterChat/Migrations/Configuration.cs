namespace ChatterChat.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ChatterChat.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ChatterChat.Models.ChatServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ChatterChat.Models.ChatServiceContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Users.AddOrUpdate(x => x.ID,
                new User() { ID = 1, Name = "SAdmin" }
            );

            context.Messages.AddOrUpdate(x => x.ID,
                new Message() { ID = 1, UserID = 1, Content = "Welcome!" }
                );
        }
    }
}
