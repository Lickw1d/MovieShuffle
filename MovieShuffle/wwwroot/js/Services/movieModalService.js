const movieModalService = {

    create: () => {
        var modal = {
            options: [],
            currentMovie: {},
            selected: {},
            refresh: (id) => {
                $('#sync-modal .tmdbSearch').hide();
                $('#sync-modal .tmdbInfo').hide();
                $('#sync-modal #tmdb-movie-search').html('');

                $('#sync-modal .btn-secondary').removeClass('visible').removeClass('invisible').addClass('visible');
                $('#sync-modal .btn-primary').removeClass('visible').removeClass('invisible').addClass('visible');
                $('#sync-modal .btn-info').removeClass('invisible').removeClass('visible').addClass('invisible');
                $('#sync-modal .btn-dark').removeClass('invisible').removeClass('visible').addClass('invisible');
                $("#sync-modal .tmdb-poster").attr('src', '');

                _modal.getMovie(id);
            },
            getMovie: (id) => {
                return axios.get('Movies/GetMovie?id=' + id).then(response => {
                    _modal.currentMovie = response.data[0];
                    $('#sync-modal-label').html(_modal.currentMovie.title);

                    if (_modal.currentMovie.tmdbId) {

                        $('#sync-modal .btn-secondary').removeClass('visible').removeClass('invisible')
                            .addClass('invisible');
                        $('#sync-modal .btn-primary').removeClass('visible').removeClass('invisible')
                            .addClass('invisible');
                        $('#sync-modal .btn-info').removeClass('invisible').removeClass('visible').addClass('visible');
                        $('#sync-modal .btn-dark').removeClass('invisible').removeClass('visible').addClass('visible');

                        if (_modal.currentMovie.posterUrl)
                            $('#sync-modal .tmdb-poster').attr('src',
                                'http://image.tmdb.org/t/p/w185/' + _modal.currentMovie.posterUrl);

                        if (_modal.currentMovie.releaseDate)
                            $('#sync-modal .release-date').html("<strong>Released:</strong> " +
                                moment(_modal.currentMovie.releaseDate).format('M/D/YYYY'));

                        if (_modal.currentMovie.description)
                            $('#sync-modal .description')
                                .html("<strong>Description:</strong><br/>" + _modal.currentMovie.description);

                        $('#sync-modal .tmdb-link').html('More Info...');
                        $('#sync-modal .tmdb-link').attr('href',
                            'https://www.themoviedb.org/movie/' + _modal.currentMovie.tmdbId);
                        $('#sync-modal .tmdbInfo').show();
                    } else {

                        _modal.search(_modal.currentMovie.title).then(res => {
                            if ($("#tmdb-movie-search").val() && _modal.options.length > 0) {
                                _modal.selected = _modal.options[0];
                            }
                        });

                        $('#sync-modal .tmdbSearch').show();
                    }

                });
            },
            search: (searchString) => {
                return axios.get('Movies/GetExternalMovies?movieTitle=' + encodeURI(searchString)).then(response => {
                    _modal.options = response.data.results;

                    if (_modal.options.length > 0) {
                        $('#sync-modal #tmdb-movie-search').html('');

                        _modal.options.forEach(value => {

                            var year = value && value.release_date ? value.release_date.split("-")[0] : 'Unreleased';
                            var option = "<option value='" + value.id + "'>" + value.title + " (" + year + ")</option>";

                            $('#sync-modal #tmdb-movie-search').append(option);
                        });

                        $('.selectpicker').selectpicker('refresh');
                    }

                });
            },
            getDomElement: (readOnly) => {

                return `<div class="modal fade" id="sync-modal" tabindex="-1" role="dialog" aria-labelledby="rollModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="sync-modal-label"></h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div display="none" class="tmdbInfo">
                                            <div class="tmdb-poster-container">
                                                <img class="tmdb-poster" src=""/>
                                            </div>
                                            <br/>
                                            <div class="release-date"></div>
                                            <br/>
                                            <div class="description"></div>
                                            <br/>
                                            <a class="tmdb-link" href="" target="_blank"></a>
                                        </div>
                                        <div dispaly="none" class="tmdbSearch">
                                            <label for="tmdb-movie-search">Search:</label>
                                            <select type="text" class="selectpicker" data-live-search="true" data-width="50%" id="tmdb-movie-search">
                                            </select>
                                        </div>
                                        <div >
                                        </div>
                                        <div class="modal-footer">` +
                                            (!readOnly
                                                ? ""
                                                : `<button type="button" class="btn btn-secondary visible" data-dismiss="modal">Cancel</button>
                                            <button type="button" class="btn btn-primary visible">Sync</button>
                                            <button type="button" class="btn btn-info visible">Unbind</button>
                                            <button type="button" class="btn btn-dark invisible" data-dismiss="modal">Close</button>`) +
                                            `</div>
                                    </div>
                                </div>
                            </div>
                        </div>`;
            }
        }

        return modal;
    }
}