const movieShuffle = movieShuffleService.create();
const _modal = movieModalService.create();
const _page = {

    setCurrentMovie: (movieShuffle) => {
        $("#currentWatch .current-question").html("<span>#" + movieShuffle.currentMovie.question.ordinal + ": " + movieShuffle.currentMovie.question.text + "</span>");

        movieShuffle.currentMovie.questionResponses.forEach(response => {
            $("#currentWatch .current-responses").append("<p  class = 'animated fadeIn'><strong>" + response.userName + ":</strong> " + response.questionResponse.movie.title + "</p>");
        });
    },

    setHeaders: (movieShuffle) => {

        movieShuffle.tableHeaders.forEach((header) => {
            _page.bindHeader($("#movieChoices thead tr"), header);
        });
    },

    bindHeader: (tHeader, header) => {
        tHeader.append("<th data-field = '" + header + "'>" + header + "</th>");
    },

    bindTable: (movieShuffle) => {
        $('#movieChoices').bootstrapTable({
            data: movieShuffle.flattenedTable
        });
    },
    clearHeaders: (tHeader) => {
        tHeader.html('');
    },
    clearModal: () => {
        $("#rollModal .modal-body .responses").html('');
        $("#rollModal .modal-body .nextQuestion").html('');
    },
    clearCurrentMovie: () => {
        $("#currentWatch .current-question").html('');
        $("#currentWatch .current-responses").html('');
    },
    setModal: (movieShuffle) => {
        $("#rollModal .modal-body .nextQuestion").html("<span>#" + movieShuffle.nextUp.question.ordinal + ": " + movieShuffle.nextUp.question.text+"</span>");

        movieShuffle.nextUp.questionResponses.forEach((response) => {
            $("#rollModal .modal-body .responses").append("<p  class = 'animated fadeIn'><strong>" + response.userName + ":</strong> " + response.questionResponse.movie.title + "</p>");
        });
    },

    refresh: () => {

        movieShuffle.getData().then(() => {
            _page.clearCurrentMovie();
            _page.clearHeaders($("#movieChoices thead tr"));

            _page.setCurrentMovie(movieShuffle);
            _page.setHeaders(movieShuffle);
            _page.bindTable(movieShuffle);
        });
    }
}


$(function () {

    _page.refresh();
    $('body').append($(_modal.getDomElement(false)));

    $('body').on('click', '.movieLink', function () { _modal.refresh($(this).data('id')) });

    $("#rollModal").on("shown.bs.modal", function (e) {
        _page.clearModal();
        movieShuffle.Roll();
        _page.setModal(movieShuffle);

    });

    $("#rollModal .btn-primary, #rollModal .btn-secondary").on("click",
        function () {
        _page.clearModal();
    });

    $("#rollModal .btn-primary").on("click",
        function() {
            movieShuffle.SetNext(movieShuffle.nextUp).then(() => { _page.refresh() });
        });
});