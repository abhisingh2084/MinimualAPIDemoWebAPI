
namespace MinimualAPIDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            List<Student> students = new List<Student>();
            app.MapGet("/student", () =>
            {
                return students;
            });

            app.MapGet("/student/{id}", (int id) =>
            {
                int index = students.FindIndex(s => s.Id == id);
                if (index < 0)
                {
                    return Results.NotFound();
                }
                return Results.Ok(students[index]);
            });

            app.MapPost("/student", (Student student) =>
            {
                students.Add(student);
            });

            app.MapPut("/student/{id}", (int id, Student student) => 
            {
                int index = students.FindIndex(s => s.Id == id);
                if(index < 0)
                {
                    return Results.NotFound();
                }
                students[index] = student;
                return Results.Ok();
            });

            app.MapDelete("/student/{id}", (int id) =>
            {
                int count = students.RemoveAll(s => s.Id == id);
                if (count > 0) { return Results.Ok(); }
                return Results.NotFound();
            });

            app.Run();
        }
    }
}
