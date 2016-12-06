$(function () {
    var lock = new Auth0Lock('MgXiTQoJTIoeMlavO9cLJIYBCyL6lWYw', 'zafuzi.auth0.com', {
        auth: {
            params: {
                scope: 'openid email'
            }
        }
    });
    var userProfile;

    $('.btn-login').click(function (e) {
        e.preventDefault();
        lock.show();
    });

    $('.btn-logout').click(function (e) {
        e.preventDefault();
        logout();
    });

    lock.on("authenticated", function (authResult) {
        lock.getProfile(authResult.idToken, function (error, profile) {
            if (error) {
                // Handle error
                console.log(error);
                return;
            }

            localStorage.setItem('id_token', authResult.idToken);

            // Display user information
            $('.nickname').text(profile.nickname);
            $('.avatar').attr('src', profile.picture);
        });
    });

    //retrieve the profile

    var id_token = localStorage.getItem('id_token');
    if (id_token) {
        lock.getProfile(id_token, function (err, profile) {
            if (err) {
                return alert('There was an error getting the profile: ' + err.message);
            }
            // Display user information
            $('.nickname').text(profile.nickname);
            $('.avatar').attr('src', profile.picture);
        });
    }

    setAnimations();
    $("img").unveil(200, function () {
        $(this).on('load', function () {
            $(this).velocity({ "opacity": "1" });
            //console.log($(this).attr('src'));
        });
        if ($(this).attr('data-src') == null) {
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
            $('.dropdown').velocity({
                'opacity': '0'
            });
            $('#menu_toggle_open').removeClass('toggled').velocity({ opacity: 0, },{
                duration: 200,
                complete: function () {
                    $(this).text('Menu');
                    $(this).velocity({ opacity: 1 }, {duration: 200});
                    $('.dropdown').css({ 'overflow': "hidden" });
                }
            });
        } else {
            $('.dropdown').velocity({
                'opacity': '1'
            });
            $('#menu_toggle_open').addClass('toggled').velocity({ opacity: 0, }, {
                duration: 200,
                complete: function () {
                    $(this).text('Close');
                    $(this).velocity({ opacity: 1 },{duration: 200});
                    $('.dropdown').css({ 'overflow': "auto" });
                }
            });
        }
    });
}

function logout(){
    localStorage.removeItem('id_token');
    userProfile = null;
    window.location.href = "/";
}