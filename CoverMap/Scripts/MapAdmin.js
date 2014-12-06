var map = L.map('map');
var markers = {};

map.on('locationfound', function (le) {
    //Get bars within map.getBounds();
    //console.log(JSON.stringify(map.getBounds()));
    //Example on getBounds:
    //{"_southWest":{"lat":55.81841833191133,"lng":12.385904788970947},"_northEast":{"lat":55.82052795137585,"lng":12.392020225524902}}
    var mapBounds = map.getBounds();
    var bounds = {
        P1Lat: mapBounds._southWest.lat,
        P1Lng: mapBounds._southWest.lng,
        P2Lat: mapBounds._northEast.lat,
        P2Lng: mapBounds._northEast.lng
    }
    getAndDisplayDataPoints(bounds);
});

map.on('moveend', function (e) {
    var mapBounds = map.getBounds();
    var bounds = {
        P1Lat: mapBounds._southWest.lat,
        P1Lng: mapBounds._southWest.lng,
        P2Lat: mapBounds._northEast.lat,
        P2Lng: mapBounds._northEast.lng
    }
    getAndDisplayDataPoints(bounds);
});
L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

var placeMarker = function (lat, lng, dataId, data) {
    var m = L.marker([lat, lng]);
    m.addTo(map);
    m.on('click', function (e) {
        getDataPoint(lat, lng, dataId, data, markers[dataId]);
    });
    m.DataID = dataId;
    markers[dataId] = m;
};

var signalToText = function (signal) {
    switch (signal) {
        case 1: return "Intet signal";
        case 2: return "Lidt signal";
        case 3: return "Middel signal";
        case 4: return "Meget signal";
        case 5: return "Godt signal";
    }
};

var sletPoint = function (id) {
    $.ajax({
        url: "/api/Covers/"+id,
        type: "DELETE"
    }).done(function (resData) {
        console.log('SLETTET ' + JSON.stringify(resData));
        window.location.reload();
    }).fail(function (err) {
        console.log(JSON.stringify(err));
    });
};

var showResult = function (res) {
    $('#dataresult li').remove();
    for (var i in res) {
        var tmp = '<li><a href="javascript:sletPoint('+res[i].CoverID+');">Slet</a> ' + res[i].NetworkName + ': '
                    + res[i].Technology + ': '
                    + signalToText(res[i].SignalStrength) + '</li>';
        $('#dataresult').append(tmp);
    }
    $('#popup').modal('show');
};

var getDataPoint = function (lat, lng, dataId, data, marker) {
    //0.01 => 1km
    //0.001 => 100 meter
    var bounds = {
        P1Lat: lat-0.01,
        P1Lng: lng-0.01,
        P2Lat: lat+0.01,
        P2Lng: lng+0.01
    }
    $.ajax({
        url: "/GetDatapointsWithinBounds",
        type: "POST",
        data: bounds
    }).done(function (resData) {
        showResult(resData);
    }).fail(function (err) {
        console.log(JSON.stringify(err));
    });
};

var getAndDisplayDataPoints = function (bounds) {
    $.ajax({
        url: "/GetDatapointsWithinBounds",
        type: "POST",
        data: bounds
    }).done(function (resData) {
        for (var i = 0; i < resData.length; i++) {
            var obj = resData[i];
            placeMarker(obj.Lattitude, obj.Longitude, obj.CoverID, obj);
        }
    }).fail(function (err) {
        alert('Getting points '+err);
    });
};

map.locate({setView: true });