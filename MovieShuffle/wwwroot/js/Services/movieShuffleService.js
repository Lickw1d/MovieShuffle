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
                    flatRow["#"] = value.question.ordinal;
                    flatRow["Question"] = value.question.text;
                    flatRow["Watched"] = value.watched ? "Yes" : "No";

                    value.questionResponses.forEach((response) => {
                        if (!tableHash[response.userName])
                            tableHash[response.userName] = true;

                        flatRow[response.userName] = "<a data-toggle='modal' class='movieLink' data-target='#sync-modal' href='#' data-id='"+response.questionResponse.movie.id+"'>"+response.questionResponse.movie.title+"</a>";
                    });

                    movieShuffle.flattenedTable.push(flatRow);

                    if (!value.watched)
                        movieShuffle.shuffleSet.push(index);

                    if (value.watching)
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



