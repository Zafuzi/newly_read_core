$(function () {
    setAnimations();
    $('header').sticky({ topSpacing: 0 });
    seedSources();
});
const API_KEY = "ccfdc66609fc4b7b87258020b85d4380";
function seedSources(){
    fetch("https://newsapi.org/v1/sources?language=en")
    .then(res => {
        return res.json();
    }).then(json => {
        console.log(json.sources);
        json.sources.map(source => {
            $('.stream_container').append($('<div class="stream_card">').text(source.name));
        });
    });
}
// Set default animation behavior using velocity
    function setAnimations() {

        // Toggle menu on click
        $('.menu_toggle').click(function (e) {
            $('#toggle_nav').hasClass('toggled') ? slide('#toggle_nav', 'horizontal', 0, 250, 0) : slide('#toggle_nav', 'horizontal', 300, 250, 0);

            var dropdown_content = $(e.currentTarget).parent().find('.dropdown_content')[0];
            var dropdown_toggle_arrow = $(e.currentTarget).find('.dropdown_toggle_arrow')[0];
        });

        // Toggle dropdowns on click
        $('.dropdown_toggle').click(function (e) {
            var dropdown_content = $(e.currentTarget).parent().find('.dropdown_content')[0];
            var dropdown_toggle_arrow = $(e.currentTarget).find('.dropdown_toggle_arrow')[0];
            if ($(dropdown_content).hasClass('toggled')) {
                $(dropdown_toggle_arrow).velocity({ rotateZ: '0deg' });
                $(dropdown_content).velocity("slideUp", {
                    complete: function () {
                        $(dropdown_content).removeClass('toggled');
                    }
                });
            } else {
                $(dropdown_toggle_arrow).velocity({ rotateZ: '180deg' });
                $(dropdown_content).velocity("slideDown", {
                    complete: function () {
                        $(dropdown_content).addClass('toggled');
                    }
                });
            }
        });

        // Auto close dropdowns on page load
        $('.dropdown > .toggled').velocity("slideUp", {
            complete: function () {
                $('.dropdown > .toggled').removeClass('toggled');
            }
        })
    }

// Slide Animations for toggled elements
    function slide(el, direction, amount, duration, delay) {
        console.log("Slide: ", el, direction, amount, duration, delay);
        switch (direction) {
            case 'horizontal':
                $(el).velocity({
                    translateX: amount
                }, {
                        duration: duration, delay: delay, complete: function () {
                            amount > 0 ? $(el).addClass('toggled') : $(el).removeClass('toggled');
                        }
                    });
                break;
            case 'height':

                $(el).velocity("slideDown", {
                    duration: duration, delay: delay, complete: function () {
                        amount != 0 ? $(el).addClass('toggled') : $(el).removeClass('toggled');
                    }
                });
                break;
        }
    }