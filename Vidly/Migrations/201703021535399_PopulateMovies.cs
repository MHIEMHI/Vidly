namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovies : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies(Name,Genre,ReleaseDate,DateAdded,NumberInStock)" +
                " VALUES ('Interstellar','Fiction','12/01/2014','2/04/2014',6) ");
            Sql("INSERT INTO Movies(Name,Genre,ReleaseDate,DateAdded,NumberInStock)" +
                " VALUES ('King Kong','Adventure','02/01/2015','2/04/2014',7) ");
            Sql("INSERT INTO Movies(Name,Genre,ReleaseDate,DateAdded,NumberInStock)" +
                " VALUES ('Walk','Adventure','02/11/2014','2/04/2014',10) ");
            Sql("INSERT INTO Movies(Name,Genre,ReleaseDate,DateAdded,NumberInStock)" +
                " VALUES ('God Father','Politics','12/08/2004','2/04/2014',1) ");
            Sql("INSERT INTO Movies(Name,Genre,ReleaseDate,DateAdded,NumberInStock)" +
                " VALUES ('Now U Can See Me','Magic','10/01/2010','2/04/2014',23) ");

        }

        public override void Down()
        {
        }
    }
}
