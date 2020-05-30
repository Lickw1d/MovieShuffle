
    const _modal = movieModalService.create();
    const _page = {
        movieData: [],
        refresh: () => {
            return axios.get('Movies/GetMovies').then(response => {
        _page.movieData = response.data;
                _.forEach(_page.movieData,
                    movieRow => {
        movieRow.title = "<a href='#' data-toggle='modal' data-target='#sync-modal' class='movieLink' data-id='" + movieRow.id + "'>" + movieRow.title + "</a>",
        movieRow["editButton"] = `<button type='button' data-toggle="modal" data-target="#sync-modal"
                                            style='background-color:` +
        (movieRow.tmdbId ? '#6f6f6f' : '#3bde9a') +
        `;color:white;font-weight:bold;'
                                            class='syncButton btn btn-indigo btn-sm m-0' data-id='` +
        movieRow.id +
        `'>` + (movieRow.tmdbId ? "Unbind" : "Sync") + `</button>`;
                        movieRow["isSynced"] = movieRow.tmdbId
                            ? `<svg class="bi bi-check" width="2em" height="2em" viewBox="0 0 16 16" fill="#3bde9a" xmlns="http://www.w3.org/2000/svg">
        <path fill-rule="evenodd" d="M13.854 3.646a.5.5 0 010 .708l-7 7a.5.5 0 01-.708 0l-3.5-3.5a.5.5 0 11.708-.708L6.5 10.293l6.646-6.647a.5.5 0 01.708 0z" clip-rule="evenodd" />
    </svg>`
                            : "";
                    });
                if ($("#movieTable").bootstrapTable) {

        $("#movieTable").bootstrapTable('load', _page.movieData);
                } else {

    }
                $("#movieTable").bootstrapTable({
        data: _page.movieData,
                    "sPaginationType": "bs_full"
                });
            });
        }
    }

    _page.refresh();

    $(function() {

        $('body').append($(_modal.getDomElement(true)));

        $('body').on('click', '.syncButton, .movieLink',function(){_modal.refresh($(this).data('id'))});

        $('body').on('keyup',
            '.bootstrap-select input[type="search"]',
            function() {

                var searchString = $(this).val();
                if (searchString.length < 3)
                    return;

                _modal.search(searchString);

            });

        $("#sync-modal .btn-primary").on("click",
            function () {

                if (_modal.selected) {
        _modal.currentMovie.tmdbId = _modal.selected.id;
                    _modal.currentMovie.posterUrl = _modal.selected.poster_path;
                    _modal.currentMovie.releaseDate = _modal.selected.release_date;
                    _modal.currentMovie.description = _modal.selected.overview;

                    axios.post('Movies/Sync', _modal.currentMovie).then(() => {
        _modal.refresh(_modal.currentMovie.id);
                        _page.refresh();
                    });
                }
            });

        $("#sync-modal .btn-info").on("click",
            function () {


        _modal.currentMovie.tmdbId = null;
                _modal.currentMovie.posterUrl = null;
                _modal.currentMovie.releaseDate =null;
                _modal.currentMovie.description = null;

                axios.post('Movies/Sync', _modal.currentMovie).then(() => {
        _modal.selected = {};
                    _modal.refresh(_modal.currentMovie.id);
                    _page.refresh();
                });

            });

        $("#tmdb-movie-search").on('change',
            function () {
                var id =parseInt($(this).val());
                _modal.selected = _.find(_modal.options, {"id": id});
            });

    });
