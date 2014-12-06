$(document).ready(function () {

    var handleGotPosition = function(pos) {
        //Get signal strength
        //Get network name
        
        //show popup

        var network = $('#networkname').val();
        var signal = $('#signal').val();
        var techonology = $('#techonology').val();
        var crd = pos.coords;

        //Send to serveren
        //Show waiting while talking to server
        //Tell the user thanks and hide the button?
        var data = {
            Longitude: crd.longitude,
            Lattitude: crd.latitude,
            NetworkName: network,
            SignalStrength: signal,
            Technology: techonology
        };
        $.ajax({
            beforeSend: function () {
                $('#popup').modal('hide');
                console.log('sending data....');
                $('#uploadsuccess').hide();
                $('#uploaderror').hide();
            },
            url: "/api/Covers/PostCover",
            type: "POST",
            data: data
        }).done(function (resData) {
            //console.log(JSON.stringify(resData));
            $('#uploadsuccess').show();
        }).fail(function (err) {
            //console.log(err);
            $('#uploaderror').show();
        });
    };

    var errorGettingPos = function (err) {
        alert('Error getting position. Error was: ' + err.message);
    };

    var options = {
        enableHighAccuracy: true,
        timeout: 5000,
        maximumAge: 0
    };

    $('#senddata').click(function () {
        navigator.geolocation.getCurrentPosition(handleGotPosition, errorGettingPos, options);
    });
});