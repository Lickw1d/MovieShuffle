﻿const movieShuffle = movieShuffleService.create();
const getDataResponse = movieShuffle.getData;

const _page = {
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

    clearModal: () => {
        $("#rollModal .modal-body .responses").html('');
        $("#rollModal .modal-body .nextQuestion").html('');
    },

    setModal: (movieShuffle) => {
        $("#rollModal .modal-body .nextQuestion").html("<span>#" + movieShuffle.nextUp.Question.Ordinal + ": " + movieShuffle.nextUp.Question.Text+"</span>");

        movieShuffle.nextUp.QuestionResponses.forEach((response) => {
            $("#rollModal .modal-body .responses").append("<p  class = 'animated fadeIn'><strong>" + response.UserName + ":</strong> " + response.QuestionResponse.Response + "</p>");
        });
    }
}


$(function () {

    getDataResponse().then(() => {
        _page.setHeaders(movieShuffle);
        _page.bindTable(movieShuffle);
    });


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
            movieShuffle.SetNext(movieShuffle.nextUp);
        });
});