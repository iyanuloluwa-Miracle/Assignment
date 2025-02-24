using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml;

// Define what a Person looks like
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Skills { get; set; }
}

// Define what our whole data looks like
class PeopleList
{
    public List<Person> People { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Let's work with JSON and XML!");
        
        // Step 1: Read and show skills from JSON
        Console.WriteLine("\n--- STEP 1: Reading skills from JSON ---");
        ReadSkillsFromJson();
        
        // Step 2: Make a new JSON file with a new person
        Console.WriteLine("\n--- STEP 2: Making a new JSON file ---");
        AddNewPerson();
        
        // Step 3: Turn JSON into XML
        Console.WriteLine("\n--- STEP 3: Converting JSON to XML ---");
        ConvertJsonToXml();
        
        // Step 4: Make a totally different XML file
        Console.WriteLine("\n--- STEP 4: Making a new XML file ---");
        MakeNewXmlFile();
        
        Console.WriteLine("\nAll done! Press any key to exit...");
        Console.ReadKey();
    }
    
    static void ReadSkillsFromJson()
    {
        try
        {
            // Try to read the file
            string jsonText = File.ReadAllText("data.json");
            
            // Turn JSON text into C# objects
            PeopleList allPeople = JsonSerializer.Deserialize<PeopleList>(jsonText);
            
            // Show all the skills
            foreach (Person person in allPeople.People)
            {
                Console.WriteLine($"{person.Name}'s skills:");
                foreach (string skill in person.Skills)
                {
                    Console.WriteLine($"  - {skill}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oops! Something went wrong: {ex.Message}");
            Console.WriteLine("I'll create a sample data.json file for you...");
            
            // Create a sample file if it doesn't exist
            CreateSampleJsonFile("data.json");
            Console.WriteLine("Please run the program again!");
        }
    }
    
    static void CreateSampleJsonFile(string fileName)
    {
        // Make sample data
        PeopleList sampleData = new PeopleList
        {
            People = new List<Person>
            {
                new Person
                {
                    Name = "Bob",
                    Age = 12,
                    Skills = new List<string> { "Scratch", "HTML", "Python" }
                }
            }
        };
        
        // Save it to a file
        string jsonText = JsonSerializer.Serialize(sampleData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, jsonText);
        Console.WriteLine($"Created sample file: {fileName}");
    }
    
    static void AddNewPerson()
    {
        try
        {
            // Read existing data
            string jsonText = File.ReadAllText("data.json");
            PeopleList allPeople = JsonSerializer.Deserialize<PeopleList>(jsonText);
            
            // Create a new person
            Person newPerson = new Person
            {
                Name = "Sally",
                Age = 11,
                Skills = new List<string> { "JavaScript", "Minecraft Modding", "Drawing" }
            };
            
            // Add the new person to our list
            allPeople.People.Add(newPerson);
            
            // Save to a new file
            string newJsonText = JsonSerializer.Serialize(allPeople, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("newdata.json", newJsonText);
            
            Console.WriteLine($"Added {newPerson.Name} to newdata.json");
            Console.WriteLine($"{newPerson.Name}'s skills:");
            foreach (string skill in newPerson.Skills)
            {
                Console.WriteLine($"  - {skill}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oops! Something went wrong: {ex.Message}");
        }
    }
    
    static void ConvertJsonToXml()
    {
        try
        {
            // Read existing data
            string jsonText = File.ReadAllText("newdata.json");
            PeopleList allPeople = JsonSerializer.Deserialize<PeopleList>(jsonText);
            
            // Create a new XML document
            XmlDocument doc = new XmlDocument();
            
            // Add XML declaration
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(declaration);
            
            // Create root element
            XmlElement root = doc.CreateElement("PeopleList");
            doc.AppendChild(root);
            
            // Add each person
            foreach (Person person in allPeople.People)
            {
                // Create person element
                XmlElement personElement = doc.CreateElement("Person");
                root.AppendChild(personElement);
                
                // Add name
                XmlElement nameElement = doc.CreateElement("Name");
                nameElement.InnerText = person.Name;
                personElement.AppendChild(nameElement);
                
                // Add age
                XmlElement ageElement = doc.CreateElement("Age");
                ageElement.InnerText = person.Age.ToString();
                personElement.AppendChild(ageElement);
                
                // Add skills
                XmlElement skillsElement = doc.CreateElement("Skills");
                personElement.AppendChild(skillsElement);
                
                // Add each skill
                foreach (string skill in person.Skills)
                {
                    XmlElement skillElement = doc.CreateElement("Skill");
                    skillElement.InnerText = skill;
                    skillsElement.AppendChild(skillElement);
                }
            }
            
            // Save to file
            doc.Save("output.xml");
            Console.WriteLine("Saved people data as XML to output.xml");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oops! Something went wrong: {ex.Message}");
        }
    }
    
    static void MakeNewXmlFile()
    {
        try
        {
            // Create a new XML document
            XmlDocument doc = new XmlDocument();
            
            // Add XML declaration
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(declaration);
            
            // Create root element - something completely different!
            XmlElement root = doc.CreateElement("FavoriteGames");
            doc.AppendChild(root);
            
            // Add some games
            AddGame(doc, root, "Minecraft", "Sandbox", 2011, true);
            AddGame(doc, root, "Roblox", "Platform", 2006, true);
            AddGame(doc, root, "Fortnite", "Battle Royale", 2017, false);
            
            // Save to file
            doc.Save("games.xml");
            Console.WriteLine("Created a new XML file about games: games.xml");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oops! Something went wrong: {ex.Message}");
        }
    }
    
    static void AddGame(XmlDocument doc, XmlElement parent, string name, string type, int year, bool hasCreativeMode)
    {
        // Create game element
        XmlElement game = doc.CreateElement("Game");
        parent.AppendChild(game);
        
        // Add game details
        XmlElement nameElement = doc.CreateElement("Name");
        nameElement.InnerText = name;
        game.AppendChild(nameElement);
        
        XmlElement typeElement = doc.CreateElement("Type");
        typeElement.InnerText = type;
        game.AppendChild(typeElement);
        
        XmlElement yearElement = doc.CreateElement("ReleaseYear");
        yearElement.InnerText = year.ToString();
        game.AppendChild(yearElement);
        
        XmlElement creativeElement = doc.CreateElement("HasCreativeMode");
        creativeElement.InnerText = hasCreativeMode.ToString();
        game.AppendChild(creativeElement);
    }
}