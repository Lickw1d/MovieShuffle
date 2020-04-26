//dependencies: axios
const movieShuffleService = {

    create: () => {
        var movieShuffle = {
            shuffleSet: [],
            remainingMoviesTable: [],
            tableHeaders: [],
            flattenedTable: [],
            nextUp: {},

            getData: () => {
                return axios.get('MovieShuffle/Get').then((response) => {
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
                });

                movieShuffle.tableHeaders = _.keys(tableHash);
            },

            Roll: () => {
                var randIndex = utils.getRandom(0, movieShuffle.shuffleSet.length - 1);
                var nextNum = movieShuffle.shuffleSet[randIndex];

                movieShuffle.nextUp = movieShuffle.remainingMoviesTable[nextNum];
            }

        }

        return movieShuffle;
    }

}



