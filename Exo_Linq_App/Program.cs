using Exo_Linq_Context;

Console.WriteLine("Exercice Linq");
Console.WriteLine("*************");

DataContext context = new DataContext();



var exJoin = context.Students.Join(context.Sections,
    st => st.Section_ID,
    se => se.Section_ID,
    (st, se) => new { NomSection = se.Section_Name, NomEtudiant = st.Last_Name });


// exo 3.1 Donner le résultat annuel moyen pour l’ensemble des étudiants.
var exo31 = context.Students.Select(st => st.Year_Result).Average();
Console.WriteLine($"La moyenne de tout les étudiants est : {exo31}");

// exo 3.2 Donner le plus haut résultat annuel obtenu par un étudiant.
var exo32 = context.Students.Select(st => st.Year_Result).Max();
Console.WriteLine($"Le résultat le plus haut est : {exo32}");

// exo 3.3 Donner la somme des résultats annuels obtenus par les étudiants.
var exo33 = context.Students.Select(st => st.Year_Result).Sum();
Console.WriteLine($"La somme de tout les résultats est : {exo33}");

// exo 3.5 Donner le nombre de lignes qui composent la séquence « Students » ayant obtenu un résultat annuel impair.
var exo34 = context.Students.Select(st => st.Year_Result).Count(r => r % 2!= 0);
Console.WriteLine($"Le nombre d'étudiants ayant un résultat impair est : {exo34}");

// exo 4.1  Donner pour chaque section, le résultat maximum (« Max_Result ») obtenu par les étudiants
var exo41 = context.Sections.GroupJoin(context.Students,
    st => st.Section_ID,
    se => se.Section_ID,
    (st, se) => new { NomSection = st.Section_Name, Max_Result = se.Max(s => s.Year_Result) });

foreach (var item in exo41)
{
    Console.WriteLine($"Section : {item.NomSection}, Résultat maximum : {item.Max_Result}");
}

// exo 4.2 Donner pour toutes les sections commençant par 10, le résultat annuel moyen (« AVGResult ») obtenu par les étudiants.
var exo42 = context.Sections.
    Where(se => se.Section_ID.ToString().StartsWith("10"))
    .GroupJoin(context.Students,
        st => st.Section_ID,
        se => se.Section_ID,
        (st, se) => new { NomSection = st.Section_Name, ResultatAnnuelMoyen = se.Any() ? se.Average(s => s.Year_Result) : 0 });

foreach (var item in exo42)
{
    Console.WriteLine($"Section : {item.NomSection}, Résultat annuel moyen : {item.ResultatAnnuelMoyen}");
}

// exo 4.3 Donner le résultat moyen (« AVGResult ») et le mois en chiffre (« BirthMonth ») pour les étudiants né le même mois entre 1970 et 1985.
var exo43 = context.Students
    .Where(st => st.BirthDate.Year >= 1970 && st.BirthDate.Year <= 1985)
    .GroupBy(st => st.BirthDate.Month)
    .Select(group => new
    {
        Mois = group.Key,
        ResultatMoyen = group.Average(st => st.Year_Result),
    })
    .OrderBy(group => group.Mois);

// exo 4.4 Donner pour toutes les sections qui compte plus de 3 étudiants, la moyenne des résultats annuels (« AVGResult »).
var exo44 = context.Students
    .GroupBy(st => st.Section_ID)
    .Where(group => group.Count() > 3)
    .Select(group => new
    {
        AVGResult = group.Average(st => st.Year_Result),
        Section = group.Key
    });

// exo 4.5 Donner pour chaque cours, le nom du professeur responsable ainsi que la section dont le professeur fait partie.
var exo45 = context.Courses
    .GroupJoin(
        context.Professors,
        course => course.Professor_ID,
        professor => professor.Professor_ID,
        (Course, Professors) => new
        {
            CourseName = Course.Course_Name,
            Professors = Professors.Select(prof => new
            {
                ProfessorName = prof.Professor_Name + " " + prof.Professor_Surname,
                SectionProf = context.Sections.FirstOrDefault(se => se.Section_ID == prof.Section_ID)?.Section_Name,
                sectionName = context.Sections.FirstOrDefault(se => se.Section_ID == prof.Section_ID)?.Section_Name
            })
        });

// exo 4.6 Donner pour chaque section, l’id, le nom et le nom de son délégué. Classer les sections dans l’ordre inverse des id de section.
var exo46 = context.Sections
    .Select(section => new
    {
        SectionName = section.Section_Name,
        SectionID = section.Section_ID,
        DelegateName = context.Students
            .Where(student => student.Student_ID == section.Delegate_ID)
            .Select(student => student.Last_Name + " " + student.First_Name)
            .FirstOrDefault()
    })
    .OrderByDescending(section => section.SectionID);

// exo 4.7 Donner, pour toutes les sections, le nom des professeurs qui en sont membres
var exo47 = context.Sections
    .GroupJoin(
        context.Professors,
        section => section.Section_ID,
        professor => professor.Section_ID,
        (section, professors) => new
        {
            SectionName = section.Section_Name,
            Professors = professors.Select(prof => new
            {
                ProfessorName = (prof.Professor_Name + " " + prof.Professor_Surname),
            })
        });

// exo 4.8 Même objectif que la question 4.7, mais seules les sections comportant au moins un professeur doivent être reprises.
var exo48 = context.Sections
    .GroupJoin(
        context.Professors,
        section => section.Section_ID,
        professor => professor.Section_ID,
        (section, professors) => new
        {
            SectionName = section.Section_Name,
            Professors = professors.Select(prof => new
            {
                ProfessorName = (prof.Professor_Name + " " + prof.Professor_Surname),
            })
        })
    .Where(group => group.Professors.Any());

// exo 4.9 Donner à chaque étudiant ayant obtenu un résultat annuel supérieur ou égal à 12
// son grade en fonction de son résultat annuel et sur base de la table grade. La liste doit être
// classée dans l’ordre alphabétique des grades attribués.

var exo49 = context.Students
    .Where(st => st.Year_Result >= 12)
    .Select(st => new
    {
        StudentName = st.Last_Name + " " + st.First_Name,
        Grade = context.Grades.FirstOrDefault(g => st.Year_Result >= g.Lower_Bound && st.Year_Result <= g.Upper_Bound)
    });

// exo 4.10 Donner la liste des professeurs et la section à laquelle ils se rapportent ainsi que
// le(s) cour(s) (nom du cours et crédits) dont le professeur est responsable. La liste est triée
// par ordre décroissant des crédits attribués à un cours.

var exo410 = context.Professors
    .Join(context.Sections,
        professor => professor.Section_ID,
        section => section.Section_ID,
        (professor, section) => new
        {
            ProfessorName = professor.Professor_Name,
            SectionName = section.Section_Name,
            ProfessorID = professor.Professor_ID
        })
    .Join(context.Courses,
        profSection => profSection.ProfessorID,
        course => course.Professor_ID,
        (profSection, course) => new
        {
            profSection.ProfessorName,
            profSection.SectionName,
            CourseName = course.Course_Name,
            CourseCredits = course.Course_Ects
        })
    .OrderByDescending(result => result.CourseCredits);

// exo 4.11 Donner pour chaque professeur son id et le total des crédits ECTS
// (« ECTSTOT ») qui lui sont attribués. La liste proposée est triée par ordre décroissant de la
// somme des crédits alloués.

var professorECTS = context.Professors
    .Select(p => new 
    {
        ProfessorID = p.Professor_ID,
        ECTSTOT = context.Courses
            .Where(c => c.Professor_ID == p.Professor_ID)
            .Sum(c => c.Course_Ects)
    })
    .OrderByDescending(p => p.ECTSTOT)
    .ToList();

foreach (var professor in professorECTS)
{
    Console.WriteLine($"Professor ID: {professor.ProfessorID}, Total ECTS: {professor.ECTSTOT}");
}





