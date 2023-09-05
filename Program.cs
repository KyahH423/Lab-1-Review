/*       
 *--------------------------------------------------------------------
 * 	   File name: Program.cs
 * 	Project name: VideoGames
 *--------------------------------------------------------------------
 * Author’s name and email:	 Kyah Hanson - hansonkm@etsu.edu				
 *          Course-Section:  CSCI-2910-800
 *           Creation Date:	 8/31/2023		
 * -------------------------------------------------------------------
 */
using System.Collections.Immutable;
using VideoGames;

// Pulling csv file and other variables, file variables from class example provided by William Rochelle
string projectRootFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
string filePath = projectRootFolder + "/videogames.csv";
List<VideoGame> gamesList = new List<VideoGame>();
double gamesListTotal = 0.0;
string userInput = "";
bool run = true;


// Uses StreamReader to read off videogames file provided to us in class and add each line as a VideoGame object to an all games list
using (StreamReader reader = new StreamReader(filePath) )
{
    reader.ReadLine();

    while( !reader.EndOfStream )
    {
        string newGame = reader.ReadLine();
        string[] gameData = newGame.Split(',');

        VideoGame game = new VideoGame(gameData[0], gameData[1], gameData[2], gameData[3], gameData[4], Convert.ToDouble(gameData[5]), Convert.ToDouble(gameData[6]), Convert.ToDouble(gameData[7]), Convert.ToDouble(gameData[8]), Convert.ToDouble(gameData[9]));
        gamesList.Add(game);
    }
    reader.Close();
}
gamesList.Sort();
gamesListTotal = gamesList.Count();


/*        Part 3 and 4      */

// Gets number of games with "Coleco" as a publisher
double colecoCount = gamesList.Count(games => games.Publisher == "Coleco");

// Uses Where() to find all games published by Coleco, they are already in alphabetical order due to the prior sort method on gamesList
IEnumerable<VideoGame> gamesByPublisher = gamesList.Where(VideoGame => VideoGame.Publisher == "Coleco");

// Prints off every game published by Coleco in alphabetical order
foreach ( VideoGame game in gamesByPublisher )
{
    Console.WriteLine(game);
}
// Prints off total number of games, total games developed by Coleco, and overall percentage of games by Coleco.
Console.WriteLine($"\nOut of {gamesListTotal} games, {colecoCount} are developed by Coleco, which is {Math.Round((colecoCount/gamesListTotal) * 100, 2)}%");



/*        Part 5 and 6       */

// Gets number of games with an "Adventure" genre
double adventureCount = gamesList.Count(games => games.Genre == "Adventure");

// Uses Where() to find all games with an "Adventure" genre, they are already in alphabetical order due to the prior sort method on gamesList
IEnumerable<VideoGame> gamesByGenre = gamesList.Where(VideoGame => VideoGame.Genre == "Adventure");

// Prints off every game with the genre: "Adventure" in alphabetical order
foreach (VideoGame game in gamesByGenre)
{
    Console.WriteLine(game);
}
// Prints off total number of games, total games with "Adventure" as the genre, and overall percentage of games with the "Adventure" genre.
Console.WriteLine($"\nOut of {gamesListTotal} games, {adventureCount} are Adventure games, which is {Math.Round((adventureCount / gamesListTotal) * 100, 2)}%");




/*        Part 7      */
//      :)

// Pulls all games with a specified Publisher provided by the user
void PublisherData()
{
    // Gets a list of all publishers to compare to user input for validation
    IEnumerable<string> allPublishers = gamesList.Select(game => game.Publisher.ToLower()).Distinct();
    List<string> publishers = allPublishers.ToList();
    publishers.Sort();

    // Prompts the user for a publisher, checks validation, and provides user with games by publisher when a valid input is entered
    Console.WriteLine("Please enter a publisher from our database to see how many games they have within our database and their percentage:");
    userInput = Console.ReadLine().ToLower();
    while (string.IsNullOrWhiteSpace(userInput) == true || publishers.Contains(userInput) == false)
    {
        Console.WriteLine("I'm sorry, the publisher you entered does not exist within our database. Please enter a publisher and ensure spelling is correct:");
        userInput = Console.ReadLine().ToLower();
    }
    double publisherCount = gamesList.Count(games => games.Publisher.ToLower() == userInput);
    gamesByPublisher = gamesList.Where(VideoGame => VideoGame.Publisher.ToLower() == userInput);

    foreach (VideoGame game in gamesByPublisher)
    {
        Console.WriteLine(game);
    }

    Console.WriteLine($"\nOut of {gamesListTotal} games, {publisherCount} are developed by {userInput}, which is {Math.Round((publisherCount / gamesListTotal) * 100, 2)}%");
}

// Allows user to search for more publishers after searching their first one
PublisherData();
while (run == true)
{
    // Checks for valid input for user
    Console.WriteLine("Would you like to search another publisher? Please enter 'Y' for yes or 'N' to stop: ");
    userInput = Console.ReadLine();
    YesOrNo(userInput);
}


/*        Part 8      */

// Pulls all games with a specified genre provided by the user
void GenreData()
{
    // Gets a list of all genres to compare to user input for validation
    IEnumerable<string> allGenres = gamesList.Select(game => game.Genre.ToLower()).Distinct();
    List<string> genres = allGenres.ToList();
    genres.Sort();

    Console.WriteLine("Please enter a genre from our database to see how many games with that genre we have within our database and their percentage:");
    userInput = Console.ReadLine().ToLower();
    while (string.IsNullOrWhiteSpace(userInput) == true || genres.Contains(userInput) == false)
    {
        Console.WriteLine("I'm sorry, the genre you entered does not exist within our database. Please enter a genre and ensure spelling is correct:");
        userInput = Console.ReadLine().ToLower();
    }
    double genreCount = gamesList.Count(games => games.Genre.ToLower() == userInput);
    gamesByGenre = gamesList.Where(VideoGame => VideoGame.Genre.ToLower() == userInput);

    foreach (VideoGame game in gamesByGenre)
    {
        Console.WriteLine(game);
    }

    Console.WriteLine($"\nOut of {gamesListTotal} games, {genreCount} are {userInput}, which is {Math.Round((genreCount / gamesListTotal) * 100, 2)}%");
}

run = true;
GenreData();
while (run == true)
{
    Console.WriteLine("Would you like to search another genre? Please enter 'Y' for yes or 'N' to stop: ");
    userInput = Console.ReadLine();
    YesOrNo(userInput);
}



// Method used to check for if the user wants to continue
void YesOrNo(string input)
{
    while (string.IsNullOrWhiteSpace(input) == true || char.TryParse(input, out char id) == false || Convert.ToChar(input) != 'y' &&
            Convert.ToChar(input) != 'Y' && Convert.ToChar(input) != 'N' && Convert.ToChar(input) != 'n')
    {
        Console.WriteLine("Error: Invalid Input\nPlease enter 'Y' for yes or 'N' to stop:");
        input = Console.ReadLine();
    }
    if (Convert.ToChar(input) == 'Y' || Convert.ToChar(input) == 'y')
    {
        PublisherData();
    }
    else
    {
        run = false;
    }
}
