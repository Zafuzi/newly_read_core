var sources = {
    business: [],
    entertainment: [],
    gaming: [],
    general: [],
    music: [],
    science: [],
    sport: [],
    technology: [],
};

$(function() {
    setAnimations();

    if (!localStorage.getItem("sources")) {
        fetchSources();
    }
    if (!localStorage.getItem("articles")) {
        fetchArticles();
    }
    $("img").unveil(200, function() {
        $(this).on('load', function(){
            $(this).velocity({"opacity": "1"});
        });
        
    });

    console.log("If you are seeing this, the push worked!");
    console.log("Ready");
});

// Get sources from the DB
function fetchSources() {
    fetch("/ReadAPI/GetSources")
        .then(res => {
            console.log(res);
            return res.json();
        })
        .then(json => {
            console.log("Successfully fetched sources: ", json);
            json.map(function(source) {
                switch (source.category) {
                    case 'business':
                        sources.business.push(source);
                        break;
                    case 'entertainment':
                        sources.entertainment.push(source);
                        break;
                    case 'gaming':
                        sources.gaming.push(source);
                        break;
                    case 'general':
                        sources.general.push(source);
                        break;
                    case 'music':
                        sources.music.push(source);
                        break;
                    case 'science-and-nature':
                        sources.science.push(source);
                        break;
                    case 'sport':
                        sources.sport.push(source);
                        break;
                    case 'technology':
                        sources.technology.push(source);
                        break;
                }
            });
            localStorage.setItem('sources', JSON.stringify(sources));
        })
        .catch(err => {
            console.log(err);
        });
}

// Get articles from the DB
function fetchArticles() {
    fetch("/ReadAPI/GetArticles")
        .then(res => {
            console.log(res);
            return res.json();
        })
        .then(json => {
            console.log("Successfully fetched articles: ", json);
            localStorage.setItem('articles', JSON.stringify(json));
        })
        .catch(err => {
            console.log(err);
        });
}
// Set default animation behavior using velocity
function setAnimations() {
    $('#menu_toggle_open').click(function() {
        $('#slide_nav').velocity({
            'height': '100vh'
        });
    });
    $('#menu_toggle_close').click(function() {
        $('#slide_nav').velocity({
            'height': '0'
        });

        $('#main_nav').velocity({
            "opacity": '1'
        }, {
            complete: function() {
                $('#main_nav').css('z-index', '99');
            }
        });
    });
}

function renderArticleToReader(json) {
    var reader_article = $('<div class="reader_article">');
    if (json == null || json.length < 10) {
        article_container.append($('<h4>').text('Sorry, but it looks like that article is not longer available.'));
        article_container.append($('<p>').text('Here is the link for the original article: ' + url));
        $('.reader').html(reader_article);
        return;
    }
    reader_article.append(json.title);
    $('.reader').html(reader_article);
    console.log(json);
}