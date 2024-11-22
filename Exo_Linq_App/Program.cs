using Exo_Linq_Context;

Console.WriteLine("Exercice Linq");
Console.WriteLine("*************");

DataContext context = new DataContext();

// var exo1 = context.Students.
//     Select(s => new { s.Last_Name, s.BirthDate, s.Login, s.Year_Result});
//
// var exo2 = context.Students.
//     Select(s => s.First_Name + " " + s.Last_Name + " " + s.BirthDate + " " + s.Login);

// var exo3 = context.Students.
//     Select(s =>s.Student_ID + " | " + s.Last_Name + " | " + s.First_Name + " | " + s.BirthDate + " | " + s.Login + " | " + s.Year_Result + " | " + s.Course_ID + " | " + s.Section_ID);

// var exo4 = context.Students
//     .Where(s => s.BirthDate.Year < 1955).Select(s => new
//     {
//         NomComplet = s.First_Name + " " + s.Last_Name,
//         ResultatAnnuel = s.Year_Result,
//         Statut = s.Year_Result >= 12 ? "OK" : "KO"
//     });

// var exo5 = context.Students
//     .Where(s => s.BirthDate >= new DateTime(1955, 1, 1) && s.BirthDate < new DateTime(1965, 1, 1))
//     .Select(s => new
//     {
//         s.Last_Name,
//         s.Year_Result,
//         Categorie = s.Year_Result == 10 ? "Neutre" : s.Year_Result > 10 ? "Supérieur" : "Inférieur"
//     });

// var exo6 = context.Students
//     .Where(s => s.Last_Name.EndsWith("r"))
//     .Select(s => new
//     {
//         s.Last_Name,
//         s.Section_ID
//     });

// var exo7 = context.Students
//     .Where(s => s.Year_Result <= 3)
//     .Select(s => new
//     {
//         s.Last_Name,
//         s.Year_Result
//     })
//     .OrderByDescending(s => s.Year_Result);
    

// var exo8 = context.Students
//     .Where(s => s.Section_ID == 1110)
//     .OrderBy(s => s.Last_Name)
//     .Select(s => new
//     {
//         NomComplet = s.First_Name + " " + s.Last_Name,
//         YearResultat = s.Year_Result,
//     });

// var exo9 = context.Students
//     .Where(s => (s.Section_ID == 1010 || s.Section_ID == 1020)
//                 && (s.Year_Result >= 12 || s.Year_Result <= 18))
//     .OrderBy(s => s.Section_ID)
//     .Select(s => new
//     {
//         NomComplet = s.First_Name + " " + s.Last_Name,
//         s.Section_ID,
//         s.Year_Result
//     });

// var exo10 = context.Students
//     .Where(s => s.Section_ID.ToString().StartsWith("13") && s.Year_Result * 5 <= 60)
//     .OrderByDescending(s => s.Year_Result) // Tri par résultat annuel décroissant
//     .Select(s => new
//     {
//         s.First_Name,
//         s.Section_ID,
//         result_100 = s.Year_Result
//     });


    
    
// foreach (var student in exo10)
// {
//     Console.WriteLine(student);
// }