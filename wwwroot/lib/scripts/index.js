$(function () {
    setAnimations();
    $("img").unveil(200, function () {
        $(this).on('load', function () {
            $(this).velocity({ "opacity": "1" });
            //console.log($(this).attr('src'));
        });
        if($(this).attr('data-src') == null){
            console.log("SMALL")
        }
        $(this).on('error', function () {
            //console.log("Error: " + $(this).attr('src'));
            $(this).attr('src', 'lib/images/default.png');
        });
    });
});


// Set default animation behavior using velocity
function setAnimations() {
    $('#menu_toggle_open').click(function () {
        if ($('#menu_toggle_open').hasClass('toggled')) {
            $('#slide_nav').velocity({
                'height': '0'
            });
            $('#menu_toggle_open').removeClass('toggled').velocity({opacity: 0,}, {
                complete: function () {
                    $(this).text('Menu');
                    $(this).velocity({ opacity: 1 });
                    $('#slide_nav').css({'overflow': "hidden"});
                }
            });
        } else {
            $('#slide_nav').velocity({
                'height': '100vh'
            });
            $('#menu_toggle_open').addClass('toggled').velocity({opacity: 0,}, {
                complete: function () {
                    $(this).text('Close');
                    $(this).velocity({ opacity: 1 });
                    $('#slide_nav').css({'overflow': "auto"});
                }
            });
        }
    });
}