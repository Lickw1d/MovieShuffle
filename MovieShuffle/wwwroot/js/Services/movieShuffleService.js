//dependencies: axios
const movieShuffleService = {

    create: () => {
        var movieShuffle = {
            shuffleSet: [],
            remainingMoviesTable: [],
            tableHeaders: [],
            flattenedTable: [],
            currentMovie: {},
            nextUp: {},

            getData: () => {
                return axios.get('MovieShuffle/Get').then((response) => {
                    movieShuffle.shuffleSet = [];
                    movieShuffle.remainingMoviesTable = [];
                    movieShuffle.tableHeaders = [];
                    movieShuffle.flattenedTable = [];
                    currentMovie: {}
                    movieShuffle.nextUp = {};
                    movieShuffle.remainingMoviesTable = response.data;
                    movieShuffle.flattenTable();
                });
            },

            flattenTable: () => {
                var tableHash = {
                    "#": true,
                    "Question": true,
                    "Watched": true
                };

                movieShuffle.remainingMoviesTable.forEach((value, index) => {
                    var flatRow = {};
                    flatRow["#"] = value.Question.Ordinal;
                    flatRow["Question"] = value.Question.Text;
                    flatRow["Watched"] = value.Watched ? "Yes" : "No";

                    value.QuestionResponses.forEach((response) => {
                        if (!tableHash[response.UserName])
                            tableHash[response.UserName] = true;

                        flatRow[response.UserName] = response.QuestionResponse.Response;
                    });

                    movieShuffle.flattenedTable.push(flatRow);

                    if (!value.Watched)
                        movieShuffle.shuffleSet.push(index);

                    if (value.Watching)
                        movieShuffle.currentMovie = value;
                });

                movieShuffle.tableHeaders = _.keys(tableHash);
            },

            Roll: () => {
                var randIndex = utils.getRandom(0, movieShuffle.shuffleSet.length - 1);
                var nextNum = movieShuffle.shuffleSet[randIndex];

                movieShuffle.nextUp = movieShuffle.remainingMoviesTable[nextNum];
            },

            SetNext: (movieItem) => {
                return axios({method:'POST',url:'MovieShuffle/SetNext', data:movieItem});
            }

        }

        return movieShuffle;
    }

}



