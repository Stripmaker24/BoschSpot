var map = L.map('map', {
    maxBounds: new L.LatLngBounds(new L.LatLng(51.6, 5.1), new L.LatLng(51.8, 5.5)),
    maxBoundsViscosity: 1.0
}).setView([51.6978162, 5.3036748], 13);

var mapNL = L.tileLayer('https://service.pdok.nl/brt/achtergrondkaart/wmts/v2_0/pastel/EPSG:3857/{z}/{x}/{y}.png', {
    minZoom: 13,
    maxZoom: 19,
    attribution: 'Kaartgegevens &copy; <a href="kadaster.nl">Kadaster</a>'
});

var allMarkerGroup = L.layerGroup();
var myMarkerGroup = L.layerGroup();

mapNL.addTo(map);
var oms = new OverlappingMarkerSpiderfier(map, {
    keepSpiderfied: false
});

oms.addListener('click', function (marker) {
    var id = marker.ID;
    var photoName = marker.PhotoName;
    var category = marker.Category;
    var group = marker.Group;
    var contender = marker.Contender;
    var rarity = marker.Rarity;
    var points = marker.Points;
    var dateTime = marker.DateTime

    document.getElementById('spotID').innerHTML = '#' + id;
    document.getElementById('spotPoints').innerHTML = 'aantal punten: ' + points;
    document.getElementById('spotImg').src = 'SpotImg/' + photoName;
    document.getElementById('spotTitle').innerHTML = category + ' > ' + group + ' > ' + contender;
    document.getElementById('spotRarity').innerHTML = 'Zeldaamheid: ' + rarity;
    document.getElementById('spotDateTime').innerHTML = 'Gespot op: ' + dateTime;
    $('#exampleModal').modal('show');
});

$(oms).ready(
    GetAllSpotsForMap(),
    GetMySpotsForMap()
);

var baseLayer = {
    'Alle Spots': allMarkerGroup,
    'Mijn Spots': myMarkerGroup
};
var overlayLayer = {};

L.control.layers(baseLayer, overlayLayer).addTo(map);

function GetMySpotsForMap()
{
    $.ajax({
        type: 'GET',
        url: '/GetMySpotsForMap',
        success: function (result) {
            var icon = L.icon({
                iconUrl: '/Img/mymarker.png',
                iconSize: [20, 35],
                popupAnchor: [0, -15]
            })
            for (var i = 0; i < result.length; i++) {
                console.log('Test');
                var marker = L.marker([result[i].spot.lat, result[i].spot.long], {
                    title: 'Details',
                    icon: icon
                });
                marker.ID = result[i].spot.id;
                marker.PhotoName = result[i].spot.photoName;
                marker.Category = result[i].categoryName;
                marker.Group = result[i].groupName;
                marker.Contender = result[i].contenderName;
                marker.Rarity = result[i].rarityType;
                marker.Points = result[i].spot.points;
                marker.DateTime = ParseDate(result[i].spot.spotTimeDate);

                var popup = new L.Popup();
                oms.addListener('mouseover', function (marker) {
                    popup.setContent('<img src="/SpotImg/' + marker.PhotoName + '" style="height:50px; width: auto;">');
                    popup.setLatLng(marker.getLatLng());
                    map.openPopup(popup);
                });
                marker.bindPopup('<img src="/SpotImg/' + result[i].spot.photoName + '" style="height:50px; width: auto;">')
                    .on('mouseover', function (e) {
                        this.openPopup();
                    })
                    .on('mouseout', function (e) {
                        this.closePopup();
                    })
                    .addTo(myMarkerGroup);
                oms.addMarker(marker);
            }
        }
    })
};

function GetAllSpotsForMap()
{
    $.ajax({
        type: 'GET',
        url: '/GetSpotsForMap',
        success: function (result) {
            var icon = L.icon({
                iconUrl: '/Img/marker.png',
                iconSize: [20, 35],
                popupAnchor: [0, -15]
            })
            for (var i = 0; i < result.length; i++) {
                var marker = L.marker([result[i].spot.lat, result[i].spot.long], {
                    title: 'Details',
                    icon: icon
                });
                marker.ID = result[i].spot.id;
                marker.PhotoName = result[i].spot.photoName;
                marker.Category = result[i].categoryName;
                marker.Group = result[i].groupName;
                marker.Contender = result[i].contenderName;
                marker.Rarity = result[i].rarityType;
                marker.Points = result[i].spot.points;
                marker.DateTime = ParseDate(result[i].spot.spotTimeDate);

                var popup = new L.Popup();
                oms.addListener('mouseover', function (marker) {
                    popup.setContent('<img src="/SpotImg/' + marker.PhotoName + '" style="height:50px; width: auto;">');
                    popup.setLatLng(marker.getLatLng());
                    map.openPopup(popup);
                });
                marker.bindPopup('<img src="/SpotImg/' + result[i].spot.photoName + '" style="height:50px; width: auto;">')
                    .on('mouseover', function (e) {
                        this.openPopup();
                    })
                    .on('mouseout', function (e) {
                        this.closePopup();
                    })
                    .addTo(allMarkerGroup);
                oms.addMarker(marker);
            }
        }
    })
};

function ParseDate(str) {
    date = new Date(str);
    if (date.getSeconds() < 10) {
        var sec = "0" + date.getSeconds();
    }
    else {
        var sec = date.getSeconds();
    }
    if (date.getHours() < 10) {
        var hour = "0" + date.getHours();
    }
    else {
        var hour = date.getHours();
    }

    if (date.getMinutes() < 10) {
        var min = "0" + date.getMinutes();
    }
    else {
        var min = date.getMinutes();
    }
    if (date.getDate() < 10) {
        var day = "0" + date.getDate();
    }
    else {
        var day = date.getDate();
    }
    if (date.getMonth() + 1 < 10) {
        var month = "0" + (date.getMonth() + 1);
        console.log(month);
    }
    else {
        var month = date.getMonth() + 1;
    }
    var str = day + "-" + month + "-" + date.getFullYear() + " " + hour + ":" + min + ":" + sec;
    return str;
};